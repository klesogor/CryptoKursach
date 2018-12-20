using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BotApi.Requests;
using BotApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BotApi.Controllers
{
    public class UserController: TelegramBotController
    {
        public UserController(IMapper mapper, IUserService userService) : base(mapper, userService)
        {
        }
        [HttpPost]
        [Route("/api/v1/start")]
        public async Task<IActionResult> Register([FromForm]RegisterRequest request)
        {
                await _userService.RegisterUser(request.ChatId, request.Name);
                return Ok();
        }
    }
}
