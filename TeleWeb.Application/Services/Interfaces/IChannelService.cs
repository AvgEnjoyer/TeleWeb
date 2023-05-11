using TeleWeb.Application.DTOs;

namespace TeleWeb.Application.Services.Interfaces
{
    public interface IChannelService : IService
    {
        public Task<IEnumerable<ChannelDTO>> GetAllAsync();
        public Task<ChannelDTO> GetByIdAsync(int id);
        public Task CreateAsync(ChannelDTO channelDTO);
        public Task UpdateAsync(int id, ChannelDTO channelDTO);
        public Task DeleteAsync(int id);
    }
}