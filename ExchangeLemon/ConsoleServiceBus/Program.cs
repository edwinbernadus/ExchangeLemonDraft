using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Hello World!");
            Test();
            Console.ReadLine();
        }
        static IQueueClient queueClient;
        static async Task Test()
        {
            // Microsoft.Azure.ServiceBus. ServiceBus.Messaging.
            // MessagingFactory
            var s = new ServiceBusConnectionStringBuilder("");
            var m = new MessageReceiver(s);
            var m2 = await m.ReceiveAsync();

            // Microsoft.Azure.ServiceBus.Message


            var p = new MessageSender(s);


            await p.SendAsync(new Message());


            //const string sqlConnString = "Data Source=.; Initial Catalog=HangfireTest; Integrated Security=True; MultipleActiveResultSets=False;";
            const string serviceBusConnString = "Endpoint=sb://orangetwo.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=/fNg8rOhr0y6/1M19nhzbR30zQKr39F2iJ7h8MfnTn0=";
            string queueName = "test-one";
#if DEBUG
            queueName = "test-one-dev";
#endif

            const string ServiceBusConnectionString = serviceBusConnString;


            queueClient = new QueueClient(ServiceBusConnectionString, queueName);



            // Send messages.
            await SendMessagesAsync();
        }

        static async Task SendMessagesAsync()
        {
            try
            {
                var i = 0;
                while (1 == 1)
                {
                    // for (var i = 0; i < numberOfMessagesToSend; i++)
                    {
                        // Create a new message to send to the queue.
                        i++;
                        string messageBody = $"Message {i}";
                        var message = new Message(Encoding.UTF8.GetBytes(messageBody));

                        // Write the body of the message to the console.
                        Console.WriteLine($"Sending message: {messageBody}");

                        // Send the message to the queue.
                        await queueClient.SendAsync(message);

                        await Task.Delay(2000);
                    }
                }

            }
            catch (Exception exception)
            {
                Console.WriteLine($"{DateTime.Now} :: Exception: {exception.Message}");
            }
        }
    }
}
