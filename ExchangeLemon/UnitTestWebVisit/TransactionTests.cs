using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace BlueLight.Main.Tests
{
    public class TransactionTests
    : IClassFixture<CustomWebApplicationFactory<ExchangeLemonCore.Startup>>
    {
        private readonly CustomWebApplicationFactory<ExchangeLemonCore.Startup> _factory;

        public TransactionTests(CustomWebApplicationFactory<ExchangeLemonCore.Startup> factory)
        {
            _factory = factory;
        }


        [Fact]
        public async Task ExecuteZero()
        {
            // Arrange
            var client = _factory.CreateClient();


            // Act
            var url = "http://localhost:5000/DevEntity";
            //var url = "http://localhost:53252/DevEntity";
            



            var response = await client.GetAsync(url);

            var message = await response.Content.ReadAsStringAsync();
            //// Assert
            //response.EnsureSuccessStatusCode(); // Status Code 200-299
            //Assert.Equal("text/plain; charset=utf-8",
            //    response.Content.Headers.ContentType.ToString());
        }


        //TODO: 104 - pending unit test web server
        public async Task Execute()
        {
            await Task.Delay(0);
            // Arrange
            var client = _factory.CreateClient();


            // Act
            var url = "/api/orderItemMain";
            
            
            InputTransactionRaw testObj = new InputTransactionRaw()
            {
                rate = "1000",
                amount = "0.5",
                mode = "buy",
                current_pair = "btc_usd"
            };

            var content = new StringContent(JsonConvert.SerializeObject(testObj), Encoding.UTF8, "application/json");
            //string result = Request.SendRequest("/api/test", content, HttpMethod.Post);

            var response = await client.PostAsync(url,content);
            
            var message = await response.Content.ReadAsStringAsync();
            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }
    }
}