using AutoMapper;
using BotApi.Data.Models;
using BotApi.Exceptions;
using BotApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BotApi.Controllers
{
    public abstract class TelegramBotController: ControllerBase
    {
        protected const string ChatIdToken = "X-CHAT-ID";

        protected readonly IMapper _mapper;
        protected readonly IUserService _userService;

        protected TelegramBotController(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        protected virtual async Task<User> GetCurrentUser()
        {
            var user = await _userService.GetUserByChatId(int.Parse(Request.Headers[ChatIdToken]));
            if (user is null) throw new UnregisteredUserException();
            return user;
        }

        protected virtual int GetCurrentChatId()
        {
            return int.Parse(Request.Headers[ChatIdToken]);
        }
    }
}
