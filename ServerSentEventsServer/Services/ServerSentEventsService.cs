using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestSseServer.Models;

namespace TestSseServer.Services
{
    public class ServerSentEventsService
    {
        private readonly ConcurrentDictionary<string, ServerSentEventsClient> _clients = new ConcurrentDictionary<string, ServerSentEventsClient>();

        internal void AddClient(ServerSentEventsClient client)
        {
            if (!_clients.ContainsKey(client.ElectronId))
            {
                _clients.TryAdd(client.ElectronId, client);
            }

        }

        internal void RemoveClient(string electronId)
        {
            _clients.TryRemove(electronId, out var client);
        }

        public Task SendEventAsync(ServerSentEvent serverSentEvent, string electronId)
        {
            if (_clients.TryGetValue(electronId, out var client))
            {
                return client.SendEventAsync(serverSentEvent);
            }

            return Task.FromResult(false);
        }
    }
}