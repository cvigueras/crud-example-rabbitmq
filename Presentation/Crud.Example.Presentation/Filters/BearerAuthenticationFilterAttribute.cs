using Crud.Example.Main.Auth.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Crud.Example.Api.Filters
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public class BearerAuthenticationFilterAttribute : Attribute, IAuthorizationFilter
    {
        private readonly IAuthService _authService;
        public BearerAuthenticationFilterAttribute(IAuthService authService)
        {
            _authService = authService;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context == null) 
            { 
                return; 
            }
            bool auth = _authService.ValidateToken(context.HttpContext.Request.Headers["Authorization"]);
            if (!auth)
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}