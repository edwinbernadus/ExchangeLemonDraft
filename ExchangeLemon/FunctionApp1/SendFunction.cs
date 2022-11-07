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
    public static class SendFunction
    {
        [FunctionName("SendFunction")]
        public static async Task Run([QueueTrigger("send", Connection = "one")]string myQueueItem, TraceWriter log)
        {

            var hostName = ModuleParam.GetHostName();
            SendMessage item = JsonConvert.DeserializeObject<SendMessage>(myQueueItem);

            var url = $"{hostName}/watcher/send/{item.blockChainId}" +
                    $"?confirmations={item.confirmations}&transactionId={item.transactionId}";



            var httpClient = new HttpClient();
            await httpClient.GetAsync(url);

            //var hostName = GlobalParam.hostName;

            //try
            //{

            //    log.Info($"C# Queue trigger function processed: {myQueueItem}");
            //    var item = JsonConvert.DeserializeObject<SendMessage>(myQueueItem);
            //    var httpClient = new HttpClient();


            //    var url = $"{hostName}/watcher/send/{item.blockChainId}" +
            //        $"?confirmations={item.confirmations}&transactionId={item.transactionId}";
            //    var s = await httpClient.GetStringAsync(url);
            //}
            //catch (Exception ex)
            //{
            //    log.Error(ex.Message, ex);
            //}

        }
    }
}