using BotApi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BotApi.Data.DAL
{
    public interface IRepository<TEntity> where TEntity : BaseWithIncrementsId
    {
        Task<TEntity> CreateAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task<TEntity> DeleteAsync(int id);

        Task<List<TEntity>> GetAllAsync();

        Task<TEntity> GetAsync(int id);

        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate);

    }
}
