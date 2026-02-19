using Moq;
using Microsoft.EntityFrameworkCore;
using Connectamente.API.Services.UsuarioService;
using Connectamente.API.Data;
using Connectamente.API.Models;
using Connectamente.API.Services.FileService;
using Connectamente.API.Services.PacienteService;
using Connectamente.API.Services.PsicologoService;
using Connectamente.API.DTOs.UsersDTOs;
using Microsoft.AspNetCore.Http;

namespace Connectamente.Tests;

public class UsuarioServiceTests
{
    private readonly AppDbContext _context;
    private readonly Mock<IFileService> _fileMock = new();
    private readonly Mock<IPacienteService> _pacienteMock = new();
    private readonly Mock<IPsicologoService> _psicologoMock = new();
    // Para o UserManager, costumamos usar um mock mais complexo ou passar null se não for usado no método testado
    private readonly UsuarioService _service;

    public UsuarioServiceTests()
    {
        // Criando um banco de dados na memória para cada teste
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new AppDbContext(options);

        // Instanciando o serviço com os mocks
        _service = new UsuarioService(
            _context,
            _fileMock.Object,
            null!, // UserManager (null para este teste simples)
            _pacienteMock.Object,
            _psicologoMock.Object
        );
    }
    public class ObterUsuarioPorId : UsuarioServiceTests
    {
        [Fact]
        public async Task ObterUsuarioPorId_DeveRetornarUserDto_QuandoUsuarioExiste()
        {
            // Arrange (Preparar)
            var usuarioId = "123";
            var usuario = new Usuario { Id = usuarioId, Nome = "Ana", Sobrenome = "Julia", Email = "ana@rock.com" };
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            // Act (Ação)
            var resultado = await _service.ObterUsuarioPorId(usuarioId);

            // Assert (Verificação)
            Assert.NotNull(resultado);
            Assert.Equal("Ana Julia", resultado.NomeCompleto);
            Assert.Equal(usuarioId, resultado.Id);
        }

        [Fact]
        public async Task ObterUsuarioPorId_DeveRetornarNulo_QuandoUsuarioNaoExiste()
        {
            // Act
            var resultado = await _service.ObterUsuarioPorId("id-inexistente");

            // Assert
            Assert.Null(resultado);
        }
    }

    public class DeletarUsuario : UsuarioServiceTests
    {
        [Fact]
        public async Task DeletarUsuario_DeveRemoverUsuario_QuandoIdExiste()
        {
            // Arrange (Preparar)
            var idExistente = "user-123";
            var usuario = new Usuario
            {
                Id = idExistente,
                Nome = "Andre",
                Sobrenome = "Matos",
                Email = "shaman@test.com"
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            // Act (Ação)
            var resultado = await _service.DeletarUsuario(idExistente);

            // Assert (Verificação)
            Assert.NotNull(resultado); // Garante que o método retornou o objeto deletado
            Assert.Equal(idExistente, resultado.Id);

            // O "Pulo do Gato": Verificar se ele sumiu do banco de verdade
            var usuarioNoBanco = await _context.Usuarios.FindAsync(idExistente);
            Assert.Null(usuarioNoBanco);
        }

        [Fact]
        public async Task DeletarUsuario_DeveRetornarNulo_QuandoIdNaoExiste()
        {
            // Act
            var resultado = await _service.DeletarUsuario("id-que-nao-existe");

            // Assert
            Assert.Null(resultado);
        }
    }

    public class ObterTodosUsuarios : UsuarioServiceTests
    {
        [Fact]
        public async Task ObterTodosUsuarios_DeveRetornarListaDeUserDto_QuandoExistiremUsuarios()
        {
            // Arrange (Preparar)
            var usuarios = new List<Usuario>
    {
        new Usuario { Id = "1", Nome = "Ozzy", Sobrenome = "Osbourne", Email = "blacksabbath@teste.com" },
        new Usuario { Id = "2", Nome = "Ronnie", Sobrenome = "Dio", Email = "dio@teste.com" }
    };

            _context.Usuarios.AddRange(usuarios);
            await _context.SaveChangesAsync();

            // Act (Ação)
            var resultado = await _service.ObterTodosUsuarios();

            // Assert (Verificação)
            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count()); // Verifica se trouxe os dois
            Assert.Contains(resultado, u => u.Nome == "Ozzy"); // Verifica se o Ozzy está na lista
            Assert.Contains(resultado, u => u.Nome == "Ronnie"); // Verifica se o Dio está na lista
        }

        [Fact]
        public async Task ObterTodosUsuarios_DeveRetornarListaVazia_QuandoNaoExistiremUsuarios()
        {
            // Act
            var resultado = await _service.ObterTodosUsuarios();

            // Assert
            Assert.Empty(resultado); // Verifica se retorna uma lista vazia (e não null)
        }
    }

    public class AtualizarUsuario : UsuarioServiceTests
    {
        [Fact]
        public async Task AtualizarUsuario_DeveAtualizarDadosEFoto_QuandoUsuarioExisteEArquivoEnviado()
        {
            // Arrange (Preparar)
            var usuarioId = "user-update-123";
            var usuarioOld = new Usuario { Id = usuarioId, Nome = "Antigo", Sobrenome = "Nome", Foto = "foto_antiga.jpg" };
            _context.Usuarios.Add(usuarioOld);
            await _context.SaveChangesAsync();

            // Simulando o arquivo de imagem (IFormFile)
            var arquivoMock = new Mock<IFormFile>();

            // Simulando o DTO com novos dados
            var updateDto = new UserUpdateDto { Nome = "Novo", Sobrenome = "Sobrenome" };

            // Configurando o Mock do FileService para retornar um caminho fictício
            _fileMock.Setup(f => f.SaveFileAsync(It.IsAny<IFormFile>(), It.IsAny<string>()))
                     .ReturnsAsync("img/usuarios/nova_foto.jpg");

            // Act (Ação)
            var resultado = await _service.AtualizarUsuario(usuarioId, arquivoMock.Object, updateDto);

            // Assert (Verificação)
            Assert.NotNull(resultado);
            Assert.Equal("Novo", resultado.Nome);
            Assert.Equal("img/usuarios/nova_foto.jpg", resultado.Foto);

            // Verifica se o FileService tentou deletar a foto antiga
            _fileMock.Verify(f => f.DeleteFileAsync("foto_antiga.jpg"), Times.Once);
        }
    }
}