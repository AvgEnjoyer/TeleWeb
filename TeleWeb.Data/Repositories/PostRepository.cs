using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleWeb.Domain.Models;

namespace TeleWeb.Data.Repositories
{
    internal class PostRepository
    {
        private readonly TeleWebDbContext _dbContext;
        public PostRepository(TeleWebDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Post> GetByIdAsync(int id)
        {
            var post = await _dbContext.Posts.FindAsync(id);
            if (post == null)
            {
                throw new ArgumentException($"Post with id {id} not found");
            }
            return post;
        }


        public async Task AddAsync(Post post)
        {
            await _dbContext.Posts.AddAsync(post);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Post post)
        {
            _dbContext.Posts.Update(post);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Post post)
        {
            var postToDelete = await _dbContext.Posts.FindAsync(post);
            if (postToDelete == null)
            {
                throw new ArgumentException($"Post with id {post.Id} not found and cant be deleted");
            }
            _dbContext.Posts.Remove(postToDelete);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var postToDelete = await _dbContext.Posts.FindAsync(id);
            if (postToDelete == null)
            {
                throw new ArgumentException($"Post with id {id} not found and cant be deleted");
            }
            _dbContext.Posts.Remove(postToDelete);
            await _dbContext.SaveChangesAsync();
        }
    }
}
