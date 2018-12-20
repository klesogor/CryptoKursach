using Bot.APIs;
using Bot.Bot;
using Bot.Bot.Replies.Interfaces;
using Bot.Routers;
using Telegram.Bot.Types;

namespace Bot.Services
{
    public class StartService : ApiService
    {
        public StartService(IAPI api) : base(api)
        {
        }

        public IReply Start(ParameterBag bag, Chat chat)
        {
            var res = _api.Start((int)chat.Id, chat.Username).Result;
            if (res)
            {
                return new Reply()
                {
                    Text = "<b>Welcome to CryptoBot</b>.\n" +
                    "First, you should subscribe for currency updates using /subscribe. Then you will start" +
                    "to recive notifications"
                };
            }
            return new Reply()
            {
                Text = "<b> Something went wrong. Try again later</b>"
            };
        }
    }
}
