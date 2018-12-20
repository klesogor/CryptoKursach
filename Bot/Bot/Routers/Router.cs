﻿using Bot.Bot;
using Bot.Bot.Replies.Interfaces;
using Bot.Exceptions;
using Bot.Services;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Bot.Routers
{
    public class Router : IRouter
    {
        //binded routes
        private readonly List<Route> _routes = new List<Route>();
        public readonly IRouterExpressionParser _parser;

        public Router(IRouterExpressionParser parser)
        {
            _parser = parser;
        }

        public IRouter Bind(string command, Func<ParameterBag,int,IReply> action, string name = null)
        {
  
            _routes.Add(
                new Route() {
                    Name = name,
                    RawRoute = command,
                    CompiledRoute = _parser.ParseExpression(command),
                    Handler = action
                }
            );
            return this;
        }

        public IReply Dispatch(string route, int chatId)
        {
            foreach (var router in _routes)
            {
                var match = Regex.Match(route, router.CompiledRoute);
                if (match.Success) {
                    var param = _parseParams(match);
                    param.AddParameter("command", route);
                    return router.Handler(param, chatId);
                }
            }

            throw new RouteException();
        }

        public Route GetRouteByName(string name)
        {
            return _routes.Find(x => x.Name == name);
        }

        private ParameterBag _parseParams(Match matches)
        {
            var bag = new ParameterBag();
            foreach (var group in matches.Groups)
            {
                var castGroup = (Group)group;
                bag.AddParameter(castGroup.Name, castGroup.Value);
            }

            return bag;
        }
    }
}