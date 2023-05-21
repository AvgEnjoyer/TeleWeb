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
    public class  AccountService : IAccountService
    {
        private readonly UserManager<UserIdentity> _userManager;
        private readonly SignInManager<UserIdentity> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public AccountService(SignInManager<UserIdentity> signInManager,
            UserManager<UserIdentity> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {

            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
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
            
            var user = await _userManager.FindByEmailAsync(model.UserNameOrEmail) 
                       ?? await _userManager.FindByNameAsync(model.UserNameOrEmail);
            

            if (user != null)
            {
                
                var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, true, false);

                if (result.Succeeded)
                {
                    await _roleManager.CreateAsync(new IdentityRole("AuthorizedUser"));//TODO: move to seed
                    
                    await _userManager.AddToRoleAsync(user, "AuthorizedUser");
                    return true;
                }
            }
            return false;
        }

        

        
        public async Task LogOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
