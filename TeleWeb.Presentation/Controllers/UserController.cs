using Microsoft.AspNetCore.Mvc;
using TeleWeb.Application.Services.Interfaces;
using TeleWeb.Domain.Models;
using TeleWeb.Application.DTOs;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics.Metrics;
using System.Security.Policy;
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
        public async Task<ActionResult<UserDTO>> GetUserById(int id)
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
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDTO userDTO)
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
        public async Task<IActionResult> DeleteUser(int id)
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
        public async Task<ActionResult<UserDTO>> GetMeAsync()
        {
            try
            {
                // Get the current user's ID
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                // Use the ID to retrieve the user from the database
                var userDTO = await _userService.GetByIdAsync(Convert.ToInt32(userId));
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

    }
    }
