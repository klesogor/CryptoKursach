using Bot.APIs;
using Bot.Routers;
using Bot.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bot
{
    public class AppInitializer
    {
        public IRouter init()
        {
            var API = new ApiMock();
            var services = new IService[] { new RateService(API), new SubscriptionService(API) };
            var router = Router.GetInstance();
            router.AddServices(services);
            return router;
        }
    }
}
