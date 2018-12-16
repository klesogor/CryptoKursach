using BotApi.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BotApi.Data.DAL
{
    public class Repository<TEntity>: IRepository<TEntity> where TEntity : BaseWithIncrementsId
    {
        protected DbContext context;
        protected DbSet<TEntity> DbSet;

        public Repository(DbContext c)
        {
            this.context = c;
            DbSet = context.Set<TEntity>();
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            return (await DbSet.AddAsync(entity)).Entity;
        }

        public async Task<TEntity> DeleteAsync(int id)
        {
            TEntity temp = await DbSet.FindAsync(id);
            if (temp != null)
            { 
                return DbSet.Remove(temp).Entity;
            }
            return null;
        }

        public async Task<TEntity> PutAsync(int id)
        {
            TEntity temp = await DbSet.FindAsync(id);
            if (temp != null)
            {
                var result = DbSet.Update(temp).Entity;      
                return result;
            }
            return null;
        }

        public async Task<TEntity> GetAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.Where(predicate).ToListAsync();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            return Task.FromResult(DbSet.Update(entity).Entity);
        }

        public async Task<TEntity> GetLastAsync()
        {
            return await DbSet.LastOrDefaultAsync();
        }
    }
}
