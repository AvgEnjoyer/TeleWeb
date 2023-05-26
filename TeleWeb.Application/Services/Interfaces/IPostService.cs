using Microsoft.AspNetCore.Http;
using TeleWeb.Application.DTOs;

namespace TeleWeb.Application.Services.Interfaces;

public interface IPostService : IService
{
    public Task CreatePostAsync(UpdatePostDTO postDTO, Guid channelId, string userId);
    public Task DeletePostAsync(Guid postId, string userId);
}