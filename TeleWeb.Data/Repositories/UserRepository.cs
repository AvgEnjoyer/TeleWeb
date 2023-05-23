using Microsoft.EntityFrameworkCore;
using TeleWeb.Domain.Models;
using TeleWeb.Data.Repositories.Interfaces;

namespace TeleWeb.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TeleWebDbContext _dbContext;
        public UserRepository(TeleWebDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
            {
                throw new ArgumentException($"User with id {id} not found in DBcotext");
            }
            return user;
        }
        public async Task<ICollection<User>> GetAllAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task CreateAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(User userWithNewInfo)
        {
            var userToUpdate = await _dbContext.Users.FindAsync(userWithNewInfo.Id);
            if (userToUpdate == null)
            {
                throw new ArgumentException($"User with id {userWithNewInfo.Id} not found in DBcontext");
            }
            _dbContext.Users.Update(userToUpdate);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var userToDelete = await _dbContext.Users.FindAsync(id);
            if (userToDelete == null)
            {
                throw new ArgumentException($"User with id {id} not found in DBcontext and cant be deleted");
            }
            _dbContext.Users.Remove(userToDelete);
            await _dbContext.SaveChangesAsync();
        }


        public async Task SaveRepoChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

    }
}
