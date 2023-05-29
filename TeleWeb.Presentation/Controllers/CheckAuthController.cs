using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TeleWeb.Presentation;

namespace TeleWeb.Controllers
{
    [ApiController]
    [Route("api/IsAuthorized/")]
    
    public class CheckAuthController : ControllerBase
    {
           [HttpGet]
           [Authorize(Roles = "AuthorizedUser")]
        public async Task<IActionResult> IsAuthorized()
        {
            return Ok(new ApiResponse()
                {
                    Success = true,
                    Message = "Is Authorized"
                });
        }
    }
}