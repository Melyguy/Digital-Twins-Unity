using Microsoft.AspNetCore.Mvc;

namespace DigitalTwinsBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServerStateController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetServers()
        {
            var servers = new[]
            {
                new { id = "Server_01", Temperature = 75.5f, cpuUsage = 55.0f, Online = true },
                new { id = "Server_02", Temperature = 80.0f, cpuUsage = 70.0f, Online = true },
                new { id = "Server_03", Temperature = 90.0f, cpuUsage = 85.0f, Online = false },
                new { id = "Server_04", Temperature = 65.0f, cpuUsage = 40.0f, Online = true },
                new { id = "Server_05", Temperature = 78.0f, cpuUsage = 60.0f, Online = true },
                new { id = "Server_06", Temperature = 88.0f, cpuUsage = 75.0f, Online = false },
            };

            return Ok(servers);
        }
    }
}
