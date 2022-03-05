using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace NSE.WebApp.MVC.Extensions
{
    public interface IUser
    {
        string UserName { get; } 
        Guid ObterUserId();
        string ObterUserEmail();
        string ObterUserToken();
        bool EstaAutenticado();
        bool PossuiRole(string role);
        IEnumerable<Claim> ObterClaims();
        HttpContext ObterHttpContext();
    }

    public class NetUser : IUser
    {
        private readonly IHttpContextAccessor _accessor;
        public NetUser(IHttpContextAccessor httpContextAccessor)
        {
            _accessor = httpContextAccessor;
        }

        public string UserName => _accessor.HttpContext.User.Identity.Name;

        public Guid ObterUserId()
        {
            return EstaAutenticado() ?
                Guid.Parse(_accessor.HttpContext.User.GetUserId()) : Guid.Empty;
        }
        public string ObterUserEmail()
        {
            return EstaAutenticado() ?
                _accessor.HttpContext.User.GetUserEmail() : "";
        }
        public string ObterUserToken()
        {
            return EstaAutenticado() ?
                _accessor.HttpContext.User.GetUserToken() : "";
        }

        public bool EstaAutenticado()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }
        public bool PossuiRole(string role)
        {
            return _accessor.HttpContext.User.IsInRole(role);
        }
        public IEnumerable<Claim> ObterClaims()
        {
            return _accessor.HttpContext.User.Claims;
        }
        public HttpContext ObterHttpContext()
        {
            return _accessor.HttpContext;
        }
    }
    public static class ClaimPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal principal) { 
            if(principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }
            var claim = principal.FindFirst("sub");
            return claim?.Value;
        }
        public static string GetUserEmail(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }
            var claim = principal.FindFirst("email");
            return claim?.Value;
        }

        public static string GetUserToken(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }
            var claim = principal.FindFirst("JWT");
            return claim?.Value;
        }
    }
}
