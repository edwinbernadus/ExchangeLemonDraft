using BackEndClassLibrary;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.DependencyInjection;

namespace BlueLight.Main.Tests
{
    public class UnitTestOutgoingBtc
    {
    


        [Fact]
        public async Task TestConfirmZero()
        {
            var service = ServiceHelper.Generate();
            var context = service.GetService<ApplicationContext>();

            var unitTestRemittance = new UnitTestRemittanceSendTransferTwoItems()
            {
                service = service
            };
            await unitTestRemittance.TestSendTransferTwoItems();
            //var service = s2.serviceProvider;
            //var BtcCloudService = s2.BtcCloudServiceRegisterNotification;
            //var context = s2.context;
            var output = context != null;
            Assert.True(output);

            var total = await context.SentItems.CountAsync();
            Assert.Equal(2, total);

            {
                var totalSent = await context.SentItems.Where(x => x.PendingTransferList.StatusTransfer == "sent").CountAsync();
                Assert.Equal(2, totalSent);
                var totalDelivered = await context.SentItems.Where(x => x.PendingTransferList.StatusTransfer == "delivered").CountAsync();
                Assert.Equal(0, totalDelivered);
            }


            IRemittanceOutgoingValidatorService remittanceOutgoingValidatorService = service.GetService<IRemittanceOutgoingValidatorService>();
            var item = await context.PendingTransferLists.FirstAsync();
            long pendingTransferId = item.Id;


            PopulateFakeConfirm(remittanceOutgoingValidatorService,0);

             await remittanceOutgoingValidatorService.Execute(pendingTransferId,true);

            Assert.Equal(0, item.ConfirmTransfer);
            Assert.Single(item.PendingTransferListHistories);

            var pendingTransferListHistory = item.PendingTransferListHistories.First();
            

            Assert.Equal("no_change-0", pendingTransferListHistory.Content);
            Assert.True(pendingTransferListHistory.IsManual);

        }

       void PopulateFakeConfirm(IRemittanceOutgoingValidatorService remittanceOutgoingValidatorService,int input)
        {
            if (remittanceOutgoingValidatorService is DummyRemittanceOutgoingValidatorService validator)
            {
                validator.fakeOutputConfirm = input;
            }
        }


        [Fact]
        public async Task<(IServiceProvider service, ApplicationContext context) >TestConfirmOne()
        {
            var service = ServiceHelper.Generate();
            var context = service.GetService<ApplicationContext>();
            var unitTestRemittance = new UnitTestRemittanceSendTransferTwoItems()
            {
                service = service
            };
            await unitTestRemittance.TestSendTransferTwoItems();
            //IServiceProvider service = s2.serviceProvider;
            //var BtcCloudService = s2.BtcCloudServiceRegisterNotification;
            //var context = s2.context;
            var output = context != null;
            Assert.True(output);

            var total = await context.SentItems.CountAsync();
            Assert.Equal(2, total);

            {
                var totalSent = await context.SentItems.Where(x => x.PendingTransferList.StatusTransfer == "sent").CountAsync();
                Assert.Equal(2, totalSent);
                var totalDelivered = await context.SentItems.Where(x => x.PendingTransferList.StatusTransfer == "delivered").CountAsync();
                Assert.Equal(0, totalDelivered);
            }


            var item = await context.PendingTransferLists.FirstAsync();
            
            long pendingTransferId = item.Id;
            await LogicOneConfirm(service, context,pendingTransferId);

            Assert.Equal(1, item.ConfirmTransfer);
            var log = item.PendingTransferListHistories.First();
            Assert.Equal(1, log.ConfirmTransfer);
            Assert.Single(item.PendingTransferListHistories);


            var pendingTransferListHistory = item.PendingTransferListHistories.First();
            Assert.Equal("update_confirm-1", pendingTransferListHistory.Content);
            Assert.True(pendingTransferListHistory.IsManual);

            (IServiceProvider service, ApplicationContext context) result = (service, context);
            return result;
        }

        
        [Fact]
        public async Task TestConfirmOneAgain()
        {
            var s = await TestConfirmOne();

            var context = s.context;
            var service = s.service;
            var item = await context.PendingTransferLists.FirstAsync();
            

            long pendingTransferId = item.Id;
            await LogicOneConfirm(service, context, pendingTransferId);

            Assert.Equal(1, item.ConfirmTransfer);
            var log = item.PendingTransferListHistories.First();
            Assert.Equal(1, log.ConfirmTransfer);
            Assert.Equal(2,item.PendingTransferListHistories.Count());


            var pendingTransferListHistory = item.PendingTransferListHistories.Last();
            Assert.Equal("no_change-1", pendingTransferListHistory.Content);
            Assert.True(pendingTransferListHistory.IsManual);

        }

