using TeleWeb.Domain.Models;
using TeleWeb.Application.DTOs;
using TeleWeb.Application.Services.Interfaces;

using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace TeleWeb.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<UserIdentity> _userManager;
        private readonly SignInManager<UserIdentity> _signInManager;
        private readonly IConfiguration _configuration;

        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public AccountService(SignInManager<UserIdentity> signInManager, UserManager<UserIdentity> userManager,
            IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {

            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        
        public async Task<IdentityResult> RegisterUserAsync(AccountRegisterDTO model)
        {
            var user = new UserIdentity { UserName = model.Username, Email = model.Email};
            var result = await _userManager.CreateAsync(user, model.Password);

            return result;
        }
        
        public async Task<bool> LoginUserAsync(AccountLoginDTO model)
        {
            
            var user = await _userManager.FindByEmailAsync(model.UserNameOrEmail) ?? await _userManager.FindByNameAsync(model.UserNameOrEmail);

            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, true, false);

                if (result.Succeeded)
                {
                    //var token = GenerateJwtToken(user);
                    //return token;
                    await GenerateCookieAsync(user);
                    return true;
                }
            }
            return false;
        }

        private async Task GenerateCookieAsync(UserIdentity user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Role, "AuthorizedUser")};

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true
            };

            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
            string cookieValue = _httpContextAccessor.HttpContext.Request.Cookies[".AspNetCore.Cookies"];
            return;
        }

        private string GenerateJwtToken(UserIdentity user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JWT:SecretKey"] ?? "");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Role, "AuthorizedUser"),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    // other claims
                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task LogOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
