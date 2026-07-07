using Microsoft.AspNetCore.Authentication; 
using Microsoft.AspNetCore.Http;
using PORTIMAGES.Application.Auth.AuthEmployee.DTOs;
using PORTIMAGES.Application.Auth.AuthEmployee.Interfaces;
using System.Security.Claims;

namespace PORTIMAGES.Infrastructure.Repositories.Auth.AuthEmployee
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public AuthRepository(IHttpContextAccessor accessor)
        {
            _contextAccessor = accessor;
        }

        public async Task SignInAsync(LoginResultDTO user)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier,Convert.ToString(user.ID)),
            new Claim(ClaimTypes.Name, user.Name ?? ""),
            new Claim(ClaimTypes.Email, user.Email ?? ""),
            new Claim(ClaimTypes.Role, user.RoleName ?? ""),
            new Claim("ProfileImage", user.Profile ?? "")
        };

            var identity = new ClaimsIdentity(claims, user.Scheme); 
            var principal = new ClaimsPrincipal(identity);

            await _contextAccessor.HttpContext!.SignInAsync(user.Scheme,principal);
        }

        public async Task SignOutAsync(string Scheme)
        {
            await _contextAccessor.HttpContext!.SignOutAsync(Scheme);
        }
    }
}
