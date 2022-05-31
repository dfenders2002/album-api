using System;
using Xunit;

namespace Album.Api.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            GreetingService greetingService = new GreetingService();
            string awnser = greetingService.Hello("Daan");
            Assert.Equal(awnser, "Hello Daan");
        }
        [Theory]
        [InlineData("", "Hello world")]
        [InlineData(" ", "Hello world")]
        [InlineData(null, "Hello world")]
        public void Test2(string value1, string expected)
        {
            GreetingService greetingService = new GreetingService();
            string awnser = greetingService.Hello(value1);
            Assert.Equal(awnser, expected);
        }
    }
}
