using System.Collections;
using Microsoft.AspNetCore.Http;
using TeleWeb.Application.DTOs;

namespace TeleWeb.Application.Services.Interfaces;

public interface IPostService : IService
{
    public Task<GetPostDTO> CreatePostAsync(UpdatePostDTO postDTO, Guid channelId, string userId);
    public Task DeletePostAsync(Guid postId, string userId);
    Task<IEnumerable<GetPostDTO>> GetPostsByChannelAsync(Guid channelId);
}