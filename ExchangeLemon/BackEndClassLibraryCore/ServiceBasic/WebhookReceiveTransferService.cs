using BlueLight.Main;
using ExchangeLemonCore.Models.ReceiveModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeLemonCore.Controllers
{
    public class WebhookReceiveTransferService
    {
        public WebhookReceiveTransferService(BitcoinSyncJob bitcoinSyncJob,
           ReceiveLogCaptureService receiveLogCapture,
           TransactionNotificationService notificationService)
        {
            BitcoinSyncJob = bitcoinSyncJob;
            ReceiveLogCapture = receiveLogCapture;
            NotificationService = notificationService;
        }

        public BitcoinSyncJob BitcoinSyncJob { get; }
        public ReceiveLogCaptureService ReceiveLogCapture { get; }
        public TransactionNotificationService NotificationService { get; }

        public async Task ReceiveLogic(BtcWebHookReceiveResult contentObject, Guid sessionId, string event1)
        {
            var address_items = contentObject.outputs.SelectMany(x => x.addresses).ToList();
            await ReceiveLogCapture.SaveAddress(sessionId, address_items, event1);

            foreach (var address in address_items)
            {
                var isValid = await this.BitcoinSyncJob.ExecuteFromAddressAsync(address);
                if (isValid)
                {
                    await this.NotificationService.NewDepositFromAddress(address, -1m, "btc");
                }

            }
        }
    }
}
