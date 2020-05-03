using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestSseServer.Services;

namespace TestSseServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SseController : ControllerBase
    {
        private readonly ILogger<SseController> _logger;
        private readonly ServerSentEventsService _serverSentEventsService;

        public SseController(ILogger<SseController> logger, ServerSentEventsService serverSentEventsService)
        {
            _logger = logger;
            _serverSentEventsService = serverSentEventsService;
        }

        [HttpPost("Mirth")]
        public JsonResult MirthFake([FromBody] object payload) => new JsonResult(payload);
    }
}