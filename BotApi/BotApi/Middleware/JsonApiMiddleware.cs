using System.Threading.Tasks;
using BotApi.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace BotApi.Middleware
{
    public class JsonApiMiddleware
    {
        private static readonly string _contentType = "Application/json";
        private static readonly int _defaultStatus = 200;

        private readonly RequestDelegate _next;

        public JsonApiMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                httpContext.Response.ContentType = _contentType;
                httpContext.Response.StatusCode = _defaultStatus;
                await _next(httpContext);
            }
            catch (HttpException ex) {
                httpContext.Response.Clear();
                httpContext.Response.StatusCode = ex.Status;
                httpContext.Response.ContentType = _contentType;
                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(new { message = ex.Message }));
            }
        }
    }
}
