using System;
using System.Collections.Generic;
using System.Text;

namespace Bot.Routers
{
    public interface IRouterExpressionParser
    {
        string parseExpression(string command);
    }
}
