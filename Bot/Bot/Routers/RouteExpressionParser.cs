using Bot.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace Bot.Routers
{
    sealed class RouteExpressionParser: IRouterExpressionParser 
    {
        private readonly string commandPattern = @"^\/?(?<command>\w+)(?:(?<arguments>[\w\d {}=_]*))$";
        private readonly string argumentPattern = @"^(?<name>--\w+=)?{(?<argument>\w+)}$";

        public string ParseExpression(string command)
        {
            var expression = new Regex(commandPattern);
            var resultsOfParsing = expression.Match(command);

            if (!resultsOfParsing.Success) throw new RouteException();

            var stringBuilder = new StringBuilder();
            //append slash to begining of command
            stringBuilder.Append("^/");
            //append command name
            stringBuilder.Append(resultsOfParsing.Groups["command"].Value);
            //if command have argumets

            if (resultsOfParsing.Groups["arguments"].Length > 0)
            {
                stringBuilder.Append(' ');
                var parsed = resultsOfParsing.Groups["arguments"]
                            .Value
                            .Split(null)
                            .Where(x => !string.IsNullOrWhiteSpace(x))
                            .Select(x => _parseArgument(x))
                            .ToArray();
                var joined = string.Join(' ',parsed);
                stringBuilder.Append(joined);
            }

            return stringBuilder.Append('$').ToString();
        }

        private string _parseArgument(string argument)
        {
            var expression = new Regex(argumentPattern);
            var result = expression.Match(argument);
            if (!result.Success) throw new RouteParamsException();
            var stringBuilder = new StringBuilder();
            //append command name
            if (result.Groups["name"] != null) stringBuilder.Append(result.Groups["name"].Value);
            //append command regex
            stringBuilder.Append($@"(?<{result.Groups["argument"].Value}>[^ ]+)");

            return stringBuilder.ToString();
        }
    }
}
