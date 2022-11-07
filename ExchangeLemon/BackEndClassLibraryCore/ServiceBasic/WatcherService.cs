using ExchangeLemonCore.Controllers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueLight.Main
{
    public class WatcherService
    {
        public WatcherService(ApplicationContext applicationContext,
            LoggingExtContext loggingExtContext,
            BitcoinSyncJob bitcoinSyncJob, TransactionNotificationService notificationService,
            WebhookInquirySentTransferService webhookInquirySentTransferService)
        {
            ApplicationContext = applicationContext;
            BitcoinSyncJob = bitcoinSyncJob;
            NotificationService = notificationService;
            WebhookInquirySentTransferService = webhookInquirySentTransferService;
            LoggingExtContext = loggingExtContext;
        }

        //[Obsolete]
        //public async Task SaveLogSendAsync(int blockChainId, List<string> transactionLists, int confirmations)
        //{
        //    var details = transactionLists.Select(x => new WatcherSendLogDetail()
        //    {
        //        TransactionId = x
        //    }).ToList();
        //    var log = new WatcherSendLog()
        //    {
        //        BlockChainId = blockChainId,
        //        Confirmations = confirmations,
        //        Details = details,
        //    };
        //    this.LoggingExtContext.WatcherSendLogs.Add(log);
        //    await LoggingExtContext.SaveChangesAsync();
        //}

        //[Obsolete]
        //public async Task SaveLogReceiveAsync(int blockChainId, List<string> addresses)
        //{
        //    var details = addresses.Select(x => new WatcherReceiveLogDetail()
        //    {
        //        Address = x
        //    }).ToList();
        //    var log = new WatcherReceiveLog()
        //    {
        //        BlockChainId = blockChainId,
        //        Details = details,
        //    };
        //    this.LoggingExtContext.WatcherReceiveLogs.Add(log);
        //    await LoggingExtContext.SaveChangesAsync();
        //}

        public ApplicationContext ApplicationContext { get; }
        public BitcoinSyncJob BitcoinSyncJob { get; }
        public TransactionNotificationService NotificationService { get; }
        public WebhookInquirySentTransferService WebhookInquirySentTransferService { get; }
        public LoggingExtContext LoggingExtContext { get; }

        async Task CompareSend(List<string> transactionLists, int confirmations)
        {
            List<string> items = await InquirySend();
            var filtered = transactionLists.Join(items, x => x, x => x, (x, y) => new { Items = x }).ToList();
            var items2 = filtered.Select(x => x.Items).ToList();

            var sessionGuid = Guid.NewGuid();
            foreach (var item in items2)
            {
                var transactionId = item;

                //await Logic(item, confirmations);
                await this.WebhookInquirySentTransferService
                    .InquiryLogicExt(transactionId, confirmations, sessionGuid);
            }
        }

        public async Task CompareSendOneItem(string transactionList, int confirmations)
        {
            List<string> items = await InquirySend();
            var items2 = items.Where(x => x == transactionList).ToList();


            var sessionGuid = Guid.NewGuid();
            foreach (var item in items2)
            {
                var transactionId = item;

                //await Logic(item, confirmations);
                await this.WebhookInquirySentTransferService
                    .InquiryLogicExt(transactionId, confirmations, sessionGuid);
            }
        }

        async Task CompareReceive(List<string> addresses)
        {
            List<string> items = await InquiryReceive();
            var filtered = addresses.Join(items, x => x, x => x, (x, y) => new { Items = x }).ToList();
            var items2 = filtered.Select(x => x.Items).ToList();
            foreach (var address in items2)
            {
                var isValid = await this.BitcoinSyncJob.ExecuteFromAddressAsync((string)address);
                if (isValid)
                {
                    await this.NotificationService.NewDepositFromAddress((string)address, -1m, "btc");
                }
            }
        }

        public async Task CompareReceiveOneItem(string addresses)
        {
            List<string> items = await InquiryReceive();
            var items2 = items.Where(x => x == addresses).ToList();

            foreach (var address in items2)
            {
                var isValid = await this.BitcoinSyncJob.ExecuteFromAddressAsync((string)address);
                if (isValid)
                {
                    await this.NotificationService.NewDepositFromAddress((string)address, -1m, "btc");
                }
            }
        }



        async Task<List<string>> InquirySend()
        {
            var items = await ApplicationContext.PendingTransferLists
                .Where(x => x.ConfirmTransfer == 0).ToListAsync();
            List<string> output = items.Select(x => x.TransactionId).ToList();
            return output;
        }




        async Task<List<string>> InquiryReceive()
        {
            var items = await ApplicationContext.UserProfileDetails.Where(x => x.CurrencyCode == "btc" &&
                string.IsNullOrEmpty(x.PublicAddress) == false).ToListAsync();
            var output = items.Select(x => x.PublicAddress).ToList();
            return output;
        }
    }
}

