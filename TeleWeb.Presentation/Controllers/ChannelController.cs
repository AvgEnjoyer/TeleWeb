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
    private readonly IValidator<GetChannelDTO> _channelValidator;

    public ChannelController(IChannelService channelService, IValidator<GetChannelDTO> channelValidator)
    {
        _channelService = channelService;
        _channelValidator = channelValidator;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetChannelDTO>>> GetAllChannels()//TODO
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
    public async Task<ActionResult<IEnumerable<GetChannelDTO>>> GetChannelById(Guid id) //TODO
    {
        try
        {
            var channelDTO = await _channelService.GetByIdAsync(id);
            return Ok(channelDTO);
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
            await _channelService.CreateAsync(channelDTO, userId);
            return Ok();
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
            await _channelService.CreatePostAsync(postDTO, channelId, userId);
            return Ok();
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
            await _channelService.DeletePostAsync(postId, userId);
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