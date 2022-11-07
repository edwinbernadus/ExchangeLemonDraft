// using Microsoft.AspNetCore.Mvc;
// using System.Linq;

// namespace ExchangeLemonCore.Controllers
// {
//     public class DevQueueController : Controller
//     {
//         // http://localhost:5000/DevQueue/GetTotalQueueDispose
//         public int GetTotalQueueDispose()
//         {
//             var output = QueueManager.TotalQueueAfterDispose;
//             return output;
//         }


//         // http://localhost:5000/DevQueue/GetTotalQueueAll
//         public int GetTotalQueueAll()
//         {
//             var output = QueueManager.TotalQueueAll;
//             return output;
//         }

//         // http://localhost:5000/DevQueue/GetTotalQueueDictionary
//         public int GetTotalQueueDictionary()
//         {
//             var output = QueueManager.queueClients.Count();
//             return output;
//         }
//     }
// }