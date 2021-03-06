﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestSseServer.Models;

namespace TestSseServer.Helper
{
    internal static class ServerSentEventsHelper
    {
        internal static async Task WriteSseEventAsync(this HttpResponse response, ServerSentEvent serverSentEvent)
        {
            if (!string.IsNullOrWhiteSpace(serverSentEvent.Id))
                await response.WriteSseEventFieldAsync("id", serverSentEvent.Id);

            if (!string.IsNullOrWhiteSpace(serverSentEvent.Type))
                await response.WriteSseEventFieldAsync("event", serverSentEvent.Type);

            if (serverSentEvent.Data != null)
            {
                foreach (string data in serverSentEvent.Data)
                    await response.WriteSseEventFieldAsync("data", data);
            }

            await response.WriteSseEventBoundaryAsync();
            response.Body.Flush();
        }

        private static Task WriteSseEventFieldAsync(this HttpResponse response, string field, string data)
        {
            return response.WriteAsync($"{field}: {data}\n");
        }

        private static Task WriteSseEventBoundaryAsync(this HttpResponse response)
        {
            return response.WriteAsync("\n");
        }
    }
}