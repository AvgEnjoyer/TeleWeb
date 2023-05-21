using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TeleWeb.Application.DTOs;
using TeleWeb.Application.Services.Interfaces;

namespace TeleWeb.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostController : ControllerBase
{
    private readonly IPostService _postService;
    private readonly IValidator<PostDTO> _postValidator;

    public PostController(IPostService postService, IValidator<PostDTO> postValidator)
    {
        _postService = postService;
        _postValidator = postValidator;
    }
    [HttpGet("{channelId}")]
    public async Task<ActionResult<IEnumerable<PostDTO>>> GetAllPostsFromChannel(int channelId)
    {
        try
        {
            var postDTOs = await _postService.GetAllFromChannelAsync(channelId);
            return Ok(postDTOs);
        }
        catch (Exception exception)
        {
            return ExceptionResult(exception);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<PostDTO>>> GetPostById(int id)
    {
        try
        {
            var postDTO = await _postService.GetByIdAsync(id);
            return Ok(postDTO);
        }
        catch (Exception exception)
        {
            return ExceptionResult(exception);
        }
    }
    [HttpPost]
    public async Task<ActionResult> CreatePost(PostDTO postDTO, int channelID)
    {
        var validationResult = await _postValidator.ValidateAsync(postDTO);
        if(!validationResult.IsValid)
            return BadRequest(validationResult.Errors);
        try
        {
            await _postService.CreateAsync(postDTO, channelID);
            return Ok();
        }
        catch (Exception exception)
        {
            return ExceptionResult(exception);
        }
    }
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdatePost(int id, PostDTO postDTO)
    {
        try
        {
            await _postService.UpdateAsync(id, postDTO);
            return Ok();
        }
        catch (Exception exception)
        {
            return ExceptionResult(exception);
        }
    }

    [HttpDelete("{id}")] 
    public async Task<ActionResult> DeletePost(int id)
    {
        try
        {
            await _postService.DeleteAsync(id);
            return Ok();
        }
        catch (Exception exception)
        {
            return ExceptionResult(exception);
        }
    }
}