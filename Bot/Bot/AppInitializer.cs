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
        protected StartService StartService { get; set; }
        public IRouter Init()
        {
            var API = new AspNetApi();
            SubscriptionService =  new SubscriptionService(API);
            StartService = new StartService(API);

            var router = new Router(new RouteExpressionParser());
            _bindRoutes(router);
            return router;
        }

        protected virtual void _bindRoutes(IRouter router)
        {
            router.Bind("/subscribe", SubscriptionService.GetAvailableCurrencies);
            router.Bind("/subscribe {currencyId}", SubscriptionService.GetAwailableMarketsByCurrency);
            router.Bind("/subscribe {currencyId} {marketId}", SubscriptionService.Subscribe);
            router.Bind("/start", StartService.Start);
        }
    }
}
