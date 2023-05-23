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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsers()
        {
            try
            {
                var userDTOs=await _userService.GetAllAsync();
                return Ok(userDTOs);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUserById(Guid id)
        {
            try
            {
                var userDTO = await _userService.GetByIdAsync(id);
                return Ok(userDTO);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> CreateUser(UserDTO userDTO)
        {
            try
            {
                await _userService.CreateAsync(userDTO);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserDTO userDTO)
        {
            try
            {
                await _userService.UpdateAsync(id, userDTO);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                await _userService.DeleteAsync(id);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [Authorize(policy: "AuthorizedUser")]
        [HttpGet("me")]
        public async Task GetMeAsync()
        {
            //TODO: Get user id from token
            
        }

    }
    }
