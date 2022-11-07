//using Microsoft.WindowsAzure.Storage;
//using Microsoft.WindowsAzure.Storage.Queue;
//using Newtonsoft.Json;
//using PulseLogic;
//using System.Threading.Tasks;

//namespace BotWalletWatcher
//{
//    public class NotificationWatcherClientServiceExt : INotificationWatcherService
//    {
//        public NotificationWatcherClientServiceExt(PulseService pulseService)
//        {
//            PulseService = pulseService;
//        }
//        public PulseService PulseService { get; }

//        CloudQueue GenerateClient(string queueName)
//        {

//            var connString = MainParam.StorageConnString;
//            var storageAccount = CloudStorageAccount.Parse(connString);
//            var queueClient = storageAccount.CreateCloudQueueClient();

//            var queue = queueClient.GetQueueReference(queueName);

//            return queue;

//        }


//        async Task SubmitMessage(string queueName, string inputMessage)
//        {
//            var client = GenerateClient(queueName);
//            var message = new CloudQueueMessage(inputMessage);
//            await client.AddMessageAsync(message);


//        }


//        public async Task SubmitSendExt(int blockChainId, string transactionId, int confirmations)
//        {
//            var message = new SendMessage()
//            {
//                blockChainId = blockChainId,
//                transactionId = transactionId,
//                confirmations = confirmations
//            };
//            var m = JsonConvert.SerializeObject(message);
//            string QueueName = MainParam.GetQueue("send");
//            if (MainParam.IsQueueDevMode)
//            {
//                QueueName = MainParam.GetQueue("dev -send");
//            }
//            await SubmitMessage(QueueName, m);
//            await this.PulseService.Send("bot-wallet-watcher-send-transaction");
//        }

//        public async Task SubmitReceiveExt(int blockChainId, string address)
//        {
//            var message = new ReceiveMessage()
//            {
//                blockChainId = blockChainId,
//                address = address,
//            };
//            var m = JsonConvert.SerializeObject(message);
//            string QueueName = MainParam.GetQueue("receive");
//            if (MainParam.IsQueueDevMode)
//            {
//                QueueName = MainParam.GetQueue("dev-receive");
//            }
//            await SubmitMessage(QueueName, m);
//            await this.PulseService.Send("bot-wallet-watcher-send-address");
//        }


//    }


//}
