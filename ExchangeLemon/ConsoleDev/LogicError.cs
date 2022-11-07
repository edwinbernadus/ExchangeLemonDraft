using RichardSzalay.MockHttp;
using System;
using System.Net;
using System.Threading.Tasks;
//using DebugWorkplace;

namespace ConsoleDev
{
    public class LogicError
    {
        public async Task TestOne()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("http://localhost/api/user/*")
                .Respond(HttpStatusCode.InternalServerError);
            // Setup a respond for the user api (including a wildcard in the URL)
            //mockHttp.When("http://localhost/api/user/*")
            //        .Respond("application/json", "{'name' : 'Test McGee'}"); // Respond with JSON

            // Inject the handler or client into your application code
            var client = mockHttp.ToHttpClient();

            var response = await client.GetAsync("http://localhost/api/user/1234");
            // or without async: var response = client.GetAsync("http://localhost/api/user/1234").Result;

            var json = await response.Content.ReadAsStringAsync();

            // No network connection required
            Console.Write(json); // {'name' : 'Test McGee'}
        }
    }
}
