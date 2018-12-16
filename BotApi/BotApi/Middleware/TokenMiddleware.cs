using BotApi.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotApi.Middleware
{
    public class TokenMiddleware
    {
        private readonly string _token;
        private readonly RequestDelegate next;

        public TokenMiddleware(RequestDelegate next, string token)
        {
            _token = token;
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request?.Headers["X-AUTH-TOKEN"].First();
            if (_token != token)
            {
                throw new InvalidTokenException();
            }

            await next(context);
        }
    }
}
