//using BlueLight.Main;
//using BotWalletWatcher;
//using ExchangeLemonCore.Models.ReceiveModels;
//using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ExchangeLemonCore.Controllers
//{
//    public class WatcherController : Controller
//    {
//        public string Hello()
//        {
//            return "Hi";
//        }

//        [HttpPost]
//        public string HelloPost()
//        {
//            return "Hi-Post";
//        }

//        public WatcherController(WatcherService watcherService, BlockChainDownloadService blockChainDownloadService)
//        {
//            WatcherService = watcherService;
//            BlockChainService = blockChainDownloadService;
//        }

//        public WatcherService WatcherService { get; }
//        public BlockChainDownloadService BlockChainService { get; }

//        public async Task receive(int id, string address)
//        {
//            var blockChainId = id;
//            await this.BlockChainService.SaveLogReceive(blockChainId, address);
//            //await WatcherService.SaveLogReceiveAsync(blockChainId, addresses);
//            //await WatcherService.CompareReceive(addresses);
//            await WatcherService.CompareReceiveOneItem(address);

//        }


//        public async Task send(int id, int confirmations, string transactionId)
//        {
//            var blockChainId = id;
//            await this.BlockChainService.SaveLogSend(blockChainId, transactionId, confirmations);
//            //await WatcherService.SaveLogSendAsync(blockChainId, transactionLists, confirmations);
//            //await WatcherService.CompareSend(transactionLists, confirmations);
//            await WatcherService.CompareSendOneItem(transactionId, confirmations);
//        }


//        [HttpPost]
//        // https://echochain.azurewebsites.net/Watcher/PostReceive
//        public async Task<string> PostReceive()
//        {
//            var content = "";
//            try
//            {

//                using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
//                {
//                    content = await reader.ReadToEndAsync();

//                    var item = JsonConvert.DeserializeObject<ReceiveMessage>(content);

//                    var blockChainId = item.blockChainId;
//                    var address = item.address;
                    
//                    await this.BlockChainService.SaveLogReceive(blockChainId, address);
//                    await WatcherService.CompareReceiveOneItem(address);

//                }
//            }
//            catch (Exception)
//            {

//            }
//            return content;
//        }



//        [HttpPost]
//        // https://echochain.azurewebsites.net/Watcher/PostSend
//        public async Task<string> PostSend()
//        {
//            var content = "";
//            try
//            {

//                using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
//                {
//                    content = await reader.ReadToEndAsync();

//                    var item = JsonConvert.DeserializeObject<SendMessage>(content);

//                    var blockChainId = item.blockChainId;
//                    var transactionId = item.transactionId;
//                    var confirmations = item.confirmations;
//                    await this.BlockChainService.SaveLogSend(blockChainId, transactionId, confirmations);
//                    await WatcherService.CompareSendOneItem(transactionId, confirmations);
                    

//                }
//            }
//            catch (Exception)
//            {

//            }
//            return content;
//        }


//    }
//}
