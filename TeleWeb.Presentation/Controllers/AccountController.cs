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

        [HttpGet("confirm")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            try
            {
                await _accountService.ConfirmEmailAsync(userId, token);
                return Ok("Email confirmed successfully!");

            }
            catch (Exception e)
            {
                return ExceptionResult(e);
            }
        }
        [HttpGet("forgot")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            try
            {
                await _accountService.ForgotPasswordAsync(email);
                return Ok("Email confirmed successfully!");

            }
            catch (Exception e)
            {
                return ExceptionResult(e);
            }
        }
        [HttpGet("reset")]
        public async Task<IActionResult> ResetPassword(string userId, string token, string newPassword)
        {
            try
            {
                await _accountService.ResetPasswordAsync(userId, token, newPassword);
                return Ok("Email confirmed successfully!");

            }
            catch (Exception e)
            {
                return ExceptionResult(e);
            }
        }

            [HttpPost("login")]
            public async Task<IActionResult> Login([FromBody] AccountLoginDTO model)
            {
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
