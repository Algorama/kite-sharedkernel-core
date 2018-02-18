using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using JWT;
using SharedKernel.Domain.Dtos;
using SharedKernel.Api.Security;

namespace SharedKernel.Api.Filters
{
    public class UserAuthorizationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            Token token = null;
            try
            {
                token = actionContext.HttpContext.RecuperarToken();
            }
            catch (SignatureVerificationException)
            {
                actionContext.Result = new UnauthorizedResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                actionContext.Result = new StatusCodeResult(500);
            }

            if (token == null || DateTime.Now > token.DataExpiracao)
                actionContext.Result = new UnauthorizedResult();

            base.OnActionExecuting(actionContext);
        }
    }
}