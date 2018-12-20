using BotApi.Data.Models;
using System.Threading.Tasks;

namespace BotApi.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserByChatId(int chatId);

        Task<User> RegisterUser(int chatId, string Name);
    }
}
