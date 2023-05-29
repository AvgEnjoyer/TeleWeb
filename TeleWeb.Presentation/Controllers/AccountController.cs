using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TeleWeb.Application.DTOs;
using TeleWeb.Application.Services.Interfaces;

namespace TeleWeb.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
            //return Ok();
            try
            {
                var result = await _accountService.RegisterUserAsync(model);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                    var badResponse = new ApiResponse<ModelStateDictionary>()
                    {
                        Success = false,
                        Message = "User creation failed! Please check user details and try again.",
                        Data = ModelState
                    };
                    return BadRequest(badResponse);
                }

                var response = new ApiResponse()
                {
                    Success = true,
                    Message = "User created successfully! Please check your email to confirm your account."
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                return ExceptionResult(e);
            }
        }

        [HttpGet("confirm")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            try
            {
                await _accountService.ConfirmEmailAsync(userId, token);
                var response = new ApiResponse()
                {
                    Success = true,
                    Message = "Email confirmed successfully!"
                };
                return Ok(response);

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
                var response = new ApiResponse()
                {
                    Success = true,
                    Message = "Check your email for a password reset link."
                };
                return Ok(response);

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
                var response = new ApiResponse()
                {
                    Success = true,
                    Message = "Password reset successfully! Email is also confirmed."
                };
                return Ok(response);

            }
            catch (Exception e)
            {
                return ExceptionResult(e);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AccountLoginDTO model)
        {
            try
            {
                await _accountService.LoginUserAsync(model);
                var response = new ApiResponse()
                {
                    Success = true,
                    Message = "Login successful!"
                };
                return Ok(response);

            }
            catch (Exception e)
            {
                return ExceptionResult(e);
            }
        }

        [HttpPost("logout")]
            public async Task<IActionResult> SignOut()
            {
                try
                {
                var response = new ApiResponse()
                {
                    Success = true,
                    Message = "Logout successful!"
                };
                await _accountService.LogOutAsync();
                return Ok(response);

                }
                catch (Exception e)
                {   
                    return ExceptionResult(e);
                }
            }
        
    }
}
