// using System;
// using Microsoft.Extensions.DependencyInjection;

// using System.Diagnostics;
// using System.Text;
// using System.Threading;
// using System.Threading.Tasks;
// using BlueLight.Main;
// using Microsoft.AspNetCore.SignalR;
// using Microsoft.Azure.ServiceBus;
// using Newtonsoft.Json;
// using System.Net.Http;

// namespace ExchangeLemonCore
// {




//     public class ServiceBusService
//     {
//         private LogicBusService TestBusService;

//         public ServiceBusService(IHttpClientFactory httpClientFactory)
//         {
//             HttpClientFactory = httpClientFactory;
//         }
//         public static bool IsServiceBusStarted { get; private set; }
//         public static bool IsInit { get; private set; }
//         public IHttpClientFactory HttpClientFactory { get; }
//         public LogicBusService ReceiveBusService { get; private set; }
//         public LogicBusService SendBusService { get; private set; }

//         void Init()
//         {
//             {
//                 string QueueName = "test-one";
// #if DEBUG
//                 QueueName = "test-one-dev";
// #endif
//                 this.TestBusService = new LogicBusService(this.HttpClientFactory);
//                 TestBusService.Populate("Signal", QueueName);
//             }
//             {
//                 string QueueName = "receiver";
//                 this.ReceiveBusService = new LogicBusService(this.HttpClientFactory);
//                 ReceiveBusService.Populate("receive", QueueName);
//             }
//             {
//                 string QueueName = "send";
//                 this.SendBusService = new LogicBusService(this.HttpClientFactory);
//                 SendBusService.Populate("send", QueueName);
//             }
//         }



//         public async Task Run()
//         {
//             // IsServiceBusStarted = true;
//             // Init();
//             // await TestBusService.Restart();
//             // await SendBusService.Restart();
//             // await ReceiveBusService.Restart();
//         }
//     }



// }
