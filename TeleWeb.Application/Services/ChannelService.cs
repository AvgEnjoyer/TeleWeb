using TeleWeb.Domain.Models;
using TeleWeb.Application.DTOs;
using TeleWeb.Application.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TeleWeb.Data.Repositories.Interfaces;

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

        public async Task CreateAsync(ChannelDTO channelDto)
        {
            var channel = _mapper.Map<Channel>(channelDto);
            await _channelRepository.CreateAsync(channel);
            await _channelRepository.SaveRepoChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var channel = await _channelRepository.FindByCondition(x => x.Id == id, true).FirstOrDefaultAsync();
            if(channel!=null)
                await _channelRepository.DeleteAsync(channel);
        }

        public async Task<IEnumerable<ChannelDTO>> GetAllAsync()
        {
            var channels = await _channelRepository.FindAll(false).ToListAsync();
            return _mapper.Map<IEnumerable<ChannelDTO>>(channels);
        }

        public async Task<ChannelDTO> GetByIdAsync(Guid id)
        {
            var channel = await _channelRepository.FindByCondition(x => x.Id == id, false)
                .FirstOrDefaultAsync();
            return _mapper.Map<ChannelDTO>(channel);
        }

        public async Task UpdateAsync(Guid id, ChannelDTO channelDto)
        {
            var channelToUpdate = await _channelRepository.FindByCondition(x=>x.Id==id, true).FirstOrDefaultAsync();
            if (channelToUpdate == null) throw new Exception();
            _mapper.Map(channelDto, channelToUpdate);
            await _channelRepository.UpdateAsync(channelToUpdate);
        }
    }
}
