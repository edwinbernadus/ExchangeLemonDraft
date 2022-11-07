using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BlueLight.Main;
using BotWalletWatcher;
using BotWalletWatcherLibrary;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
//
//using Microsoft.AspNetCore.Cors;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

namespace BlueLight.Main
{
    public class BlockChainStatusController : Controller
    {
        private readonly SignalDashboard _signalDashboard;

        public BlockChainStatusController(BlockContext blockContext,
                    WatcherService watcherService,
                    BlockChainPersistantService blockChainPersistantService,
                    IConfiguration configuration, SignalDashboard signalDashboard)
        {
            _signalDashboard = signalDashboard;
            BlockContext = blockContext;
            WatcherService = watcherService;
            BlockChainPersistantService = blockChainPersistantService;
            Configuration = configuration;
        }

        public BlockContext BlockContext { get; }
        public WatcherService WatcherService { get; }
        public BlockChainPersistantService BlockChainPersistantService { get; }
        public IConfiguration Configuration { get; }


        // http://localhost:5000/blockchainstatus/inquiry
        // http://win8.southeastasia.cloudapp.azure.com/blockchainstatus/inquiry
        public async Task<int> Inquiry()
        {
            int result = await this.BlockChainPersistantService.InquiryLastBlock();
            return result;
        }



        [HttpPost]
        public async Task<int> Upload()
        {
            var requestBodyStream = Request.Body;
            var requestBodyText = new StreamReader(requestBodyStream).ReadToEnd();
            var input = JsonConvert.DeserializeObject<WatcherBlockPersistant>(requestBodyText);

            foreach (var item in input.ReceiveItems)
            {
                var blockChainId = item.BlockChainId;
                var address = item.Address;
                await this.BlockChainPersistantService.SaveLogReceive(blockChainId, address);
                await WatcherService.CompareReceiveOneItem(address);
            }

            foreach (var item in input.SentItems)
            {
                var blockChainId = item.BlockChainId;
                var transactionId = item.TransactionId;
                var confirmations = item.Confirmations;
                await this.BlockChainPersistantService.SaveLogSend(blockChainId, transactionId, confirmations);
                await WatcherService.CompareSendOneItem(transactionId, confirmations);
            }

            var watcherBlockChain = input.WatcherBlockChain;
            this.BlockContext.WatcherBlockChains.Add(watcherBlockChain);
            await this.BlockContext.SaveChangesAsync();

            var output = watcherBlockChain.BlockChainNumber;
            try
            {
                await _signalDashboard.Submit("blockchain-uploaded");
            }
            catch (Exception ex)
            {
                var m = ex.Message;
            }

            return output;
        }

    }
}