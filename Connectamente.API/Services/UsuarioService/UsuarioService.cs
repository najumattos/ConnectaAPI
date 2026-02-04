using Connectamente.API.Data;
using Connectamente.API.DTOs.UsersDTOs;
using Connectamente.API.Models;
using Connectamente.API.Services.FileService;
using Connectamente.API.Services.PacienteService;
using Connectamente.API.Services.PsicologoService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Connectamente.API.Services.UsuarioService;

public class UsuarioService(
    AppDbContext context,
    IFileService fileService,
    UserManager<Usuario> userManager,
    IPacienteService pacienteService,
    IPsicologoService psicologoService) : IUsuarioService
{
    private readonly AppDbContext _context = context;
    private readonly IFileService _fileService = fileService;
    private readonly UserManager<Usuario> _userManager = userManager;

    public async Task<IEnumerable<UserDto>> ObterTodosUsuarios()
    {
        var usuarios = await _context.Usuarios
           .AsNoTracking()
           .ToListAsync();
        return usuarios.Select(u => MapearUserDto(u));


    }
    
    public async Task<UserDto> ObterUsuarioPorId(string id)
    {
        var u = await _context.Usuarios
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);
        if (u == null) return null;

        return MapearUserDto(u);
    }
    
    public async Task<UserDto> AtualizarUsuario(string idUsuario, IFormFile arquivo, UserUpdateDto usuarioUpdateDto)
    {
        var usuarioBanco = await _context.Usuarios.FindAsync(idUsuario);
        if (usuarioBanco == null) return null;
        if (arquivo != null)
        {
            await AtualizarFoto(usuarioBanco, arquivo);
        }

        AtualizarCampos(usuarioBanco, usuarioUpdateDto);
        // Salvamos tudo de uma única vez (Uma única viagem ao banco!)
        await _context.SaveChangesAsync();
        return MapearUserDto(usuarioBanco);
    }
        
    public async Task<Usuario> DeletarUsuario(string id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario == null)
        {
            return null;
        }
        _context.Usuarios.Remove(usuario);
        await _context.SaveChangesAsync();
        return usuario;
    }


    #region Métodos Auxiliares
    private async Task<string> AtualizarFoto(Usuario usuario, IFormFile novaFoto)

    {
        //var usuario = await _context.Usuarios.FindAsync(usuarioId);
        if (usuario.Id == null) return null;

        //Se o usuário já tiver uma foto, a antiga é deletada
        if (!string.IsNullOrEmpty(usuario.Foto))
        {
            await _fileService.DeleteFileAsync(usuario.Foto);
        }

        var novoPath = await _fileService.SaveFileAsync(novaFoto, "img/usuarios");

        // 3. Atualiza o caminho da string no banco de dados
        usuario.Foto = novoPath;

        // 4. Retornamos a URL completa para o Front-end já exibir a imagem
        return _fileService.GetFileUrl(novoPath);
    }
    
    private static void AtualizarCampos(Usuario u, UserUpdateDto userUpdateDto)
    {
        // Só atualiza se o que veio do DTO não for nulo ou vazio
        if (!string.IsNullOrWhiteSpace(userUpdateDto.Nome))
            u.Nome = userUpdateDto.Nome;

        if (!string.IsNullOrWhiteSpace(userUpdateDto.Sobrenome))
            u.Sobrenome = userUpdateDto.Sobrenome;

        if (!string.IsNullOrWhiteSpace(userUpdateDto.Celular))
            u.PhoneNumber = userUpdateDto.Celular;
    }

    public UserDto MapearUserDto(Usuario u)
    {
        /*O conceito de "Flattening" (Achatamento)
         Ao "trazer" esses campos do usuario(nome e foto) no DTO, você entrega um "pacote pronto". A tela de "Listagem de Psicólogos" recebe tudo o que precisa em uma única requisição.
         */
        return new UserDto
        {
            Id = u.Id,
            Nome = u.Nome,
            Email = u.Email,
            Sobrenome = u.Sobrenome,
            NomeCompleto = $"{u.Nome} {u.Sobrenome}",
            Foto = u.Foto,
            Celular = u.PhoneNumber,
            DataNascimento = u.DataNascimento.ToString("dd/MM/yyyy"),
            TipoPerfil = u.TipoPerfil.ToString()

        };
    }

    public async Task CriarPerfilAuto(Usuario usuario)
    {
        if (usuario.TipoPerfil == Enums.TipoPerfil.Paciente)
        {
            await pacienteService.CriarPacienteAuto(usuario);
            await _userManager.AddToRoleAsync(usuario, "Paciente");
        }
        if (usuario.TipoPerfil == Enums.TipoPerfil.Psicologo)
        {
            await psicologoService.CriarPsicologoAuto(usuario);
            await _userManager.AddToRoleAsync(usuario, "Psicologo");
        }
    }
    #endregion
}