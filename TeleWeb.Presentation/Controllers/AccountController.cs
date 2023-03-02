using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TeleWeb.Application.DTOs;
using TeleWeb.Application.Services.Interfaces;
using TeleWeb.Domain.Models;

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
        public async Task<IActionResult> Login(AccountLoginDTO model)
        {
            var token = await _accountService.LoginUserAsync(model);

            if (token != null)
            {
                return Ok(new { Token = token });
            }

            return BadRequest("Invalid credentials");
        }
        [HttpPost("logout")]
        public async Task<IActionResult> SignOut(string? returnUrl = null)
        {
            await _accountService.LogOutAsync();
            return LocalRedirect(returnUrl ?? "/");
        }
    }
}
