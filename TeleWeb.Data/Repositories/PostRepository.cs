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
    public class PostRepository : RepositoryBase<Post>, IPostRepository
    {
        private readonly TeleWebDbContext _dbContext;

        public PostRepository() : base(new TeleWebDbContext())//TODO: Remove this constructor
        {
            
        }
        public PostRepository(TeleWebDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task CreateAsync(Post post, Channel channel, User whoPosted)
        {
            post.Channel = channel;
            post.Date=DateTime.Now;
            post.AdminWhoPosted = whoPosted;
            await CreateAsync(post);
        }
    }
}
