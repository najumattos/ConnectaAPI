using Moq;
using Microsoft.EntityFrameworkCore;
using Connectamente.API.Data;
using Microsoft.AspNetCore.Identity;
using Connectamente.API.Enums;
using Connectamente.API.Usuario;
using Connectamente.API.Psicologo.PsicologoService;
using Connectamente.API.Psicologo;

namespace Connectamente.Tests;

public class PsicologoServiceTests
{
    private readonly AppDbContext _context;
    private readonly Mock<UserManager<UsuarioModel>> _userManagerMock;
    private readonly PsicologoService _service;

    public PsicologoServiceTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new AppDbContext(options);

        // Mock do UserManager (necessário para o construtor)
        var store = new Mock<IUserStore<UsuarioModel>>();
        _userManagerMock = new Mock<UserManager<UsuarioModel>>(store.Object, null!, null!, null!, null!, null!, null!, null!, null!);

        _service = new PsicologoService(_context, _userManagerMock.Object);
    }

    public class ObterPsicologoPorId : PsicologoServiceTests
    {
        [Fact]
        public async Task ObterPsicologoPorId_DeveRetornarDto_QuandoPsicologoExiste()
        {
            // Arrange
            var id = "psico-1";
            var usuario = new UsuarioModel { Id = id, Nome = "Bruce", Sobrenome = "Dickinson" };
            var psicologo = new PsicologoModel

            {

                UsuarioId = usuario.Id,

                CRP = "12345",

                Usuario = usuario,

                Descricao = "Descrição válida",

                ModalidadeDeAtendimento = ModalidadeAtendimento.Presencial,

                AbordagensTerapeuticas = new List<AbordagemTerapeutica>(),

                CondicoesTerapeuticas = new List<CondicaoTerapeutica>(),

                TiposPacientes = new List<TipoPaciente>()

            };



            _context.Usuarios.Add(usuario);
            _context.Psicologos.Add(psicologo);
            await _context.SaveChangesAsync();

            // Act
            var resultado = await _service.ObterPsicologoPorId(id);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("12345", resultado.CRP);
            Assert.Equal(id, resultado.IdPsicologo);
        }
    }

    public class DeletarPsicologo : PsicologoServiceTests
    {
        [Fact]
        public async Task DesativarConta_DeveMudarParaDesativado_QuandoSucesso()
        {
            // Arrange
            var id = "psico-delete";
            var usuario = new UsuarioModel { Id = id, TipoPerfil = TipoPerfil.Psicologo, Nome="Ana", Sobrenome="Julia" };
            var psicologo = new PsicologoModel

            {

                UsuarioId = usuario.Id,

                CRP = "06/12345",

                Usuario = usuario,

                Descricao = "Descrição válida",

                ModalidadeDeAtendimento = ModalidadeAtendimento.Presencial,

                AbordagensTerapeuticas = new List<AbordagemTerapeutica>(),

                CondicoesTerapeuticas = new List<CondicaoTerapeutica>(),

                TiposPacientes = new List<TipoPaciente>()

            };


            _context.Usuarios.Add(usuario);
            _context.Psicologos.Add(psicologo);
            await _context.SaveChangesAsync();

            _userManagerMock.Setup(u => u.FindByIdAsync(id)).ReturnsAsync(usuario);

            // Act
            var resultado = await _service.DesativarPerfilPsicologo(id);

            
          // Assert.NotNull(resultado); 
            Assert.Equal(TipoPerfil.Desativado, usuario.TipoPerfil);

            // Verifica se removeu da tabela de Psicologos
         /*  var existeNoBanco = await _context.Psicologos.AnyAsync(p => p.UsuarioId == id);
            Assert.False(existeNoBanco);               */
        }
    }
}