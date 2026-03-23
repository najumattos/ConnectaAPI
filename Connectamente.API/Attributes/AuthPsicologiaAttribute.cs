using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Connectamente.API.Attributes;

public class AuthPsicologiaAttribute(string perfil) : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        //quando esse atributo for chamado quer dizer que apenas usuarios que tem autorizaçao para acessar o modulo de psicologia podem acessar a rota
        //o parametro perfil verifica se o usuario é psicologo.Coordenador / psicologo.Estudante / Psicologo.particular
        // 1. Pega o usuário do Token
        var usuario = context.HttpContext.User;

        // 2. Verifica se ele tem a Claim (etiqueta) do módulo
        // Lembra que no Login você vai salvar "Modulo" = "Psicologia" ou "EducaçãoFisica"
        var temModulo = usuario.Claims.Any(c => c.Type == "Role" && c.Value == perfil);

        if (!temModulo)
        {
            // Se ele não pertence ao módulo, barramos o acesso (403 Forbidden)
            context.Result = new ForbidResult();
        }
    }
}