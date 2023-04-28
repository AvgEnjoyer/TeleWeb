using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TeleWeb.Application.DTOs;
using TeleWeb.Application.Services.Interfaces;
using TeleWeb.Data.Repositories.Interfaces;
using TeleWeb.Domain.Models;

namespace TeleWeb.Application.Services
{
    public class ChannelService : IChannelService
    {
        private readonly IChannelRepository _channelRepository;
        private readonly IMapper _mapper;

        public ChannelService(IChannelRepository channelRepository, IMapper mapper)
        {
            _channelRepository = channelRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(ChannelDTO channelDTO)
        {
            var channel = _mapper.Map<Channel>(channelDTO);
            await _channelRepository.CreateAsync(channel);
            await _channelRepository.SaveRepoChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var channel = await _channelRepository.FindByCondition(x => x.Id == id, true).FirstOrDefaultAsync();
            if(channel!=null)
                await _channelRepository.DeleteAsync(channel);
        }

        public Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<UserDTO> GetByIdAsync(int id)
        {
            var channel = await _channelRepository.FindByCondition(x => x.Id == id, true).FirstOrDefaultAsync();
            return _mapper.Map<UserDTO>(channel);
        }

        public Task UpdateAsync(int id, ChannelDTO channelDTO)
        {
            throw new NotImplementedException();
        }
    }
}
