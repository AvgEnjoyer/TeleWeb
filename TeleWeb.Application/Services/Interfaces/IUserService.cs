using TeleWeb.Application.DTOs;
namespace TeleWeb.Application.Services.Interfaces
{
    public interface IUserService : IService
    {
        public Task<IEnumerable<GetChannelDTO>> GetUserSubscriptionsByIdentityAsync(string identityId);
    }
}
