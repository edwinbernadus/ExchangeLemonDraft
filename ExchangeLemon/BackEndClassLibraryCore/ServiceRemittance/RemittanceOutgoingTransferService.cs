// ;
//using BlueLight.Main.ViewModel;
// using Microsoft.AspNetCore.Mvc;

using BlueLight.Main;
using NBitcoin;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndClassLibrary
{
    public class RemittanceOutgoingTransferService
    {
        private ApplicationContext _applicationContext;

        public IBtcServiceSendMoney BtcService { get; }
        //public IBtcCloudServiceRegisterNotification RegisterNotification { get; set; }

        public RemittanceOutgoingTransferService(ApplicationContext applicationContext,
           IBtcServiceSendMoney btcService)
        {
            _applicationContext = applicationContext;
            BtcService = btcService;
            
        }

        //public void Populate(IBtcCloudServiceRegisterNotification registerNotification)
        //{
        //    this.RegisterNotification = registerNotification;
        //}

        private long ConvertToSatoshi(decimal amount)
        {
            var value = amount;
            var nRet = new Money(value, MoneyUnit.BTC);
            var s2 = nRet.Satoshi;
            return s2;

        }


        public async Task SendBtcToCloud(List<PendingTransferList> items)
        {
            var output = items.Select(x => new SentItem()
            {
                Address = x.AddressDestination,
                Amount = x.Amount,
                From = x.UserProfileDetail.Id,
                PendingTransferList = x,
            }).ToList();

            foreach (var item in items)
            {
                item.StatusTransfer = "ongoing";
            }

            await this._applicationContext.SentItems.AddRangeAsync(output);
            await this._applicationContext.SaveChangesAsync();

            //var privateKey = "cPFY3pPiUsAyXHNsqqjyGqgxofqQAzna4kvJHvVBdLCK77Rfn4rV";
            foreach (var item in output)
            {
                try
                {
                    await LogicDetail(item, items);
                }
                catch (System.Exception)
                {

                    
                }
                
            }

        }

        async Task LogicDetail(SentItem item, List<PendingTransferList> items)
        {
            long amountInSatoshi = ConvertToSatoshi(item.Amount);
#pragma warning disable CS0219 // Variable is assigned but its value is never used
            long amountFeeSatoshi = 1000;
#pragma warning restore CS0219 // Variable is assigned but its value is never used
            var transactionId = await this.BtcService.SendMoney(item.Address, amountInSatoshi);
            //var transactionId = result.TransactionId;

            var pendingTransfer = items.First(x => x.Id == item.PendingTransferList.Id);
            pendingTransfer.StatusTransfer = "sent";
            pendingTransfer.TransactionId = transactionId;
            await this._applicationContext.SaveChangesAsync();

            //TODO: 107 - remark register
#if DEBUG
            //var addressId = "";
            //await RegisterNotification.RegisterNotifyTransfer(addressId);
#endif
        }

    }




}