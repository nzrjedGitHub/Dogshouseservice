using Microsoft.AspNetCore.Mvc;

namespace Dogshouseservice.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class PingsController : ControllerBase
    {
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok("\"Dogshouseservice.Version1.0.1\"");
        }
    }
}