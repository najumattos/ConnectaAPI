using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Connectamente.API.Attributes;
public class ValidarIdRouteAttribute : ActionFilterAttribute
{
    //verifica se o id informado é o mesmo da rota, ou seja, verifica se o usuario está tentando acessar ou editar um recurso que não é dele
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        // Procuramos por qualquer parâmetro seja exatamente "id" 
        var idParam = context.ActionArguments
            .FirstOrDefault(x => x.Key.Equals("id", StringComparison.OrdinalIgnoreCase));

        if (idParam.Value == null || string.IsNullOrWhiteSpace(idParam.Value.ToString()))
        {
            context.Result = new BadRequestObjectResult("O ID informado na rota é inválido ou obrigatório.");
        }

        base.OnActionExecuting(context);
    }
}
