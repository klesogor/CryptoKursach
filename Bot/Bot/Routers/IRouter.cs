using Bot.Bot.Replies.Interfaces;
using System;

namespace Bot.Routers
{
    public interface IRouter
    {
        IRouter Bind(string route, Func<ParameterBag,int,IReply> action, string name = null);

        IReply Dispatch(string route, int ChatId);

        Route GetRouteByName(string name);
    }
}
