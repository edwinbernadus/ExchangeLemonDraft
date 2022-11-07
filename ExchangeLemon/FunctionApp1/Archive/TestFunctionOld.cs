//using System;
//using System.Diagnostics;
//using System.Net.Http;
//using System.Threading.Tasks;
//using Microsoft.Azure.WebJobs;
//using Microsoft.Azure.WebJobs.Host;
//using Newtonsoft.Json;



//public static class TestFunction
//{
//    [FunctionName("TestFunction")]
//    public static async Task Run([QueueTrigger("debug", Connection = "mainConnection")]string myQueueItem, TraceWriter log)
//    {
//        var hostName = GlobalParam.hostName;
//        try
//        {

//            log.Info($"C# Queue trigger function processed: {myQueueItem}");

//            var Section = "test";
//            var message1 = "hello";
//            message1 = myQueueItem;
//            var url = $"{hostName}/capture/{Section}/{message1}";

            
//            var httpClient = new HttpClient();
//            await httpClient.GetAsync(url);
//        }
//        catch (Exception ex)
//        {
//            log.Error(ex.Message, ex);
//        }

//    }
//}