using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AspCoreAuthenticatAuthorizeProjectExample.Models;
using AspCoreAuthenticatAuthorizeProjectExample.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspCoreAuthenticatAuthorizeProjectExample.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;
        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = "/")
        {
            return View( new LoginModel {  ReturnUrl = returnUrl });
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = _userRepository.GetBYUserNameAndPassword(model.UserName , model.Password);
            if (user == null) Unauthorized();
            var claims = new List<Claim> 
            { 
                new Claim(ClaimTypes.NameIdentifier , user.UserId.ToString()),
                new Claim( ClaimTypes.Name , user.UserName ),
                new Claim(ClaimTypes.Role , user.Role),
                new Claim("FavoriteColor" , user.FavColor)
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties { IsPersistent = model.RememeberLogin} );
            return LocalRedirect(model.ReturnUrl);
            
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }
    
        [AllowAnonymous]
        public IActionResult LoginWithGoogle(string returnUrl = "/")
        {
            var props = new AuthenticationProperties 
            { 
                RedirectUri = Url.Action("GoogleLoginCallback"),
                Items = { { "returnUrl", returnUrl } }
            };
            return Challenge(props , GoogleDefaults.AuthenticationScheme);
        }
        [AllowAnonymous]
        public async Task<IActionResult> GoogleLoginCallback()
        {
            var result = await HttpContext.AuthenticateAsync(
                ExternalAuthenticationDefaults.AuthenticationScheme );

            var externalClaims = result.Principal.Claims.ToList();

            var subjectIdClaim = externalClaims.FirstOrDefault(
                x => x.Type == ClaimTypes.NameIdentifier );
            var subval = subjectIdClaim.Value;
            var user = _userRepository.GetByGoogleId(subval);

            var claims = new List<Claim>{

                new Claim(ClaimTypes.NameIdentifier , user.UserId.ToString()),
                new Claim( ClaimTypes.Name , user.UserName ),
                new Claim(ClaimTypes.Role , user.Role),
                new Claim("FavoriteColor" , user.FavColor)
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignOutAsync(ExternalAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return LocalRedirect(result.Properties.Items["returnUrl"]);
        }
    }
}
