// ;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BlueLight.Main
{

    public class InquirySpotMarketService : IInquirySpotMarketService

    {
        public ApplicationContext ApplicationContext { get; }

        public InquirySpotMarketService(ApplicationContext applicationContext)
        {
            ApplicationContext = applicationContext;
        }
        public async Task<decimal> GetVolumeAsync(string currencyPair)
        {
            var yesterday = DateTime.Now.AddDays(-1);
            //var output = await this.ApplicationContext.AccountTransactions
            //    //.Include(x => x.Transaction)
            //    .Where(x => x.CurrencyPair == currencyPair &&
            //    x.CreatedDate > yesterday)
            //    .Select(x => new { x.Amount, x.CurrencyCode, x.CurrencyPair, x.TransactionRate, x.CreatedDate}).ToListAsync();
            //var result = output.Sum(x => x.Amount * x.TransactionRate);
            //return result;

            var output2 = await this.ApplicationContext.Transactions
           .Where(x => x.CurrencyPair == currencyPair &&
           x.TransactionDate >= yesterday)
           .Select(x => new { x.Amount, x.CurrencyPair, x.TransactionRate, x.TransactionDate}).ToListAsync();
            var result2 = output2.Sum(x => x.Amount * x.TransactionRate);
            return result2;
            
        }

        public async Task<LastChange> GetLastChangeAsync(string currencyPair)
        {
            var currentSpotMarket = await this.ApplicationContext.SpotMarkets
                .FirstAsync(x => x.CurrencyPair == currencyPair);
            var currentLastRate = currentSpotMarket.LastRate;

            var yesterday = DateTime.Now.AddDays(-1);
          

            var yesterdaySpotMarket = await this.ApplicationContext.Transactions
                
                .Where(x => x.CurrencyPair == currencyPair &&
                x.Amount > 0 && x.TransactionDate >= yesterday)
                .OrderBy(x => x.TransactionDate)
                .Select(x => new {
                    x.Amount,
                    //x.CurrencyCode,
                    x.CurrencyPair,
                    x.TransactionRate,
                    x.TransactionDate
                }).FirstOrDefaultAsync();

            var yesterdayLastRate = yesterdaySpotMarket?.TransactionRate ?? 0;

            //return result;

            var diff = currentLastRate - yesterdayLastRate;
            var diffPercentage = (decimal)diff / (decimal)currentLastRate;
            var diffPercentage2 = diffPercentage * 100;

            var output = new LastChange()
            {
                DiffPercentage = diffPercentage2,
                PreviousLastRate = yesterdayLastRate
            };
            return output;
        }

        

        public void PopulateAvailableBalance(UserProfile userProfile, MvSpotMarketDetail input)
        {
            var currencyPair = input.SpotMarket.CurrencyPair;
            var currency = currencyPair.Split('_')[0];
            var balance = userProfile.GetAvailableBalanceFromCurrency(currency);
            input.MyBalance = balance;

        }
    }
}