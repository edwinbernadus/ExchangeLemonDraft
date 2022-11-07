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
//    public class WatcherExtController : Controller
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

//        public WatcherExtController(
//            WatcherService watcherService,
//            BlockChainDownloadService blockChainService)
//        {
//            WatcherService = watcherService;
//            BlockChainService = blockChainService;
//        }

//        public WatcherService WatcherService { get; }
//        public BlockChainDownloadService BlockChainService { get; }

//        [HttpPost]
//        // https://echochain.azurewebsites.net/WatcherExt/Receive
//        public async Task<string> Receive()
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
//        // https://echochain.azurewebsites.net/WatcherExt/Send
//        public async Task<string> Send()
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
