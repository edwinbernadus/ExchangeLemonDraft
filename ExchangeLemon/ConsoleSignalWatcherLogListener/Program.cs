using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace ConsoleSignalPlayground
{
    class Program
    {


        static void Main(string[] args)
        {
            Console.WriteLine("start log transaction-ver2");
            NewMethod();
            Console.ReadLine();
        }

        private static Task NewMethod()
        {
            return Caller();
        }

        static async Task Caller()
        {
            try
            {


                await Execute();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static async Task Execute()
        {
            var factory = new Logic();
            await factory.Execute();
            // await m.ExecuteOne();

        }

        
    }
}



//static async Task TestSubmitQueue()
//{
//    var m = "DefaultEndpointsProtocol=https;AccountName=echochainstorage;AccountKey=FtYpsjo+0Bet/FH6H8mvZIpiVLQuH+yUp0aAJiNpl4bEzMP5MBt8nDLOy7MtHnM1SQ7gZzXKvWH2xK4e1Bv6Pw==;EndpointSuffix=core.windows.net";
//    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(m);
//    CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

//    // Retrieve storage account from connection string.

//    // Retrieve a reference to a container.
//    CloudQueue queue = queueClient.GetQueueReference("send");

//    // Create the queue if it doesn't already exist
//    await queue.CreateIfNotExistsAsync();

//    CloudQueueMessage message = new CloudQueueMessage("Hello, World");
//    await queue.AddMessageAsync(message);


//}