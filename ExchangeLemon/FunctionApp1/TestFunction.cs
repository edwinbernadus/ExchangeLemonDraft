using System;
using System.Net.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using System.Threading.Tasks;

//namespace FunctionApp1

using FunctionApp1;
namespace FunctionApp1
{
    public static class TestFunction
    {
        [FunctionName("TestFunction")]
        public static async Task Run([QueueTrigger("debug", Connection = "one")]string myQueueItem, TraceWriter log)
        {
            log.Info($"C# Queue trigger function processed: {myQueueItem}");

            var hostName = ModuleParam.GetHostName();
            var Section = "test";
            var message1 = "hello";
            message1 = myQueueItem;
            var url = $"{hostName}/capture/{Section}/{message1}";

            var httpClient = new HttpClient();
            await httpClient.GetAsync(url);
        }
    }

}