using TeleWeb.Domain.Models;

namespace TeleWeb.Data.Repositories.Interfaces
{
    public interface IPostRepository: IRepositoryBase<Post>
    {
        public Task CreateAsync(Post post, Channel channel, User whoPosted);
    }
}
