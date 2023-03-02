using TeleWeb.Domain.Models;
using TeleWeb.Application.DTOs;
using TeleWeb.Application.Services.Interfaces;

using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace TeleWeb.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<UserIdentity> _userManager;
        private readonly SignInManager<UserIdentity> _signInManager;
        private readonly IConfiguration _configuration;
       
        
        public AccountService(SignInManager<UserIdentity> signInManager, UserManager<UserIdentity> userManager, IConfiguration configuration)
        {

            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        
        public async Task<IdentityResult> RegisterUserAsync(AccountRegisterDTO model)
        {
            var user = new UserIdentity { UserName = model.Username, Email = model.Email};
            var result = await _userManager.CreateAsync(user, model.Password);

            return result;
        }
        public async Task<string?> LoginUserAsync(AccountLoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.UserNameOrEmail) ?? await _userManager.FindByNameAsync(model.UserNameOrEmail);

            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserNameOrEmail, model.Password, true, false);

                if (result.Succeeded)
                {
                    var token = GenerateJwtToken(user);
                    return token;
                }
            }
            return null;
        }

        private string GenerateJwtToken(UserIdentity user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"] ?? "");

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
