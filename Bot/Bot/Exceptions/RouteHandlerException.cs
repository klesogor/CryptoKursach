using System;
using System.Collections.Generic;
using System.Text;

namespace Bot.Exceptions
{
    public class RouteHandlerException: DomainException
    {
        public RouteHandlerException() : base("Incorrect route handler!") { }
    }
}
