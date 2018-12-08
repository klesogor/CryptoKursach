using System;
using System.Collections.Generic;
using System.Text;

namespace Bot.Exceptions
{
    public class RouteParamsException: DomainException
    {
        public RouteParamsException() : base("Incorrect route argument format") { }
    }
}
