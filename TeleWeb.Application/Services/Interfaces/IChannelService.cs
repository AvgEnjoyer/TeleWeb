using Microsoft.AspNetCore.Http;
using TeleWeb.Application.DTOs;

namespace TeleWeb.Application.Services.Interfaces
{
    public interface IChannelService : IService
    {
        public Task<IEnumerable<GetChannelDTO>> GetAllAsync();
        public Task<GetChannelDTO> GetByIdAsync(Guid id);
        public Task CreateAsync(UpdateChannelDTO channelDTO, string userId);
        public Task CreatePostAsync(UpdatePostDTO postDTO, Guid channelId, string userId);
        public Task UpdateAsync(Guid id, UpdateChannelDTO channelDTO, string userId);
        public Task DeleteAsync(Guid id, string userId);
        public Task SubscribeAsync(Guid channelId, string userId);
        public Task DeletePostAsync(Guid postId, string userId);
    }
}