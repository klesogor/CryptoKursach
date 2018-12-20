using Bot.APIs.DTO;
using Bot.Bot.Replies;
using Bot.Bot.Replies.Interfaces;
using Bot.Exceptions;
using Bot.Routers;
using Newtonsoft.Json;
using System;
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
                var result = await _router.Dispatch(e.Message.Text, e.Message.Chat);
                result.Render(_renderer, (int)e.Message.Chat.Id);

            }
            catch (DomainException) {
                new Reply() { Text = "Unknown or incorrect command" }
                    .Render(_renderer,(int)e.Message.Chat.Id); 
            }
            catch (ApiException ex) {
                new Reply() { Text = JsonConvert.DeserializeObject<ErrorDto>(ex.RawData).Message }
                .Render(_renderer, (int)e.Message.Chat.Id);
            }
        }

        private async void _botCallbackHandler(object sender, CallbackQueryEventArgs e)
        {
            try
            {
                var result = await _router.Dispatch(e.CallbackQuery.Data, e.CallbackQuery.Message.Chat);
                result.Render(_renderer, (int)e.CallbackQuery.Message.Chat.Id);

            }
            catch (DomainException)
            {
                new Reply() { Text = "Unknown or incorrect command" }
                    .Render(_renderer, (int)e.CallbackQuery.Message.Chat.Id);
            }
            catch (ApiException ex)
            {
                new Reply() { Text = JsonConvert.DeserializeObject<ErrorDto>(ex.RawData).Message }
                .Render(_renderer, (int)e.CallbackQuery.Message.Chat.Id);
            }
        }
    }
}
