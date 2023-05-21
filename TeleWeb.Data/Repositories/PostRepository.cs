using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TeleWeb.Data.Repositories.Interfaces;
using TeleWeb.Domain.Models;

namespace TeleWeb.Data.Repositories
{
    internal class PostRepository : RepositoryBase<Post>, IPostRepository
    {
        private readonly TeleWebDbContext _dbContext;

        public PostRepository(TeleWebDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        
    }
}
