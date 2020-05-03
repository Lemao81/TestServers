using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestSseServer.Models;
using TestSseServer.Services;

namespace TestSseServer
{
    public class ServerSentEventsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ServerSentEventsService _serverSentEventsService;

        public ServerSentEventsMiddleware(RequestDelegate next, ServerSentEventsService serverSentEventsService)
        {
            _next = next;
            _serverSentEventsService = serverSentEventsService;
        }

        public Task Invoke(HttpContext context)
        {
            if (context.Request.Headers["Accept"] == "text/event-stream")
            {
                context.Response.ContentType = "text/event-stream";
                context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                context.Response.Body.Flush();

                var electronId = context.Request.Query["electronId"].ToString();
                if (!string.IsNullOrEmpty(electronId))
                {
                    var client = new ServerSentEventsClient(context.Response, electronId);
                    _serverSentEventsService.AddClient(client);
                    context.RequestAborted.WaitHandle.WaitOne();

                    _serverSentEventsService.RemoveClient(electronId);

                    return Task.FromResult(true);
                }

                return Task.FromResult(false);
            }
            else
            {
                context.Response.StatusCode = 200;
                return _next(context);
            }
        }
    }

    public static class ServerSentEventsMiddlewareExtensions
    {
        public static IApplicationBuilder UseServerSentEventsMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ServerSentEventsMiddleware>();
        }
    }
}