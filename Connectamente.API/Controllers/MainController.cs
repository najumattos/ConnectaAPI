using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Connectamente.API.Controllers;


[ApiController]
[Route("api/[controller]")]
//[Authorize]    // Todas as rotas tem que ser protegidas.
//[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]     //BadRequest 400 cliente que errou, não enviou os dados corretamente
//[ProducesResponseType(StatusCodes.Status401Unauthorized)]    //401 Unauthorized: O usuário não enviou um token ou o token expirou. (O sistema não sabe quem você é).
//[ProducesResponseType(StatusCodes.Status403Forbidden)]       //403 Forbidden: O usuário está autenticado, mas o token dele não tem a "Claim" ou "Role" necessária para acessar esse endpoint específico. (O sistema sabe quem você é, mas você não tem permissão).
//[ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
public abstract class MainController : ControllerBase
{
    // Propriedade protegida: acessível apenas por quem herda (UsuariosController, etc)
     //  protected string UsuarioIdToken => User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? string.Empty;


}


