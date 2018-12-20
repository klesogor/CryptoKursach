using BotApi.Data.Models;
using System.Threading.Tasks;

namespace BotApi.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserByChatId(int chatId);
    }
}
