using Microsoft.EntityFrameworkCore;
using TeleWeb.Domain.Models;
using TeleWeb.Data.Repositories.Interfaces;

namespace TeleWeb.Data.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        private readonly TeleWebDbContext _dbContext;
        
        public UserRepository(TeleWebDbContext dbContext) :base(dbContext)
        {
            _dbContext = dbContext;
        }

        public UserRepository() : base(new TeleWebDbContext()) //TODO: remove this constructor
        {
        }
    }
}
