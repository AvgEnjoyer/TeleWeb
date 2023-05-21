using TeleWeb.Application.DTOs;

namespace TeleWeb.Application.Services.Interfaces;

public interface IPostService
{
    public Task<IEnumerable<PostDTO>> GetAllFromChannelAsync(int channelId);
    public Task<PostDTO> GetByIdAsync(int id);
    public Task CreateAsync(PostDTO postDTO, int channelId);
    public Task UpdateAsync(int id, PostDTO postDTO);
    public Task DeleteAsync(int id);
}