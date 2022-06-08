using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;
using Xunit;
using System.Net;

namespace Album.Api.Tests
{
    public class UnitTest1 : IClassFixture<WebApplicationFactory<Album.Api.Startup>>
    {
        [Fact]
        public void Test1()
        {
            GreetingService greetingService = new GreetingService();
            string anwser = greetingService.Hello("Daan");
            Assert.Equal(anwser, "Hello Daan from " + Dns.GetHostName());
        }
        [Theory]
        [InlineData("", "Hello world")]
        [InlineData(" ", "Hello world")]
        [InlineData(null, "Hello world")]
        public void Test2(string value1, string expected)
        {
            expected = expected  + " from " + Dns.GetHostName();
            GreetingService greetingService = new GreetingService();
            string awnser = greetingService.Hello(value1);
            Assert.Equal(awnser, expected);
        }

        private readonly WebApplicationFactory<Album.Api.Startup> _factory;

        public UnitTest1(WebApplicationFactory<Album.Api.Startup> factory)
        {
            _factory = factory;
        }


        [Theory]
        [InlineData("https://localhost:44309/api/hello", "Hello world")]
        [InlineData("https://localhost:44309/api/hello?name=Daan", "Hello Daan")]
        public async void GetTest(string url, string awnser)
        {
            awnser  = awnser + " from " + Dns.GetHostName();
            var client = _factory.CreateClient();
            var response = await client.GetAsync(url);
            var result = "";
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsStringAsync();
            }
            Assert.Equal(awnser, result);
        }
    }
}
