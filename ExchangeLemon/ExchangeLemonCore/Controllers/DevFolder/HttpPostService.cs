using System;
using System.Threading.Tasks;
//using ExchangeLemonCore.Data;
//using BackEndStandard;
using BlueLight.Main;
//using ExchangeLemonCore.Data;
using Microsoft.AspNetCore.SignalR;
using System.Net.Http;
using System.Net;

namespace ExchangeLemonCore.Controllers
{
    public class HttpPostService : IHttpPostService
    {
        public HttpPostService(IHttpClientFactory httpClientFactory)
        {
            HttpClientFactory = httpClientFactory;
        }

        public IHttpClientFactory HttpClientFactory { get; }

        public async Task<string> SendPost(string url, string bodyContent)
        {
            var client = this.HttpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(url),
                Headers = {
                { HttpRequestHeader.ContentType.ToString(), "application/json" },
            },
                Content = new StringContent(bodyContent)
            };

            var response = client.SendAsync(httpRequestMessage).Result;
            var output = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode == false)
            {
                var errMsg = $"[{response.StatusCode}] - {response.ReasonPhrase}";
                throw new ArgumentException(output);
            }

            return output;
        }
    }
}