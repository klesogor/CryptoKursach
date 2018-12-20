using BotApi.Data.DAL;
using BotApi.Data.Models;
using BotApi.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace BotApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;

        public UserService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<User> GetUserByChatId(int chatId)
        {
            return (await _uow.GetRepository<User>().GetAllAsync(u => u.ChatId == chatId)).First();
        }
    }
}
