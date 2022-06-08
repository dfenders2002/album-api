using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Album.Api
{
    public class GreetingService
    {
        //private readonly ILogger<GreetingService> _logger;
        //public GreetingService(ILogger<GreetingService> logger)
        //{
        //    _logger = logger;
        //}
        public string Hello(string name)
        {
            if (name == "" || name == null || name == " ")
            {
                //_logger.LogInformation("Hello world");
                return "Hello world from " + Dns.GetHostName();
            }
            else
            {
                //_logger.LogInformation($"Hello {name}");
                return $"Hello {name} from " + Dns.GetHostName() ;
            }
        }
    }
}
