using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using FunctionApp1;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
namespace FunctionApp1
{
    public static class ReceiveFunction
    {
        [FunctionName("ReceiveFunction")]
        public static async Task Run([QueueTrigger("receive", Connection = "one")]string myQueueItem, TraceWriter log)
        {

            var hostName = ModuleParam.GetHostName();
            var item = JsonConvert.DeserializeObject<ReceiveMessage>(myQueueItem);

            var url = $"{hostName}/watcher/receive/{item.blockChainId}" +
                  $"?address={item.address}";


            var httpClient = new HttpClient();
            await httpClient.GetAsync(url);


            //var hostName = GlobalParam.hostName;
            //try
            //{

            //    log.Info($"C# Queue trigger function processed: {myQueueItem}");
            //    var item = JsonConvert.DeserializeObject<ReceiveMessage>(myQueueItem);
            //    var httpClient = new HttpClient();


            //    var url = $"{hostName}/watcher/receive/{item.blockChainId}" +
            //        $"?address={item.address}";
            //    var s = await httpClient.GetStringAsync(url);
            //}
            //catch (Exception ex)
            //{
            //    log.Error(ex.Message, ex);
            //}

        }
    }
}