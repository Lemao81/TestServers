using Microsoft.AspNetCore.Mvc;

namespace MockingServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MockController : ControllerBase
    {
        [HttpPost("Mirth")]
        public JsonResult MirthFake([FromBody] string[] args) => new JsonResult(args);
    }
}