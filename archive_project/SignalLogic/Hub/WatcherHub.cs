//using BotWalletWatcher;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BlueLight.Main
{



    public class WatcherHub : Hub
    {

        //public WatcherHub(WatcherService watcherService, BlockChainDownloadService blockChainService)
        //{
        //    WatcherService = watcherService;
        //    BlockChainService = blockChainService;
        //}

        //public WatcherService WatcherService { get; }
        //public BlockChainDownloadService BlockChainService { get; }

        public static TaskCompletionSource<string> tcs = Generate();

        public static void ResetTcs()
        {
            try
            {
                tcs.SetCanceled();
            }
            catch (Exception)
            {

            }

            try
            {
                tcs.Task.Dispose();
            }
            catch (Exception)
            {

            }

            tcs = Generate();

        }

        static TaskCompletionSource<string> Generate()
        {
            var tcs = new TaskCompletionSource<string>();
            const int timeoutMs = 3000;
            var ct = new CancellationTokenSource(timeoutMs);
            ct.Token.Register(() => tcs.TrySetCanceled(), useSynchronizationContext: false);
            return tcs;
        }
        public void InquiryTransfer(string transactionId)
        {
            tcs.SetResult(transactionId);
        }

        //public async Task Send(int blockChainId, string transactionId, int confirmations)
        //{
        //    await this.BlockChainService.SaveLogSend(blockChainId,transactionId, confirmations);
        //    //await WatcherService.SaveLogSendAsync(blockChainId, transactionLists, confirmations);
        //    //await WatcherService.CompareSend(transactionLists, confirmations);
        //    await WatcherService.CompareSendOneItem(transactionId, confirmations);
        //}

        //public async Task Receive(int blockChainId, string address)
        //{
        //    await this.BlockChainService.SaveLogReceive(blockChainId,address);
        //    //await WatcherService.SaveLogReceiveAsync(blockChainId, addresses);
        //    //await WatcherService.CompareReceive(addresses);
        //    await WatcherService.CompareReceiveOneItem(address);
        //}


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