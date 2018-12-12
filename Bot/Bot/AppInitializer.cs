using Bot.APIs;
using Bot.Bot;
using Bot.Routers;
using Bot.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bot
{
    public class AppInitializer
    {
        protected RateService RateService { get; set; }
        protected SubscriptionService SubscriptionService { get; set; }
        public IRouter Init()
        {
            var API = new AspNetApi();
            SubscriptionService =  new SubscriptionService(API);

            var router = new Router(new RouteExpressionParser());
            _bindRoutes(router);
            return router;
        }

        public void _bindRoutes(IRouter router)
        {
            router.Bind("/subscribe", SubscriptionService.GetAvailableCurrencies);
        }
    }
}
