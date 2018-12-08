using Bot.APIs.DTO;
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
        private object IMarkupResponse;

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
            _bot.StartReceiving();
        }

        private async void _botMessageHandler(object sender, MessageEventArgs e)
        {
            try
            {
                var result = _router.Dispatch(e.Message.Text);
                await _bot.SendTextMessageAsync(
                    chatId: e.Message.Chat,
                    text: result.Message,
                    replyMarkup: result.Markup,
                    parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown
                    );
  
            }
            catch (DomainException) {
                await _bot.SendTextMessageAsync(chatId: e.Message.Chat, text: "Unknown command");
            }
        }
    }
}
