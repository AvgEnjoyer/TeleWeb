using TeleWeb.Application.DTOs;

namespace TeleWeb.Application.Services.Interfaces;

public interface IPostService
{
    public Task<IEnumerable<PostDTO>> GetAllFromChannelAsync(Guid channelId);
    public Task<PostDTO> GetByIdAsync(Guid id);
    public Task CreateAsync(PostDTO postDTO, Guid channelId);
    public Task UpdateAsync(Guid id, PostDTO postDTO);
    public Task DeleteAsync(Guid id);
}