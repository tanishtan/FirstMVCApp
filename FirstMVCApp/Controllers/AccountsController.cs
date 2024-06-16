using FirstMVCApp.Infrastructure;
using FirstMVCApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FirstMVCApp.Controllers
{
    public class AccountsController : Controller
    {
        IUserService _authService;
        public AccountsController(IUserService authService) => _authService = authService;

        
        [HttpGet]   
        public IActionResult SignIn(string returnUrl="")
        {
            var model = new LoginViewModel();
            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(LoginViewModel model, string returnUrl="")
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            //Perform the validations here. Preferably create a UserService to validate the user
            //based on the return values of the Authenticate Method, perform the next steps.

            var status = _authService.Authenticate(model);
            if(!status)
            {
                ModelState.AddModelError("", "Bad username/password");
                return View(model);
            }

            //Build the claim set, create the identity and the Principal and SignIn into the HttpPipeline
            //Generate the output cookie. Asp .net checks the cookie for it's presence. If present, the user is 
            //treated as authnticated and authorization works fine, else the user is forced to authenticate.

            var listOfClaims = new List<Claim>
            {
                new Claim("Username", model.Username),
                new Claim("Password", model.Password)
            };
            var cookieAuthDefaults = CookieAuthenticationDefaults.AuthenticationScheme;
            var identity = new ClaimsIdentity(claims: listOfClaims, authenticationType: cookieAuthDefaults);
            AuthenticationProperties authProp = new AuthenticationProperties
            {
                IsPersistent = false
            };

            var principal = new ClaimsPrincipal(identity: identity);
            //SignIn - takes the principal and identitfy - encrypts it, creates a cookie and assigns the
            //encrypted value as a cookie value, attahes the cookie to the response stream.

            Response.Cookies.Append("name", "encrypted value");
            try
            {
                await HttpContext.SignInAsync(principal: principal, scheme: cookieAuthDefaults, properties: authProp);
                HttpContext.Session.SetString("Username", model.Username);
            }
            catch
            {

            }


            returnUrl = string.IsNullOrEmpty(returnUrl) ? "/" : returnUrl;
            return Redirect(returnUrl);
        }
        public async Task<IActionResult> Signout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("Username");
            HttpContext.Session.Clear();
            return Redirect("/");
        }
    }
}
