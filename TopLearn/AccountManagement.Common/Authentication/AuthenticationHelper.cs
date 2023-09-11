using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace AccountManagement.Common.Authentication
{
    public class AuthenticationHelper : IAuthenticationHelper
    {
        #region constructor injection

        private readonly IHttpContextAccessor _contextAccessor;
        public AuthenticationHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        #endregion

        public void Signin(AuthenticationViewModel account)
        {
            var claims = new List<Claim>
            {
                new Claim("AccountId", account.Id.ToString()),
                new Claim(ClaimTypes.Name, account.Fullname),
                new Claim("Username", account.Username),
                new Claim(ClaimTypes.Email,account.Email)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(10),
                IsPersistent = account.RememberMe
            };

            _contextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

        }

        public void SignOut()
        {
            _contextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public bool IsAuthenticated()
        {
            return _contextAccessor.HttpContext.User.Identity is { IsAuthenticated: true };
        }

        public long CurrentAccountId()
        {
            return IsAuthenticated()
                ? long.Parse(_contextAccessor.HttpContext.User.Claims.First(x => x.Type == "AccountId").Value)
                : 0;
        }

        public AuthenticationViewModel CurrentAccountInfo()
        {
            var result = new AuthenticationViewModel();
            if (!IsAuthenticated())
            {
                return result;
            }

            var claims = _contextAccessor.HttpContext.User.Claims.ToList();
            result.Id = long.Parse(claims.First(x => x.Type == "AccountId").Value);
            result.Username = claims.First(x => x.Type == "Username").Value;
            result.Fullname = claims.First(x => x.Type == ClaimTypes.Name).Value;
            return result;
        }
    }
}