using Bot.Bot;
using Bot.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bot.Routers
{
    public interface IRouter
    {
        IRouter Bind(string route, string action, string name = null);

        IReply Dispatch(string route);

        Route GetRouteByName(string name);

        void AddServices(IEnumerable<IService> services);
    }
}
