using Bot.Bot;
using Bot.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bot.Routers
{
    public interface IRouter
    {
        IRouter Bind(string route, Func<ParameterBag,IReply> action, string name = null);

        IReply Dispatch(string route);

        Route GetRouteByName(string name);
    }
}
