using Microsoft.VisualBasic;
using TeleWeb.Domain.Models;

namespace TeleWeb.Data.Repositories.Interfaces
{
    public interface IUserRepository : IRepository
    {
        Task<User> GetByIdAsync(Guid id);
        Task<ICollection<User>> GetAllAsync();
        Task CreateAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(Guid id);
    }
}
