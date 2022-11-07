//using System;
//using System.Threading.Tasks;

//namespace BotWalletWatcher
//{
//    public class NotificationWatcherServerService : INotificationWatcherService
//    {
//        public Task SubmitReceiveExt(int blockChainId, string address)
//        {
//            return Task.Delay(0);
//        }

//        public Task SubmitSendExt(int blockChainId, string transactionId, int confirmations)
//        {
//            return Task.Delay(0);
//        }

        
//    }
//}


////public async Task Execute(string method, string content)
////{
////    var hostname = MainParam.signal_hostname;
////    this._hubConnection = new HubConnectionBuilder()
////        .WithUrl($"{hostname}/signal/watcher")
////        .Build();
////    await _hubConnection.StartAsync();
////    // await _hubConnection.SendAsync("SubmitSyncBot");
////    await _hubConnection.SendAsync(method, content);
////    await _hubConnection.StopAsync();
////}

////internal async Task SubmitReceive(int blockChainId,List<string> addresses)
////{
////    await ConnectionStart();
////    await _hubConnection.SendAsync("Receive", blockChainId,addresses);
////    await ConnectionEnd();
////}

////internal async  Task SubmitSend(int blockChainId,List<string> transactionLists, int confirmations)
////{
////    await ConnectionStart();
////    await _hubConnection.SendAsync("Send", blockChainId,transactionLists, confirmations);
////    await ConnectionEnd();
////}