// ;
//using BlueLight.Main.ViewModel;
// using Microsoft.AspNetCore.Mvc;

using BlueLight.Main;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndClassLibrary
{

    public class RemittanceOutgoingAdminService
    {
        private readonly ApplicationContext _applicationContext;

        public RemittanceOutgoingTransferService _remittanceOutgoingTransferService { get; }
        //public IBtcCloudServiceRegisterNotification registerNotification { get; }

        public RemittanceOutgoingAdminService(ApplicationContext applicationContext,
            RemittanceOutgoingTransferService remittanceOutgoingTransferService
            //IBtcCloudServiceRegisterNotification btcCloudServiceRegisterNotification
            )
        {
            _applicationContext = applicationContext;
            this._remittanceOutgoingTransferService  = remittanceOutgoingTransferService;
            //registerNotification = btcCloudServiceRegisterNotification;
        }

        public async Task SubmitHold(UserProfileDetail source, MvDepositInsert input)
        {
      
            var detail = source;
            var userProfile = detail.UserProfile;

            var logic = new UserProfileDetailLogic();
            var holdTransaction = logic.AddHold(detail,input.Amount, null, input.Currency);
            detail.HoldTransactions = detail.HoldTransactions ?? new List<HoldTransaction>();
            detail.HoldTransactions.Add(holdTransaction);
            var pendingTransfer = new PendingTransferList()
            {
                Amount = input.Amount,
                UserProfileDetail = detail,
                //AccountTransaction = accountTransaction,
                HoldTransaction = holdTransaction,
                IsApprove = true,
                AddressDestination = input.Address,
                StatusTransfer = "submitted",
                
            };
            await _applicationContext.PendingTransferLists.AddAsync(pendingTransfer);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task Reject(long id)
        {
            var pending = await _applicationContext
                .PendingTransferLists
                .FirstAsync(x => x.Id == id);
            pending.IsApprove = false;
            
            await _applicationContext.SaveChangesAsync();
        }

        public async Task FixSentFailed()
        {
            var source = _applicationContext.PendingTransferLists
                .Include(x => x.HoldTransaction)
                .Include(x => x.UserProfileDetail)
                .ThenInclude(x => x.UserProfile);

            var items = await source.Where(x => x.PendingBulkTransfer != null
               && x.StatusTransfer == "ongoing").ToListAsync();

            var log = new PendingBulkTransfer()
            {
                Status = "resend",
                Collection = items
            };

            var outgoingItems = items;

            



            await _applicationContext.PendingBulkTransfers.AddAsync(log);
            await _applicationContext.SaveChangesAsync();
            //TODO: 105 - send btc to cloud

            var output = items.Select(x => new { x.AddressDestination, x.Amount }).ToList();
            //this._remittanceOutgoingTransferService.Populate(this.registerNotification);
            await this._remittanceOutgoingTransferService.SendBtcToCloud(outgoingItems);
        }

        public async Task ReverseHold(long id)
        {
            var pending = await _applicationContext
                .PendingTransferLists
                .Include(x => x.HoldTransaction)
                .Include(x => x.UserProfileDetail)
                .FirstAsync(x => x.Id == id);
            pending.IsApprove = false;
            var detail = pending.UserProfileDetail;
            var holdTransaction = pending.HoldTransaction;

            var logic = new UserProfileDetailLogic();
            var removeHoldTransaction = logic.RemoveHold(detail,holdTransaction.Amount, null, holdTransaction.CurrencyCode);
            detail.HoldTransactions.Add(removeHoldTransaction);

            await _applicationContext.SaveChangesAsync();
        }

        public async Task Approve(long id)
        {
            var pending = await _applicationContext
                .PendingTransferLists
                .FirstAsync(x => x.Id == id);
            pending.IsApprove = true;

            await _applicationContext.SaveChangesAsync();
        }



        public async Task SendAndReleaseHold()
        {
            var items = await _applicationContext.PendingTransferLists
            .Include(x => x.HoldTransaction)
            .Include(x => x.UserProfileDetail)
            .ThenInclude(x => x.UserProfile)
            .Where(x => x.PendingBulkTransfer == null).ToListAsync();
            var log = new PendingBulkTransfer()
            {
                Status = "send",
                Collection = items
            };

            var outgoingItems = items.Where(x => x.IsApprove).ToList();
            var cancelItems = items.Where(x => x.IsApprove == false).ToList();

            var logic = new UserProfileDetailLogic();
            foreach (var item in outgoingItems)
            {
                var detail = item.UserProfileDetail;
                var holdTransaction = logic.RemoveHold(detail,item.Amount, null, item.HoldTransaction.CurrencyCode);
                detail.HoldTransactions.Add(holdTransaction);
                var transaction = new BlueLight.Main.Transaction();
                detail.UserProfile.OutgoingTransfer(item.Amount, item.HoldTransaction.CurrencyCode);
            }


            foreach (var item in cancelItems)
            {
                await ReverseHold(item.Id);
            }

            await _applicationContext.PendingBulkTransfers.AddAsync(log);
            await _applicationContext.SaveChangesAsync();
            //TODO: 105 - send btc to cloud

            var output = items.Select(x => new { x.AddressDestination, x.Amount }).ToList();
            //this._remittanceOutgoingTransferService.Populate(this.registerNotification);
            await this._remittanceOutgoingTransferService.SendBtcToCloud(outgoingItems);
        }







    }
    
}