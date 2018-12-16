using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotApi.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BotApi.Data.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Dictionary<Type, object> _repositories;
        private readonly DbContext context;

        public UnitOfWork(DbContext context)
        {
            _repositories = new Dictionary<Type, object>();
            this.context = context;
        }

        public IRepository<T> GetRepository<T>() where T : BaseWithIncrementsId, new()
        {
            if (_repositories.ContainsKey(typeof(T)))
            {
                return (IRepository<T>)_repositories[typeof(T)];
            }
            else
            {
                var repo = new Repository<T>(context);
               _repositories.Add(typeof(T), repo);
                return repo;
            }
        }

        public Task<int> SaveAsync()
        {
            throw new NotImplementedException();
        }
    }
}
