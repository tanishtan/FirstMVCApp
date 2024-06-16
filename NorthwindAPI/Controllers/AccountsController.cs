using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NorthwindAPI.Models;
using NorthwindAPI.Services;
using NuGet.Packaging.Signing;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NorthwindAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AccountsController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly IUserServiceAsync _userService;

        public AccountsController(
            IOptions<AppSettings> options,
            IUserServiceAsync service
            )
            {
            _appSettings = options.Value;
            _userService = service;
            }

        //public UserModel LoggedInUser


        //api/accounts/authenticate
        [HttpPost("authenticate")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userService.AuthenticateAsync(model);
            if (user == null)
            {
                return NotFound("The resource not found");
            }
            //Create the Identity based on claims
            var identity = new ClaimsIdentity(
                new[]
                {
                    new Claim("UserId",user.UserId.ToString()),
                    new Claim("Role",user.RoleName),
                    new Claim("Email",user.Email)
                });

            // get the bytes from the appsettings secret key
            var keyBytes = Encoding.UTF8.GetBytes(_appSettings.AppSecret);

            //define the signing credentials for jwt - header
            var signingCredentials = new SigningCredentials(
                key: new SymmetricSecurityKey(keyBytes),
                algorithm: _appSettings.Algorithm
                );

            //define the securitydescriptor - payload
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = identity, //sub
                Expires = DateTime.UtcNow.AddDays(1), //exp
                SigningCredentials = signingCredentials //header
            };
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var preToken = tokenHandler.CreateToken(tokenDescriptor); // signature
            var writeableToken = tokenHandler.WriteToken(preToken);// generates a base64 writeable token
            var authResponse = new AuthenticationResponseModel(user, writeableToken);
            return Ok(authResponse);
        }
            
    }
}
