using NBitcoin;
using QBitNinja.Client;
using QBitNinja.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueLight.Main
{
    public partial class BtcBusinessLogicNew 
    {
        public IBitcoinGetBalance BitcoinGetBalance { get; }

        public BtcBusinessLogicNew(IBitcoinGetBalance bitcoinGetBalance)
        {
            BitcoinGetBalance = bitcoinGetBalance;
        }
        public async Task<InquiryDiffResultNew> InquiryDiff(string walletAddress, decimal balance, List<string> oldItems)
        {

            //await Task.Delay(0);


            BtcTransaction inquiryResult = await this.BitcoinGetBalance.Execute(walletAddress);

            if (inquiryResult.Details.Count == 0)
            {
                var zeroResult = new InquiryDiffResultNew()
                {
                    HasNewItem = false
                };
                return zeroResult;
            }


            var output = inquiryResult.Details.Count;


            //debug only
            //var totalConfirm = inquiryResult.Details.Where(x => x.IsConfirm ).Count();
            var totalTransaction = inquiryResult.Details.Count;




            var totalOperations = inquiryResult.Details;



            //var result = GetDiffPositiveOnly(totalOperations, oldItems);

            if (inquiryResult.Details.Any() == false)
            {
                var zeroResult = new InquiryDiffResultNew()
                {
                    HasNewItem = false
                };
                return zeroResult;
            }


            var balance1 = new Money(balance, MoneyUnit.BTC);
            var newDiffAmountInput = inquiryResult.Balance - balance1;

            var newTransactions = inquiryResult.Details.Except(oldItems).ToList();
            var output2 = new InquiryDiffResultNew()
            {
                HasNewItem = true,
                Address = walletAddress,
                NewDiffAmount = newDiffAmountInput.ToDecimal(MoneyUnit.BTC),
                NewTransactions = newTransactions,
                
            };
            return output2;
        }




    }
}





//private (decimal newDiffAmount, List<BtcTransactionDetail> transactions) GetDiffPositiveOnly
//    (List<BalanceOperation> totalOperations, List<string> oldItems)
//{
//    var allItems = totalOperations.ToList();

//    var newItems = allItems
//        .Where(x => x.Amount > 0)
//        .Select(x => x.TransactionId.ToString()).ToList();

//    var diffItems = newItems.Except((IEnumerable<string>)oldItems);
//    var filteredItems = totalOperations.Where(x => diffItems.Contains(x.TransactionId.ToString())).ToList();

//    var items3 = filteredItems.Select(x => x.Amount.ToDecimal(MoneyUnit.BTC)).ToList();
//    var newDiffAmount = items3.Sum(x => x);

//    var transactions = filteredItems.Select(x => new BtcTransactionDetail  { TransactionId = x.TransactionId , Amount = x.Amount }).ToList();

//    (decimal newDiffAmount, List<BtcTransactionDetail> transactions) output =  (newDiffAmount, transactions);
//    return output;
//}