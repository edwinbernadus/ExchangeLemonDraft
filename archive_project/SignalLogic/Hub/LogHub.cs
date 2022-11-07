using Microsoft.AspNetCore.SignalR;

namespace BlueLight.Main
{
    public class LogHub : Hub
    {

    }
}

//public async Task Send(int blockChainId,List<string> transactionLists, int confirmations)
//{
//    await WatcherService.SaveLogSendAsync(blockChainId, transactionLists, confirmations);
//    await WatcherService.CompareSend(transactionLists, confirmations);
//}

//public async Task Receive(int blockChainId,List<string> addresses)
//{
//    await WatcherService.SaveLogReceiveAsync(blockChainId, addresses);
//    await WatcherService.CompareReceive(addresses);
//}

//public async Task Test(string input1)
//{

//}