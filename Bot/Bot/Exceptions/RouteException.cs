using System;
using System.Collections.Generic;
using System.Text;

namespace Bot.Exceptions
{
    public class RouteException : DomainException
    {
        public RouteException():base("Incorrect route format") { }
    }
}
