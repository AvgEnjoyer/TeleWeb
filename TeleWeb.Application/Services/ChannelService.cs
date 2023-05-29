using System.Security.Claims;
using TeleWeb.Domain.Models;
using TeleWeb.Application.DTOs;
using TeleWeb.Application.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TeleWeb.Data.Repositories.Interfaces;

namespace TeleWeb.Application.Services;

public class ChannelService : IChannelService
{
    private readonly IChannelRepository _channelRepository;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public ChannelService(IChannelRepository channelRepository, IMapper mapper,
        IUserRepository userRepository)
    {
        _userRepository = userRepository;
        _channelRepository = channelRepository;
        _mapper = mapper;
    }
    

    protected async Task<Channel> VerifyAdmin(Guid channelId, string userId)
    {
        var channel = await _channelRepository.FindByCondition(x => x.Id == channelId, true).Include(x => x.Admins)
            .FirstOrDefaultAsync();
        if (channel == null)
        {
            throw new ArgumentException("Invalid channel Id.");
        }

        if (channel.Admins.All(x => x.IdentityId.ToString() != userId))
            throw new ArgumentException("You are not admin of this channel.");
        return channel;
    }
    public async Task IsAdminAsync(Guid channelId, string userId)
    {
        var _ = VerifyAdmin(channelId, userId);
    }
    public async Task<GetChannelDTO> CreateAsync(UpdateChannelDTO channelDto, string userId)
    {
        var adminUser = await _userRepository.FindByCondition(x => x.IdentityId == new Guid(userId), true)
            .Include(x => x.AdministratingChannels)
            .Include(x => x.OwnedChannels)
            .Include(x => x.Subscriptions).FirstOrDefaultAsync();
        if (adminUser == null) throw new ApplicationException("Unexpected behaviour. Invalid user Id.");
        var channel = _mapper.Map<Channel>(channelDto);
        await _channelRepository.CreateChannelAsync(channel, adminUser);
        await _channelRepository.SaveRepoChangesAsync();
        return _mapper.Map<GetChannelDTO>(channel);
    }

    

    public async Task DeleteAsync(Guid id, string userId)
    {
        var channel = await _channelRepository
            .FindByCondition(x => x.Id == id, true)
            .Include(x=>x.PrimaryAdmin)
            .FirstOrDefaultAsync();
        if (channel == null)
        {
            throw new ArgumentException("Invalid channel Id.");
        }

        if (channel.PrimaryAdmin.IdentityId.ToString() != userId)
            throw new ArgumentException("You are not primary admin of this channel.");
        
        await _channelRepository.DeleteAsync(channel);
    }

    public async Task SubscribeAsync(Guid channelId, string? userId)
    {
        var channel = await _channelRepository
            .FindByCondition(x => x.Id == channelId, true)
            .Include(x=>x.Subscribers).FirstOrDefaultAsync();
        if (channel == null) throw new ArgumentException("Invalid channel Id.");
        var user = await _userRepository
            .FindByCondition(x => x.IdentityId == new Guid(userId), true)
            .Include(x=>x.Subscriptions)
            .Include(x=>x.OwnedChannels).FirstOrDefaultAsync();
        if (user == null) throw new ArgumentException("Unexpected behaviour. Invalid user Id.");
        if (user.OwnedChannels.Any(x => x.Id == channelId))
            throw new ArgumentException("Admin of this channel cannot unsubscribe to it.");
        if (channel.Subscribers.Any(x => x.IdentityId == user.IdentityId ))
        {
            channel.Subscribers.Remove(user);
        }
        else
        {
            channel.Subscribers.Add(user);
        }
        await _channelRepository.SaveRepoChangesAsync();

    }

    

    public async Task<IEnumerable<GetChannelDTO>> GetAllAsync()
    {
        var channels = await _channelRepository.FindAll(false).ToListAsync();
        return _mapper.Map<IEnumerable<GetChannelDTO>>(channels);
    }

    public async Task<GetChannelDTO> GetByIdAsync(Guid id)
    {
        var channel = await _channelRepository.FindByCondition(x => x.Id == id, false)
            .FirstOrDefaultAsync();
        //_userRepository.FindByCondition(x => x.Id == new Guid(), false).FirstOrDefault();
        return _mapper.Map<GetChannelDTO>(channel);
    }

    public async Task UpdateAsync(Guid id, UpdateChannelDTO channelDto, string userId)
    {
        var channelToUpdate = await _channelRepository.FindByCondition(x => x.Id == id, true).FirstOrDefaultAsync();
        if (channelToUpdate == null) throw new Exception();
        _mapper.Map(channelDto, channelToUpdate);
        await _channelRepository.UpdateAsync(channelToUpdate);
    }
    
    
    
}

