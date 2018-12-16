using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotApi.Middleware
{
    public static class RegisterMiddlewareExtension
    {
        public static IApplicationBuilder UseBotApiMiddleware(this IApplicationBuilder builder, IConfiguration config)
        {
            builder.UseMiddleware<JsonApiMiddleware>();
            builder.UseMiddleware<TokenMiddleware>(
                   config.GetSection("Secrets:BotToken").Value
                );

            return builder;
        }
    }
}
