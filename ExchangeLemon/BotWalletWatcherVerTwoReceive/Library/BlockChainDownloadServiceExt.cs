using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BitcoinLib.Responses;
using BitcoinLib.Services.Coins.Base;
using BotWalletWatcher.Helper;
using BotWalletWatcherLibrary;
using LogLibrary;
using PulseLogic;

namespace BotWalletWatcher
{
    public class BlockChainDownloadServiceExt
    {
        public bool IsTestMode { get; set; }
        public int Seconds { get; set; } = 5;





        private readonly BlockChainLocalPositionService _blockChainLocalPositionService;

        public BlockChainDownloadServiceExt(
                                    FileSaveService fileSaveService,
                                    BlockChainLocalPositionService blockChainLocalPositionService,
                                    PulseService pulseService)
        {
            _blockChainLocalPositionService = blockChainLocalPositionService;
            FileSaveService = fileSaveService;

            PulseService = pulseService;

            CoinService = CoinServiceHelper.GenerateCoinService();
        }



        public ICoinService CoinService { get; }
        public FileSaveService FileSaveService { get; }

        public PulseService PulseService { get; }
        public ICoinService CoinService1 { get; }
        public int CurrentLocalPosition { get; private set; }
        public bool IsTestModeTwo { get; set; }

        internal async Task<GetBlockResponse> InquiryBlockChain(int newLocalPosition)
        {
            await Task.Delay(0);
            var blockHash = CoinService.GetBlockHash(newLocalPosition);
            GetBlockResponse items = CoinService.GetBlock(blockHash);
            return items;
        }


        public async Task<List<TransactionResult>> InquiryTransaction(string txId)
        {

            await Task.Delay(0);
            var rawTransaction = CoinService.GetRawTransaction(txId);
            var hex = rawTransaction.Hex;
            DecodeRawTransactionResponse result1 = CoinService.DecodeRawTransaction(hex);

            var details = result1.Vout.ToList();

            //var type1 = "pubkeyhash";
            //var details1 = details.Where(x => x.ScriptPubKey.Type == type1).ToList();


            List<TransactionResult> details2 = new List<TransactionResult>();
            foreach (var item in details)
            {
                List<TransactionAddress> inputAddress = new List<TransactionAddress>();
                if (item.ScriptPubKey.Addresses != null)
                {
                    inputAddress =
                        item.ScriptPubKey.Addresses
                        .Select(x => new TransactionAddress()
                        {
                            Addreses = x,

                        }).ToList();
                }

                var input = new TransactionResult()
                {
                    Amount = item.Value,
                    Addreses = inputAddress
                };
                details2.Add(input);

            }

            return details2;


        }






        public async Task ExecuteDownload()
        {
            var isContinue = true;




            while (isContinue)
            {
                int localPosition = await _blockChainLocalPositionService.GetLocalPosition();
                uint nodePosition = this.CoinService.GetBlockCount();

                CurrentLocalPosition = localPosition;

                Console3.WriteLine($"Local Position: {localPosition}");
                Console3.WriteLine($"Node Position: {nodePosition}");


                if (CurrentLocalPosition < nodePosition)
                {
                    Console3.WriteLine($"Current Position: {CurrentLocalPosition}");
                    if (!IsTestMode)
                    {
                        CurrentLocalPosition++;
                        await ChainLogic(CurrentLocalPosition);
                    }
                }
                else
                {

                    await Task.Delay(5000);
                }
            }
        }

        public async Task ChainLogic(int blockChainNumber)
        {
            var getBlockResponse =
                 await this.InquiryBlockChain(blockChainNumber);
            var transactionLists = getBlockResponse.Tx;
            Console3.WriteLine($"Transaction list total: {transactionLists.Count}");
            var transactionDetailLists = new List<TransactionResult>();
            foreach (var item in transactionLists)
            {
                try
                {
                    Console3.WriteLine($"Inquiry item: {item}");
                    var transaction = await this.InquiryTransaction(item);
                    transactionDetailLists.AddRange(transaction);
                }
                catch (Exception)
                {
                    throw new ArgumentException("node not detected");
                }

            }


            int confirmations = getBlockResponse.Confirmations;


            var watcherBlockChain = new WatcherBlockChain()
            {
                BlockChainNumber = blockChainNumber,
                Confirmations = confirmations,
                IsFinish = false
            };


            Console3.WriteLine($"[start] Send items");
            var sentItems = await SendLogicPersistant(blockChainNumber, transactionLists, confirmations);
            Console3.WriteLine($"[end] Send items");



            var addresses = transactionDetailLists.SelectMany(x => x.Addreses.Select(y => y.Addreses)).ToList();
            Console3.WriteLine($"[start] Receive items");
            var receiveItems = await ReceiveLogicPersistant(blockChainNumber, addresses);
            Console3.WriteLine($"[end] Receive items");


            var result = new WatcherBlockPersistant()
            {
                WatcherBlockChain = watcherBlockChain,
                SentItems = sentItems,
                ReceiveItems = receiveItems,

            };


            var source = $"{blockChainNumber}.txt";
            var outputFolder = "output";
            var target = Path.Combine(outputFolder, $"{blockChainNumber}.zip");
            // var target = $"{outputFolder}//{blockChainNumber}.zip";

            FileSaveService.Save(source, result);
            FileSaveService.EnsureFolder(outputFolder);
            FileSaveService.Archive(source, target);
            FileSaveService.Delete(source);

            await FileHelper.SaveAsync("last_block", blockChainNumber.ToString());
        }


        public async Task<List<WatcherTransaction>> SendLogicPersistant(int blockChainId, List<string> transactionLists, int confirmations)
        {
            var output = new List<WatcherTransaction>();
            foreach (var item in transactionLists)
            {
                Console3.WriteLine($"SendLogic {item} - [start]");
                await this.PulseService.Send("bot-wallet-watcher-receive-transaction");

                {
                    var transactionId = item;
                    var input = new WatcherTransaction()
                    {
                        TransactionId = transactionId,
                        Confirmations = confirmations,
                        BlockChainId = blockChainId,
                        IsSend = false
                    };
                    output.Add(input);
                }

                Console3.WriteLine($"SendLogic {item} - [end]");
            }
            return output;
        }


        public async Task<List<WatcherAddressItem>> ReceiveLogicPersistant
            (int blockChainId, List<string> addresses)
        {
            var output = new List<WatcherAddressItem>();
            foreach (var item in addresses)
            {
                Console3.WriteLine($"ReceiveLogic {item}-[start]");
                await this.PulseService.Send("bot-wallet-watcher-receive-address");
                {
                    var address = item;
                    var input = new WatcherAddressItem()
                    {
                        Address = address,
                        BlockChainId = blockChainId,
                        IsSend = false
                    };
                    output.Add(input);
                }
                Console3.WriteLine($"ReceiveLogic {item}-[end]");

            }
            return output;
        }


    }
}
