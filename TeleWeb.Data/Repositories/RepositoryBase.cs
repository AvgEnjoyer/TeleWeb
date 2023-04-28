using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TeleWeb.Data.Repositories.Interfaces;
using TeleWeb.Domain.Models;

namespace TeleWeb.Data.Repositories
{
    public abstract class RepositoryBase<T>: IRepositoryBase<T> where T: class
    {
        private readonly TeleWebDbContext _dbContext;

        public RepositoryBase(TeleWebDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<T> FindAll(bool trackChanges)
        {
            if (trackChanges)
            {
                // If trackChanges is true, return the queryable with tracking enabled
                return _dbContext.Set<T>();
            }
            else
            {
                // If trackChanges is false, return the queryable with tracking disabled
                return _dbContext.Set<T>().AsNoTracking();
            }
        }


        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
        {
            if (trackChanges)
            {
                // If trackChanges is true, return the queryable with tracking enabled
                return _dbContext.Set<T>().Where(expression);
            }
            else
            {
                // If trackChanges is false, return the queryable with tracking disabled
                return _dbContext.Set<T>().Where(expression).AsNoTracking();
            }
        }

        public async Task CreateAsync(T entity)
        {
            await _dbContext.AddAsync<T>(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task SaveRepoChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
