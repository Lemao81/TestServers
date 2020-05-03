using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestSseServer.Helper;

namespace TestSseServer.Models
{
    public class ServerSentEventsClient
    {
        private readonly HttpResponse _response;

        public string ElectronId { get; }

        internal ServerSentEventsClient(HttpResponse response, string electronId)
        {
            _response = response;
            ElectronId = electronId;
        }

        public Task SendEventAsync(ServerSentEvent serverSentEvent)
        {
            return _response.WriteSseEventAsync(serverSentEvent);
        }
    }
}