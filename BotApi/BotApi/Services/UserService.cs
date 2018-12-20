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

        public async Task<User> RegisterUser(int chatId, string Name)
        {
            var user = new User() { ChatId = chatId, Username = Name };
            var created = await _uow.GetRepository<User>().CreateAsync(user);

            await _uow.SaveAsync();

            return created;
        }
    }
}
