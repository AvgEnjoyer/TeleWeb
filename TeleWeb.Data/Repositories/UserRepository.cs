using Microsoft.EntityFrameworkCore;
using TeleWeb.Domain.Models;
using TeleWeb.Data.Repositories.Interfaces;

namespace TeleWeb.Data.Repositories
{
    public class UserRepository : IRepository
    {
        private readonly TeleWebDbContext _dbContext;
        public UserRepository(TeleWebDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
            {
                throw new ArgumentException($"User with id {id} not found");
            }
            return user;
        }
        public async Task<ICollection<User>> GetAllAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task AddAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(User user)
        {
            var userToDelete = await _dbContext.Users.FindAsync(user);
            if (userToDelete == null)
            {
                throw new ArgumentException($"User with id {user.Id} not found and cant be deleted");
            }
            _dbContext.Users.Remove(userToDelete);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var userToDelete = await _dbContext.Users.FindAsync(id);
            if (userToDelete == null)
            {
                throw new ArgumentException($"User with id {id} not found and cant be deleted");
            }
            _dbContext.Users.Remove(userToDelete);
            await _dbContext.SaveChangesAsync();
        }

    }
}
