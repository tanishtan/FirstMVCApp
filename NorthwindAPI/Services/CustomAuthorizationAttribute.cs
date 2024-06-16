using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NorthwindAPI.Models;

namespace NorthwindAPI.Services
{
    public class CustomAuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.Items["User"] as UserModel;
            if(user is null)
            {
                //context.Result = new ForbidResult(Scheme); //sends 401: Unauthorized/forbidden response
                context.Result = new JsonResult(
                    new { message = "You are not authorized to use these API, contact your admin. " }
                    )
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
            }
        }
    }
}
