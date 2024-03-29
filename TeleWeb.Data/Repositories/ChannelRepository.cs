﻿using TeleWeb.Data.Repositories.Interfaces;
using TeleWeb.Domain.Models;

namespace TeleWeb.Data.Repositories
{
    public class ChannelRepository : RepositoryBase<Channel>, IChannelRepository
    {
        private readonly TeleWebDbContext _dbContext;
        
        public ChannelRepository(TeleWebDbContext dbContext) :base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Post> FindPostsByChannelId(Guid channelId)
        {
            return _dbContext.Channels.Where(x => x.Id == channelId).SelectMany(x => x.Posts);
        }

        public async Task CreateChannelAsync(Channel channel, User admin)
        {
            channel.Admins.Add(admin);
            channel.PrimaryAdmin= admin;
            admin.OwnedChannels.Add(channel);
            
            admin.Subscriptions.Add(channel);
            await _dbContext.Channels.AddAsync(channel);
        }
    }
}
