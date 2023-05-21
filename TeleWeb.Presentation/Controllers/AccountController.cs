using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using TeleWeb.Application.DTOs;
using TeleWeb.Application.Services.Interfaces;

namespace TeleWeb.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        
        [HttpPost("register")]
        public async Task<IActionResult> Register(AccountRegisterDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _accountService.RegisterUserAsync(model);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return BadRequest(ModelState);
            }

            return Ok();
        }
        
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] AccountLoginDTO model)
        {
            
            if (HttpContext.User.Identities.Any(i=>i.HasClaim(c=>c.Value=="AuthorizedUser"))) //.IsInRole("AuthorizedUser"))
            {
                return BadRequest("You are already logged in");
            }

            return await _accountService.LoginUserAsync(model) 
                ? Ok() 
                : BadRequest("Invalid credentials");
        }
        [HttpPost("logout")]
        public async Task SignOut()
        {
             await _accountService.LogOutAsync();
        }
    }
}
