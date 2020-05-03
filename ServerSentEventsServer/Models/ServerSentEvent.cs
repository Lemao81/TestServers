using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestSseServer.Models
{
    public class ServerSentEvent
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public IList<string> Data { get; set; }
    }
}