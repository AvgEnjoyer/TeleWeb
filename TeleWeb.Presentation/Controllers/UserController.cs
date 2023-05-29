using Microsoft.AspNetCore.Mvc;
using TeleWeb.Application.Services.Interfaces;
using TeleWeb.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace TeleWeb.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("subscriptions")]

        [Authorize(Roles="AuthorizedUser")]
        public async Task<ActionResult<IEnumerable<GetChannelDTO>>> GetMySubscriptions()
        {
            try
            {
                var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var subscriptions = await _userService.GetUserSubscriptionsByIdentityAsync(userId);
                var response = new ApiResponse<IEnumerable<GetChannelDTO>>()
                {
                    Success = true,
                    Data = subscriptions,
                    Message = "Subscriptions retrieved successfully"
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex);
            }
        }
    }
}
