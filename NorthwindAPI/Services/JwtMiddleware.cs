using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NorthwindAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace NorthwindAPI.Services
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _settings;
        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> options)
        {
            _next = next;
            _settings = options.Value;
        }
        public async Task Invoke(HttpContext context, [FromServices] IUserServiceAsync service)
        {
            //HTTP-Header
            // Authorization: Bearer <token>
            var header = context.Request.Headers["Authorization"];
            var headerValue = header.FirstOrDefault();
            var token = headerValue?.Split(" ").Last();
            if(!string.IsNullOrEmpty(token) )
            {
                //have the token, now we need to extract key
                var key = Encoding.UTF8.GetBytes(_settings.AppSecret);
                var signingKey = new SymmetricSecurityKey(key);
                var tokenValidationParameter = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = signingKey,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero //adjusting for timespan difference
                };
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                tokenHandler.ValidateToken(
                    token: token,
                    validationParameters: tokenValidationParameter,
                    validatedToken: out SecurityToken outputToken
                    );

                var jwtToken = outputToken as JwtSecurityToken;
                var userId = jwtToken?.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
                var roleName = jwtToken?.Claims.FirstOrDefault(c => c.Type == "Role")?.Value;
                var email = jwtToken?.Claims.FirstOrDefault(c => c.Type == "Email")?.Value;

                //Get the user details based on userId, provide one addtional method in 
                //IUserServiceAsync interfact which will retrive the userObject basd on UserId that is provided here

                var user = new UserModel() { UserId = Convert.ToInt32(userId), RoleName = roleName, Email = email };

                //this object will now be added to the context items, so that other parts of thd app can extract
                // and utimlize the object

                context.Items["User"] = user;
            }

            //last stmt in middleware
            await _next(context);
        }
    }
}
