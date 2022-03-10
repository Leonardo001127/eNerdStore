using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;
using NSE.WebApp.MVC.Services;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NSE.WebApp.MVC.Controllers
{
    public class IdentidadeController : MainController
    {
        private readonly IAuthService _authService; 

        public IdentidadeController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpGet]
        [Route("nova-conta")]
        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        [Route("nova-conta")]
        public async Task<IActionResult> Registro(UsuarioRegistro user)
        {
            if (!ModelState.IsValid) return View(user);


            var result = await _authService.Registro(user);

            if (ResponseHasErrors(result.ResponseResult))
            {
                return View(user);
            }

            await RealizarLogin(result);

            return RedirectToAction("Index", "Catalogo");
        }

        [HttpGet]
        [Route("Login")]
        public IActionResult Login(string ReturnUrl= null)
        {
            ViewData["ReturnUrl"] = ReturnUrl;
            return View();
        }
         
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(UsuarioLogin user, string ReturnUrl = null)
        {
            ViewData["ReturnUrl"] = ReturnUrl;
            if (!ModelState.IsValid) return View(user);

            var result = await _authService.Login(user);

            if (ResponseHasErrors(result.ResponseResult))
            {
                return View(user);
            }

            await RealizarLogin(result);

            if(string.IsNullOrEmpty(ReturnUrl))
                return RedirectToAction("Index", "Catalogo");
            else
                return LocalRedirect(ReturnUrl);
        }

        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Catalogo");
        }


        private async Task RealizarLogin(UsuarioRespostaLogin resp)
        {
            var token = ObterTokenFormatado(resp.accessToken);

            var claims = new List<Claim>();
            claims.Add(new Claim("JWT", resp.accessToken));
            claims.AddRange(token.Claims); 
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = System.DateTimeOffset.UtcNow.AddHours(1),
                IsPersistent = true
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), 
                authProperties);

        }

        private static JwtSecurityToken ObterTokenFormatado(string jwt)
        {
            return new JwtSecurityTokenHandler().ReadJwtToken(jwt) as JwtSecurityToken;
        }
    }
}
