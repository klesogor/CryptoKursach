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
        public RateService RateService { get; set; }
        public SubscriptionService SubscriptionService { get; set; }
        public StartService StartService { get; set; }
        public AggregationService AggregationService { get; set; }

        public IRouter Init()
        {
            var API = new AspNetApi();
            SubscriptionService =  new SubscriptionService(API);
            StartService = new StartService(API);
            AggregationService = new AggregationService(API);
            RateService = new RateService(API);

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
            router.Bind("/subscriptions", SubscriptionService.GetSubscriptions);
            router.Bind("/unsubscribe", SubscriptionService.GetSubscriptionsForUnsubscribe);
            router.Bind("/unsubscribe {currencyId}", SubscriptionService.Unsubscribe);
            router.Bind("/rate {currencyId} {marketId}", RateService.GetRate);
        }
    }
}
