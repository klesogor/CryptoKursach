using Bot.Bot.Replies.Interfaces;
using System;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Bot.Routers
{
    public interface IRouter
    {
        IRouter Bind(string route, Func<ParameterBag,Chat,Task<IReply>> action, string name = null);

        Task<IReply> Dispatch(string route, Chat chat);

        Route GetRouteByName(string name);
    }
}
