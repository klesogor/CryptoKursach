using Bot.Bot.Replies.Interfaces;
using System;
using System.Collections.Generic;
using Telegram.Bot.Types;

namespace Bot.Routers
{
    public interface IRouter
    {
        IRouter Bind(string route, Func<ParameterBag,Chat,IReply> action, string name = null);

        IReply Dispatch(string route, Chat chat);

        Route GetRouteByName(string name);
    }
}