        async Task LogicOneConfirm(IServiceProvider service,ApplicationContext context,long pendingTransferId)
        {
            var s3 = service.GetService<IRemittanceOutgoingValidatorService>();
            
            PopulateFakeConfirm(s3, 1);
            await s3.Execute(pendingTransferId,true);

        }


        //[Fact]
        //public async Task  TestConfirmFinish()
        //{
        //    var service = ServiceHelper.Generate();
        //    var unitTestRemittance = new UnitTestRemittanceSendTransferTwoItems()
        //    {
        //        service = service
        //    };
        //    var context = service.GetService<ApplicationContext>();
        //    await unitTestRemittance.TestSendTransferTwoItems();
        //    //var BtcCloudService = s2.BtcCloudServiceRegisterNotification;
        //    //var context = s2.context;
        //    var output = context != null;
        //    Assert.True(output);

        //    var total = await context.SentItems.CountAsync();
        //    Assert.Equal(2, total);

        //    {
        //        var totalSent = await context.SentItems.Where(x => x.PendingTransferList.StatusTransfer == "sent").CountAsync();
        //        Assert.Equal(2, totalSent);
        //        var totalDelivered = await context.SentItems.Where(x => x.PendingTransferList.StatusTransfer == "delivered").CountAsync();
        //        Assert.Equal(0, totalDelivered);
        //    }


        //    DevBtcCloudServiceRegisterNotification BtcCloudService = null;
        //    var s3 = BtcCloudService as DevBtcCloudServiceRegisterNotification;
        //    await CheckNotificationTransfer(context, s3);
        //    await CheckNotificationTransfer(context,s3);


        //    {
        //        var totalSent = await context.SentItems.Where(x => x.PendingTransferList.StatusTransfer == "sent").CountAsync();
        //        Assert.Equal(0, totalSent);
        //        var totalDelivered = await context.SentItems.Where(x => x.PendingTransferList.StatusTransfer == "delivered").CountAsync();
        //        Assert.Equal(2, totalDelivered);
        //        //Assert.Equal(3, totalDelivered);
        //    }
            
        //}

        //private async Task CheckNotificationTransfer(ApplicationContext context, DevBtcCloudServiceRegisterNotification service)
        //{
        //    var transferId = service.QueueNotifyTransfer();
        //    Assert.NotNull(transferId);
            
        //    var items = await context.PendingTransferLists.ToListAsync();
        //    var item = items.FirstOrDefault(x => x.StatusTransfer == "sent" &&
        //    x.TransactionId == transferId);
        //    item.StatusTransfer = "delivered";
        //    await context.SaveChangesAsync();
        //}

        [Fact]

        public async Task TestConfirmAllCheck()
        {
            var service = ServiceHelper.Generate();
            var unitTestRemittance = new UnitTestRemittanceSendTransferTwoItems()
            {
                service = service
            };
            await unitTestRemittance.TestSendTransferTwoItems();
            

            var checkAllService = service.GetService<RemittanceOutgoingCheckAllManualService>();
            var context = service.GetService<ApplicationContext>();

            var validator = service.GetService<IRemittanceOutgoingValidatorService>();
            PopulateFakeConfirm(validator, 2);

            var id = 1;
            //1st checking all items
            
            {
                var result = await checkAllService.Execute(id);
                var items = await context.PendingTransferLists
                    .SelectMany(x => x.PendingTransferListHistories).ToListAsync();
                var total = items.Count();
                var totalManual = items.Where(x => x.IsManual).Count();
                Assert.Equal("1-2", result);
                Assert.Equal(2, total);
                Assert.Equal(2, totalManual);
            }

            {
                await Task.Delay(500);
                //checking first item
                var pendingFirstItem = await context.PendingTransferLists.FirstAsync();
                
                await validator.Execute(pendingFirstItem.Id,true);
                var items = await context.PendingTransferLists
                       .SelectMany(x => x.PendingTransferListHistories).ToListAsync();
                var total = items.Count();
                var totalManual = items.Where(x => x.IsManual).Count();
                Assert.Equal(3, total);
                Assert.Equal(3, totalManual);
            }

            {
                
                //2nd checking all items
                var result = await checkAllService.Execute(id);
                var items = await context.PendingTransferLists
                        .SelectMany(x => x.PendingTransferListHistories).ToListAsync();
                var total = items.Count();
                var totalManual = items.Where(x => x.IsManual).Count();
                Assert.Equal("2-1", result);
                Assert.Equal(5, total);
                Assert.Equal(5, totalManual);
            }
            

        }
    }
}