// using System;
// using System.Collections.Generic;
// using System.Threading.Tasks;
// using Microsoft.Azure.ServiceBus;

// namespace ExchangeLemonCore
// {
//     public class QueueManager
//     {
//         public static int TotalQueueAll = 0;
//         public static int TotalQueueAfterDispose = 0;
//         public static Dictionary<string, QueueClient> queueClients =
//        new Dictionary<string, QueueClient>();

//         public static QueueClient Get(string queueName)
//         {
//             var output = queueClients.GetValueOrDefault(queueName);
//             return output;
//         }

//         public static void Set(string queueName, QueueClient input)
//         {
//             TotalQueueAll++;
//             TotalQueueAfterDispose++;
//             // await Remove(queueName);
//             queueClients[queueName] = input;

//         }

//         internal static async Task Remove(string queueName)
//         {
//             try
//             {

//                 var queueClient = Get(queueName);
//                 if (queueClient != null)
//                 {
//                     await queueClient.CloseAsync();
//                     queueClients.Remove(queueName);
//                     TotalQueueAfterDispose--;
//                 }
//             }
//             catch (Exception)
//             {


//             }

//         }
//     }
// }