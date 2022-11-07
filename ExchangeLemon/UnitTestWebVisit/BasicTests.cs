using System.Threading.Tasks;
using Xunit;


using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


namespace BlueLight.Main.Tests
{



    public class BasicTests
    : IClassFixture<WebApplicationFactory<ExchangeLemonCore.Startup>>
    {
        private readonly WebApplicationFactory<ExchangeLemonCore.Startup> _factory;

        public BasicTests(WebApplicationFactory<ExchangeLemonCore.Startup> factory)
        {
            _factory = factory;
        }

        [Theory]

        [InlineData("/")]

        [InlineData("/Account/Login")]
        [InlineData("/Account/Register")]
        [InlineData("/SpotMarket")]
        [InlineData("/SpotMarket/Details/btc_usd")]
        

        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);
            var message = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }


        [Theory]

        [InlineData("/DevEntity")]

        public async Task Get_EndpointsReturnSuccessAndCorrectContentTypeTextPlain(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);
            var message = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/plain; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }
    }
}