using TeleWeb.Domain.Models;

namespace TeleWeb.Data.Repositories.Interfaces
{
    public interface IChannelRepository: IRepositoryBase<Channel> 
    {
        public IQueryable<Post> FindPostsByChannelId(Guid channelId);
        public Task CreateChannelAsync(Channel channel, User admin);
    }
}
