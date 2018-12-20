using Bot.APIs.DTO;
using Bot.Bot.Replies;
using Bot.Bot.Replies.Interfaces;
using Bot.Exceptions;
using Bot.Routers;
using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace Bot.Bot
{
    public class TelegramBot : IBot
    {
        private readonly string _PKEY;
        private readonly IRouter _router;
        private ITelegramBotClient _bot;
        private IResponseRenderer _renderer;

        public TelegramBot(string PKEY, IRouter router)
        {
            _PKEY = PKEY;
            _router = router;
        }

        public void Start()
        {
            _bot = new TelegramBotClient(_PKEY);
            Console.WriteLine("Starting bot...");
            _bot.OnMessage += _botMessageHandler;
            _bot.OnCallbackQuery += _botCallbackHandler;
            _bot.StartReceiving();
            _renderer = new Renderer(_bot);
        }

        private async void _botMessageHandler(object sender, MessageEventArgs e)
        {
            try
            {
                var result = _router.Dispatch(e.Message.Text, e.Message.Chat);
                result.Render(_renderer, (int)e.Message.Chat.Id);
  
            }
            catch (DomainException) {
                await _bot.SendTextMessageAsync(chatId: e.Message.Chat, text: "Unknown command");
            }
        }

        private async void _botCallbackHandler(object sender, CallbackQueryEventArgs e)
        {
            try
            {
                var result = _router.Dispatch(e.CallbackQuery.Data, e.CallbackQuery.Message.Chat);
                result.Render(_renderer, (int)e.CallbackQuery.Message.Chat.Id);

            }
            catch (DomainException)
            {
                await _bot.SendTextMessageAsync(chatId: e.CallbackQuery.Message.Chat, text: "Unknown command");
            }
        }
    }
}
