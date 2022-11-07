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
// using System.Collections;
// using System.Collections.Generic;

// namespace ExchangeLemonCore
// {

//     public class LogicBusService
//     {

//         public void Populate(string section, string queueName)
//         {
//             this.Section = section;
//             this.QueueName = queueName;
//         }

//         public LogicBusService(IHttpClientFactory httpClientFactory)
//         {
//             HttpClientFactory = httpClientFactory;
//         }

//         // static HttpClient httpClient = new HttpClient();

//         const string ServiceBusConnectionString = "Endpoint=sb://orangetwo.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=/fNg8rOhr0y6/1M19nhzbR30zQKr39F2iJ7h8MfnTn0=";
// #if DEBUG
//         // string hostName = "http://localhost:5000";
//         string hostName = "http://localhost:53252";

// #else
//         string hostName = "https://phantomstore.azurewebsites.net";
// #endif

//         string QueueName = "";
//         string Section = "";

//         public IHttpClientFactory HttpClientFactory { get; }





//         public async Task Restart()
//         {

//             Debug.WriteLine("calling logic async");

//             var queueClient = new QueueClient(ServiceBusConnectionString, QueueName);
//             await QueueManager.Remove(QueueName);
//             QueueManager.Set(QueueName, queueClient);

//             Debug.WriteLine("======================================================");
//             Debug.WriteLine("Press ENTER key to exit after receiving all the messages.");
//             Debug.WriteLine("======================================================");

//             RegisterOnMessageHandlerAndReceiveMessages();

//         }

//         Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
//         {
//             Console.WriteLine($"Message handler encountered an exception {exceptionReceivedEventArgs.Exception}.");
//             var context = exceptionReceivedEventArgs.ExceptionReceivedContext;
//             Debug.WriteLine("Exception context for troubleshooting:");
//             Debug.WriteLine($"- Endpoint: {context.Endpoint}");
//             Debug.WriteLine($"- Entity Path: {context.EntityPath}");
//             Debug.WriteLine($"- Executing Action: {context.Action}");
//             return Task.CompletedTask;
//         }

//         void RegisterOnMessageHandlerAndReceiveMessages()
//         {
//             // Configure the message handler options in terms of exception handling, number of concurrent messages to deliver, etc.
//             var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
//             {
//                 // Maximum number of concurrent calls to the callback ProcessMessagesAsync(), set to 1 for simplicity.
//                 // Set it according to how many messages the application wants to process in parallel.
//                 MaxConcurrentCalls = 1,

//                 // Indicates whether the message pump should automatically complete the messages after returning from user callback.
//                 // False below indicates the complete operation is handled by the user callback as in ProcessMessagesAsync().
//                 AutoComplete = false
//             };

//             // Register the function that processes messages.

//             var queueClient = QueueManager.Get(this.QueueName);
//             queueClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);

//         }


//         async Task ProcessMessagesAsync(Message message, CancellationToken token)
//         {
//             // Process the message.
//             Debug.WriteLine($"Received message: SequenceNumber:{message.SystemProperties.SequenceNumber} Body:{Encoding.UTF8.GetString(message.Body)}");

//             var message1 = Encoding.UTF8.GetString(message.Body);
//             await SendMessage(message1);
//             var queueClient = QueueManager.Get(this.QueueName);
//             await queueClient.CompleteAsync(message.SystemProperties.LockToken);
//         }



//         private async Task SendMessage(string message1)
//         {
//             var url = $"{hostName}/capture/{Section}/{message1}";
//             try
//             {
//                 var httpClient = HttpClientFactory.CreateClient(Section);
//                 await httpClient.GetAsync(url);
//                 httpClient.Dispose();
//             }
//             catch (Exception)
//             {

//             }
//         }


//     }



// }
