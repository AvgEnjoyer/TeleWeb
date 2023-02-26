using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleWeb.Domain.Models;

namespace TeleWeb.Data.Repositories
{
    internal class ChannelRepository
    {
        private readonly TeleWebDbContext _dbContext;
        public ChannelRepository(TeleWebDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Channel> GetByIdAsync(int id)
        {
            var channel = await _dbContext.Channels.FindAsync(id);
            if (channel == null)
            {
                throw new ArgumentException($"Channel with id {id} not found");
            }
            return channel;
        }

        public async Task AddAsync(Channel channel)
        {
            await _dbContext.Channels.AddAsync(channel);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Channel channel)
        {
            _dbContext.Channels.Update(channel);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Channel channel)
        {
            var channelToDelete = await _dbContext.Channels.FindAsync(channel);
            if (channelToDelete == null)
            {
                throw new ArgumentException($"Channel with id {channel.Id} not found and cant be deleted");
            }
            _dbContext.Channels.Remove(channelToDelete);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var channelToDelete = await _dbContext.Channels.FindAsync(id);
            if (channelToDelete == null)
            {
                throw new ArgumentException($"Channel with id {id} not found and cant be deleted");
            }
            _dbContext.Channels.Remove(channelToDelete);
            await _dbContext.SaveChangesAsync();
        }
    }
}
