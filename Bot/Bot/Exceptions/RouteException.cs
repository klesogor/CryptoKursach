using System;
using System.Collections.Generic;
using System.Text;

namespace Bot.Exceptions
{
    class RouteException : DomainException
    {
        public RouteException():base("Incorrect route format") { }
    }
}
