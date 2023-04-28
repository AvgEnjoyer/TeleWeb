using TeleWeb.Application.DTOs;

namespace TeleWeb.Application.Services.Interfaces
{
    public interface IChannelService
    {
        public Task<IEnumerable<UserDTO>> GetAllAsync();
        public Task<UserDTO> GetByIdAsync(int id);
        public Task CreateAsync(ChannelDTO channelDTO);
        public Task UpdateAsync(int id, ChannelDTO channelDTO);
        public Task DeleteAsync(int id);
    }
}