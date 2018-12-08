﻿using Bot.Bot;
using System;

namespace Bot
{
    class Program
    {
        static void Main(string[] args)
        {
            var conf = new AppInitializer();
            var router = conf.init();
            router.Bind("/test {user_id} {currency_id} {market_id}", "SubscriptionService@Subscribe");
            var bot = new TelegramBot("703904124:AAF1xpEKAlVx_l1ovSUCDhaNuY7-b8ZydHY",router);
            bot.Start();
            Console.WriteLine("Press any key to stop...");
            Console.ReadKey();
        }
    }
}
