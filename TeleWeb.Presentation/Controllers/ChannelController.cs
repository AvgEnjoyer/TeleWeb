using FluentValidation;
using Microsoft.AspNetCore.Authorization;
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
    private readonly IValidator<ChannelDTO> _channelValidator;

    public ChannelController(IChannelService channelService, IValidator<ChannelDTO> channelValidator)
    {
        _channelService = channelService;
        _channelValidator = channelValidator;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ChannelDTO>>> GetAllChannels()
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
    public async Task<ActionResult<IEnumerable<ChannelDTO>>> GetChannelById(Guid id)
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
    public async Task<ActionResult> CreateChannel(ChannelDTO channelDTO)
    {
        var validationResult = await _channelValidator.ValidateAsync(channelDTO);
        if(!validationResult.IsValid)
            return BadRequest(validationResult.Errors);
        try
        {
            await _channelService.CreateAsync(channelDTO);
            return Ok();
        }
        catch (Exception exception)
        {
            return ExceptionResult(exception);
        }
    }
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateChannel(Guid id, ChannelDTO channelDTO)
    {
        try
        {
            await _channelService.UpdateAsync(id, channelDTO);
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
            await _channelService.DeleteAsync(id);
            return Ok();
        }
        catch (Exception exception)
        {
            return ExceptionResult(exception);
        }
    }
}