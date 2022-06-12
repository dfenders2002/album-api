using Microsoft.AspNetCore.Mvc;

namespace Album.Api.Controllers
{
    [Route("/health")]
    [ApiController]
    public class HealthController : Controller
    {
        [HttpGet("")]
        public string Get()
        {
            return "healthy";
        }
    }
}