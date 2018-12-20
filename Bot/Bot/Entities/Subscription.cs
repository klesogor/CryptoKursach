using System;
using System.Collections.Generic;
using System.Text;

namespace Bot.Entities
{
    public class Subscription
    {
        public int Id { get; set; }
        public Currency Currency { get; set; }
        public Market Market { get; set; }
    }
}
