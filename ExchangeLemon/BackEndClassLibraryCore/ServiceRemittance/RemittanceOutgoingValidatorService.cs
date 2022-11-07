using BlueLight.Main;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
namespace BackEndClassLibrary
{
    public class RemittanceOutgoingValidatorService : IRemittanceOutgoingValidatorService
    {
        public RemittanceOutgoingValidatorService(ApplicationContext applicationContext,
            IBtcConfirmTransactionInquiry btcConfirmTransactionInquiry)
        {
            ApplicationContext = applicationContext;
            BtcConfirmTransactionInquiry = btcConfirmTransactionInquiry;
        }

        public ApplicationContext ApplicationContext { get; }
        public IBtcConfirmTransactionInquiry BtcConfirmTransactionInquiry { get;  }
        bool _IsManual { get; set; }

        public async Task Execute(long pendingTransferId,bool isManual)
        {
            this._IsManual = isManual;
            var pendingTransferList = await this.ApplicationContext.PendingTransferLists
                .Include(x => x.PendingTransferListHistories)
                .FirstAsync(x => x.Id == pendingTransferId);
            var transactionId = pendingTransferList.TransactionId;
            try
            {
                var confirm = await this.BtcConfirmTransactionInquiry.Execute(transactionId);
                await LogicCompare(confirm, pendingTransferList);
            }
            catch (Exception ex)
            {
                await LogicError(ex, pendingTransferList);
            }

        }

        private async Task LogicError(Exception ex, PendingTransferList pendingTransferList)
        {

            var log = new PendingTransferListHistory()
            {
                ConfirmTransfer = -1,
                Content = ex.Message,
                IsError = true,
                IsManual = this._IsManual
            };
            pendingTransferList.PendingTransferListHistories.Add(log);
            await this.ApplicationContext.SaveChangesAsync();
        }

        private async Task LogicCompare(int newConfirm, PendingTransferList pendingTransferList)
        {
            var prevConfirm = pendingTransferList.ConfirmTransfer;

            if (prevConfirm < newConfirm)
            {
                pendingTransferList.ConfirmTransfer = newConfirm;
                pendingTransferList.LastCheckTransferDate = DateTime.Now;

                var log = new PendingTransferListHistory()
                {
                    ConfirmTransfer = newConfirm,
                    Content = $"update_confirm-{newConfirm}",
                    IsManual = this._IsManual,
                };
                pendingTransferList.PendingTransferListHistories.Add(log);

                await this.ApplicationContext.SaveChangesAsync();
                //var p = ApplicationContext.PendingTransferLists.First().PendingTransferListHistories.Count();
            }
            else
            {
                pendingTransferList.ConfirmTransfer = newConfirm;
                var log = new PendingTransferListHistory()
                {
                    ConfirmTransfer = prevConfirm,
                    Content = $"no_change-{prevConfirm}",
                    IsManual = this._IsManual

                };
                pendingTransferList.PendingTransferListHistories.Add(log);
                await this.ApplicationContext.SaveChangesAsync();
            }
        }


    }
}
