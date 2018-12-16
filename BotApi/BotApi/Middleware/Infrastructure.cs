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
        public static IApplicationBuilder UseMiddleware(this IApplicationBuilder builder, IConfiguration config)
        {
            builder.UseMiddleware<JsonApiMiddleware>();
            builder.UseMiddleware<TokenMiddleware>(
                    config.GetValue("Secrets.BotToken", "No token")
                );

            return builder;
        }
    }
}
