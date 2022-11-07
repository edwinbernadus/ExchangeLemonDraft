using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;

namespace FunctionApp1
{
    public static class TestCall
    {
        [FunctionName("Version")]
        //public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            // string name = req.Query["name"];

            // string requestBody = new StreamReader(req.Body).ReadToEnd();
            // dynamic data = JsonConvert.DeserializeObject(requestBody);
            // name = name ?? data?.name;


            var localVersion = 2;
            var globalVersion =  BlueLight.Main.VersionItem.ParamVersion;
            var output = $"{localVersion} - {globalVersion}";

            // var localVersion = 1;
            // var output = $"Version : {localVersion} - {BlueLight.Main.VersionItem.ParamVersion}";

            return (ActionResult)new OkObjectResult(output);
            //return name != null
            //    ? (ActionResult)new OkObjectResult($"Hello, {name}")
            //    : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }
    }
}
