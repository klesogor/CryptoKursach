using Bot.APIs;
using Bot.Bot;
using Bot.Bot.Replies.Interfaces;
using Bot.Routers;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Bot.Services
{
    public class StartService : ApiService
    {
        public StartService(IAPI api) : base(api)
        {
        }

        public async Task<IReply> Start(ParameterBag bag, Chat chat)
        {
            await _api.Start((int)chat.Id, chat.Username);

            return new Reply()
            {
                Text = "<b>Welcome to CryptoBot</b>.\n" +
                "First, you should subscribe for currency updates using /subscribe. Then you will automaticly" +
                "recive notifications"
            };
        }    
    }
}
