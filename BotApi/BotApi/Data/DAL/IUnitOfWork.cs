using BotApi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotApi.Data.DAL
{
    public interface IUnitOfWork
    {
        IRepository<T> GetRepository<T>() where T : BaseWithIncrementsId, new();

        Task<int> SaveAsync();
    }
}
