using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Dogshouseservice.API.Controllers
{
    [Route("api")]
    [ApiController]
    [EnableRateLimiting("FixedWindowPolicy")]
    public class PingsController : ControllerBase
    {
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok("\"Dogshouseservice.Version1.0.1\"");
        }
    }
}