using Bot.Bot;
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
        private readonly Dictionary<string, IService> _services = new Dictionary<string, IService>();
        public IRouterExpressionParser _parser { get; set; } = new RouteExpressionParser();
        private static IRouter _instance;

        private Router()
        {

        }

        public static IRouter GetInstance()
        {
            if (_instance == null)
                _instance = new Router();
            return _instance;
        }

        public void AddServices(IEnumerable<IService> services)
        {
            foreach (var service in services)
            {
                _services.Add(service.GetType().Name, service);
            }
        }

        public IRouter Bind(string command, string action, string name = null)
        {
            var (instance, method) = _getBindingsForRoute(action);
            object cb(ParameterBag bag) => method.Invoke(
                    instance,
                    new object[] { bag }
                );
            var debug = _parser.parseExpression(command);
            _routes.Add(
                new Route() {
                    Name = name,
                    RawRoute = command,
                    CompiledRoute = _parser.parseExpression(command),
                    Handler = cb
                }
            );
            return this;
        }

        public IReply Dispatch(string route)
        {
            foreach (var router in _routes)
            {
                var match = Regex.Match(route, router.CompiledRoute);
                if (match.Success) {
                    return (IReply)router.Handler(_parseParams(match));
                }
            }

            throw new RouteException();
        }

        public Route GetRouteByName(string name)
        {
            return _routes.Find(x => x.Name == name);
        }

        private (IService instance, MethodInfo method) _getBindingsForRoute(string route)
        {
            var splitedRoute = route.Split('@');
            _services.TryGetValue(splitedRoute[0], out IService service);
            if (service is null) throw new RouteHandlerException();
            var methodInfo = service.GetType().GetMethod(splitedRoute[1]);
            if (methodInfo is null) throw new RouteHandlerException();

            return (instance: service, method: methodInfo);
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
