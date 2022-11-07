using BackEndClassLibrary;
using BlueLight.Main;
using ExchangeLemonCore.Models.SendModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeLemonCore.Controllers
{

    public class WebhookInquirySentTransferService
    {
        public WebhookInquirySentTransferService(
            ReceiveLogCaptureService receiveLogCaptureService,
            ApplicationContext applicationContext,
            TransactionNotificationService notificationService)
        {
            ReceiveLogCapture = receiveLogCaptureService;
            ApplicationContext = applicationContext;
            NotificationService = notificationService;
        }

        public ReceiveLogCaptureService ReceiveLogCapture { get; }

        public ApplicationContext ApplicationContext { get; }
        public TransactionNotificationService NotificationService { get; }

        public async Task InquiryLogic(BtcWebHookSendResult contentObject, Guid sessionId, string event1)
        {
            var address_items = contentObject.outputs.SelectMany(x => x.addresses).ToList();

            //var address = address_items.First();
            await ReceiveLogCapture.SaveAddress(sessionId, address_items, event1);


            var transactionId = contentObject.hash;

            var pendingTransferList = await this.ApplicationContext
                .PendingTransferLists.FirstOrDefaultAsync(x => x.TransactionId == transactionId);
            if (pendingTransferList != null)
            {

                long pendingTransferId = pendingTransferList.Id;
                var newConfirmTransfer = contentObject.confirmations;
                pendingTransferList.ConfirmTransfer = newConfirmTransfer;
                AddLog(pendingTransferList, newConfirmTransfer);
                await this.ApplicationContext.SaveChangesAsync();
            }
        }



        private void AddLog(PendingTransferList pendingTransferList, int newConfirmTransfer)
        {
            
        }

        internal async Task InquiryLogicExt(string transactionId, int confirmations, Guid sessionId)
        {


            //var address = address_items.First();
            await ReceiveLogCapture.SaveAddress(sessionId, new List<string>(), "send");




            var pendingTransferList = await this.ApplicationContext
                .PendingTransferLists
                .Include(x => x.UserProfileDetail)
                .ThenInclude(x => x.UserProfile)
                .FirstOrDefaultAsync(x => x.TransactionId == transactionId);
            if (pendingTransferList != null)
            {

                long pendingTransferId = pendingTransferList.Id;
                var newConfirmTransfer = confirmations;
                pendingTransferList.ConfirmTransfer = newConfirmTransfer;
                AddLog(pendingTransferList, newConfirmTransfer);
                await this.ApplicationContext.SaveChangesAsync();
                var userName = pendingTransferList.UserProfileDetail.UserProfile.username;

                var amount = pendingTransferList.Amount;
                var createdDate = pendingTransferList.CreatedDate;

                await this.NotificationService.NewTransferConfirmStatus(userName, pendingTransferList);
            }
        }
    }
}
