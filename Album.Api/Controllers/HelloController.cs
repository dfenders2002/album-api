using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Album.Api.Controllers
{
    public class response
    {
        public string Response { get; set; }
    }
    public class HelloController : Controller
    {
        //private readonly ILogger<HelloController> _logger;

        //public HelloController(ILogger<HelloController> logger)
        //{
        // _logger = logger;
        //}
        [Route("/api/hello")]
        [HttpGet("{name?}")]
        public string hello(string name = "") { 
            //_logger.LogInformation(Name);
            response myreponse = new response();
            GreetingService greetingService = new GreetingService();
            myreponse.Response = greetingService.Hello(name);
            return myreponse.Response;
        }
    }
}
