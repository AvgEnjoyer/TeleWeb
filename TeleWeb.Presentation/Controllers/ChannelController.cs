using System.Security.Claims;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TeleWeb.Application.DTOs;
using TeleWeb.Application.Services.Interfaces;
using TeleWeb.Validation;

namespace TeleWeb.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChannelController : ControllerBase
{
    private readonly IChannelService _channelService;
    private readonly IPostService _postService;

    public ChannelController(IChannelService channelService,
        IPostService postService)
    {
        _channelService = channelService;
        _postService = postService;
    }
    [HttpGet]
    public async Task<ActionResult> GetAllChannels()//TODO
    {
        try
        {
            var channelDTOs = await _channelService.GetAllAsync();
            return Ok(channelDTOs);
        }
        catch (Exception exception)
        {
            return ExceptionResult(exception);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetChannelById(Guid id) //TODO
    {
        try
        {
            var channelDTO = await _channelService.GetByIdAsync(id);
            var response = new ApiResponse<GetChannelDTO>()
            {
                Success = true,
                Data = channelDTO
            };
            return Ok(response);
        }
        catch (Exception exception)
        {
            return ExceptionResult(exception);
        }
    }
    [HttpGet("{channelId}/isAdmin")]
    public async Task<ActionResult> isAdmin(Guid channelId) //TODO
    {
        try
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            _channelService.IsAdminAsync(channelId, userId);
            var response = new ApiResponse()
            {
                Success = true,
                Message = "User is admin"
            };
            return Ok(response);
        }
        catch (Exception exception)
        {
            return ExceptionResult(exception);
        }
    }
    [HttpPost]
    [Authorize(Roles = "AuthorizedUser")]
    public async Task<ActionResult> CreateChannel([FromBody]UpdateChannelDTO channelDTO)
    {
        // var validationResult = await _channelValidator.ValidateAsync(channelDTO);
        // if(!validationResult.IsValid)
        //     return BadRequest(validationResult.Errors);
        try
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var getChannelDTO = await _channelService.CreateAsync(channelDTO, userId);
            var response = new ApiResponse<GetChannelDTO>()
            {
                Success = true,
                Data = getChannelDTO,
                Message = "Channel created successfully"
            };
            return Ok(response);
        }
        catch (Exception exception)
        {
            return ExceptionResult(exception);
        }
    }
    [HttpPost("post")]
    [Authorize(Roles = "AuthorizedUser")]
    public async Task<ActionResult> CreatePost([FromBody] UpdatePostDTO postDTO, Guid channelId)
    {
        /*var validationResult = await _channelValidator.ValidateAsync(channelDTO);
        if(!validationResult.IsValid)
            return BadRequest(validationResult.Errors);*/
        try
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var getPostDTO = await _postService.CreatePostAsync(postDTO, channelId, userId);
            var response = new ApiResponse<GetPostDTO>()
            {
                Success = true,
                Data = getPostDTO,
                Message = "Post created successfully"
            };
            return Ok(response);
        }
        catch (Exception exception)
        {
            return ExceptionResult(exception);
        }
    }
    [HttpDelete("post/{postId}")]
    [Authorize(Roles = "AuthorizedUser")]
    public async Task<ActionResult> DeletePost( Guid postId)
    {
        /*var validationResult = await _channelValidator.ValidateAsync(channelDTO);
        if(!validationResult.IsValid)
            return BadRequest(validationResult.Errors);*/
        try
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _postService.DeletePostAsync(postId, userId);
            return Ok();
        }
        catch (Exception exception)
        {
            return ExceptionResult(exception);
        }
    }
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateChannel(Guid id, UpdateChannelDTO channelDTO) //TODO
    {
        try
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _channelService.UpdateAsync(id, channelDTO, userId);
            return Ok();
        }
        catch (Exception exception)
        {
            return ExceptionResult(exception);
        }
    }
    [HttpPost("subscribe")]
    [Authorize(Roles = "AuthorizedUser")]
    public async Task<ActionResult> SubscribeToChannel(Guid channelId) 
    {
        try
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _channelService.SubscribeAsync(channelId, userId);
            return Ok();
        }
        catch (Exception exception)
        {
            return ExceptionResult(exception);
        }
    } 

    [HttpDelete("{id}")] 
    public async Task<ActionResult> DeleteChannel(Guid id)
    {
        try
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _channelService.DeleteAsync(id, userId);
            return Ok();
        }
        catch (Exception exception)
        {
            return ExceptionResult(exception);
        }
    }
}