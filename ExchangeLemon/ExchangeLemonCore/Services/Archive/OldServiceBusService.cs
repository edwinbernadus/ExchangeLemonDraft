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
//         static QueueClient testOneQueueClient;
//         static QueueClient sendQueueClient;
//         static QueueClient receiveQueueClient;
//         static HttpClient httpClient = new HttpClient();
//         public static bool IsServiceBusStarted { get; private set; }
//         public static bool IsInit { get; private set; }


//         // static int Total = 0;

//         public async Task Restart()
//         {
//             try
//             {
//                 if (testOneQueueClient != null)
//                 {
//                     await testOneQueueClient.CloseAsync();
//                 }
//             }
//             catch (Exception)
//             {


//             }

//             testOneQueueClient = null;
//             Run();
//         }

//         public void Start()
//         {
//             if (IsInit == false)
//             {
//                 Run();
//             }
//         }

//         public void Run()
//         {
//             IsServiceBusStarted = true;
//             Debug.WriteLine("calling logic async");
//             const string ServiceBusConnectionString = "Endpoint=sb://orangetwo.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=/fNg8rOhr0y6/1M19nhzbR30zQKr39F2iJ7h8MfnTn0=";
//             string QueueName = "test-one";
// #if DEBUG
//             QueueName = "test-one-dev";
// #endif


//             testOneQueueClient = new QueueClient(ServiceBusConnectionString, QueueName);
//             sendQueueClient = new QueueClient(ServiceBusConnectionString, "send");
//             receiveQueueClient = new QueueClient(ServiceBusConnectionString, "receiver");

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
//             testOneQueueClient.RegisterMessageHandler(ProcessMessagesTestAsync, messageHandlerOptions);
//             sendQueueClient.RegisterMessageHandler(ProcessMessagesSendAsync, messageHandlerOptions);
//             receiveQueueClient.RegisterMessageHandler(ProcessMessagesReceiveAsync, messageHandlerOptions);

//             //     sendQueueClient.RegisterMessageHandler((message, y) =>
//             //     {
//             //         return new Task(async () =>
//             //        {
//             //            var message1 = Encoding.UTF8.GetString(message.Body);
//             //            await SendSignalTest(message1, "send");
//             //            await sendQueueClient.CompleteAsync(message.SystemProperties.LockToken);
//             //        });
//             //     }, messageHandlerOptions);

//             //     receiveQueueClient.RegisterMessageHandler((message, y) =>
//             //    {
//             //        return new Task(async () =>
//             //       {
//             //           var message1 = Encoding.UTF8.GetString(message.Body);
//             //           await SendSignalTest(message1, "receive");
//             //           await receiveQueueClient.CompleteAsync(message.SystemProperties.LockToken);
//             //       });
//             //    }, messageHandlerOptions);
//         }


//         async Task ProcessMessagesTestAsync(Message message, CancellationToken token)
//         {
//             // Process the message.
//             Debug.WriteLine($"Received message: SequenceNumber:{message.SystemProperties.SequenceNumber} Body:{Encoding.UTF8.GetString(message.Body)}");

//             var message1 = Encoding.UTF8.GetString(message.Body);
//             await SendItemTest(message1);
//             await testOneQueueClient.CompleteAsync(message.SystemProperties.LockToken);
//         }

//         async Task ProcessMessagesSendAsync(Message message, CancellationToken token)
//         {
//             // Process the message.
//             Debug.WriteLine($"Received message: SequenceNumber:{message.SystemProperties.SequenceNumber} Body:{Encoding.UTF8.GetString(message.Body)}");

//             var message1 = Encoding.UTF8.GetString(message.Body);
//             await SendItemSend(message1);
//             await testOneQueueClient.CompleteAsync(message.SystemProperties.LockToken);
//         }

//         async Task ProcessMessagesReceiveAsync(Message message, CancellationToken token)
//         {
//             // Process the message.
//             Debug.WriteLine($"Received message: SequenceNumber:{message.SystemProperties.SequenceNumber} Body:{Encoding.UTF8.GetString(message.Body)}");

//             var message1 = Encoding.UTF8.GetString(message.Body);
//             await SendItemReceive(message1);
//             await testOneQueueClient.CompleteAsync(message.SystemProperties.LockToken);
//         }

//         private async Task SendItemTest(string message1)
//         {
//             var hostName = "https://phantomstore.azurewebsites.net";
// #if DEBUG
//             hostName = "http://localhost:5000";
// #endif

//             var section = "signal";
//             var url = $"{hostName}/capture/{section}/{message1}";
//             try
//             {
//                 await httpClient.GetAsync(url);
//             }
//             catch (Exception)
//             {

//             }
//         }

//         private async Task SendItemSend(string message1)
//         {
//             var hostName = "https://phantomstore.azurewebsites.net";
// #if DEBUG
//             hostName = "http://localhost:5000";
// #endif

//             var section = "send";
//             var url = $"{hostName}/capture/{section}/{message1}";
//             try
//             {
//                 await httpClient.GetAsync(url);
//             }
//             catch (Exception)
//             {

//             }
//         }

//         private async Task SendItemReceive(string message1)
//         {
//             var hostName = "https://phantomstore.azurewebsites.net";
// #if DEBUG
//             hostName = "http://localhost:5000";
// #endif

//             var section = "receive";
//             var url = $"{hostName}/capture/{section}/{message1}";
//             try
//             {
//                 await httpClient.GetAsync(url);
//             }
//             catch (Exception)
//             {

//             }
//         }
//     }



// }
