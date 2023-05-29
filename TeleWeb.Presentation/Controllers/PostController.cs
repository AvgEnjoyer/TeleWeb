using System.Security.Claims;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeleWeb.Application.DTOs;
using TeleWeb.Application.Services.Interfaces;

namespace TeleWeb.Presentation.Controllers;

[Route("api/Channel/")]
[ApiController]
public class PostController : ControllerBase
{
    private readonly IPostService _postService;
    private readonly IValidator<UpdatePostDTO> _postValidator;

    public PostController(IPostService postService, IValidator<UpdatePostDTO> postValidator)
    {
        _postService = postService;
        _postValidator = postValidator;
    }
    [HttpPost("{channelId}/post/")]
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
                Message = "Post created successfully",
                Data = getPostDTO
            };
            return Ok(response);
        }
        catch (Exception exception)
        {
            return ExceptionResult(exception);
        }
    }
    [HttpPut("/api/Post/{postId}")]
    [Authorize(Roles = "AuthorizedUser")]
    public async Task<ActionResult> UpdatePost( [FromBody]UpdatePostDTO postDTO, Guid postId)//TODO:
    {
        
        try
        {
            //var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            //await _postService.UpdatePostAsync(postDTO, postId, userId);
            return Ok();
        }
        catch (Exception exception)
        {
            return ExceptionResult(exception);
        }
    }
    
    [HttpDelete("/api/Post/{postId}")]
    [Authorize(Roles = "AuthorizedUser")]
    public async Task<ActionResult> DeletePost( Guid postId)//TODO:
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
    [HttpGet("{channelId}/post/")]
    public async Task<ActionResult> GetPostsByChannel(Guid channelId)
    {
        try
        {
            var postDTOs = await _postService.GetPostsByChannelAsync(channelId);
            var response = new ApiResponse<IEnumerable<GetPostDTO>>()
            {
                Data = postDTOs
            };
            return Ok(response);
        }
        catch (Exception exception)
        {
            return ExceptionResult(exception);
        }
    }
}