using System;
using System.Collections.Generic;
using System.Text;

namespace Bot.Entities
{
    public class Chart
    {
        public Currency Currency { get; set; }
        public Market Market { get; set; }
        public string ChartUrl { get; set; }
    }
}
