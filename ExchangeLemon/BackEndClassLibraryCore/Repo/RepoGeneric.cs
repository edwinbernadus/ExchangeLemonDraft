//using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BlueLight.Main
{
    public class RepoGeneric
    {
        private readonly RepoUser repoUser;

        public ApplicationContext _context { get; }

        public RepoGeneric(ApplicationContext context, RepoUser repoUser)
        {
            _context = context;
            this.repoUser = repoUser;
        }

        public async Task<decimal> GetLastPrice(string currencyPair)
        {
            var spotMarket = await _context.SpotMarkets.FirstOrDefaultAsync(x => x.CurrencyPair == currencyPair);
            if (spotMarket == null)
            {
                return -1;
            }
            var output = spotMarket.LastRate;
            return output;
        }

        public async Task<List<MvInquiryBalance>> GetCurrentWallet(string userName)
        {
            var db = this._context;

            var userProfile = await repoUser.GetUser(userName);
            var output = userProfile.UserProfileDetails.
            Select(x => new MvInquiryBalance()
            {
                Amount = x.Balance,
                CurrencyCode = x.CurrencyCode,
                Balance = x.Balance,
                HoldBalance = x.HoldBalance,
                AvailableBalance = UserProfileLogic.GetAvailableBalance(x),
                Address = x.PublicAddress
            }).ToList();

            return output;
        }

        public List<MvInquiryBalance> WelcomePageDataPlain()
        {
            var currencies = UserProfile.GetDefaultCurrencies();
            var output = currencies.Select(x => new MvInquiryBalance()
            {
                CurrencyCode = x
            }).ToList();
            return output;
        }

        //public static async Task<IEnumerable<MvDetailReportOrder>> GetHistoryOrders(
        //   string userName, string currency_pair, bool transactionOnly, DBContext _context)
        public async Task<IQueryable<Order>> GetHistoryOrders(
            string userName, string currency_pair, bool transactionOnly)
        {
            var user = await repoUser.GetUser(userName);

            var query =
                _context.Orders
                .Include(x => x.Transactions)
                .Where(x => x.UserProfile.id == user.id &&
                   x.CurrencyPair == currency_pair);

            if (transactionOnly)
            {
                query = query
                    //.Where(x => x.HasTransactions);
                    .Where(x => x.TotalTransactions > 0);
            }

            var output = query.OrderByDescending(x => x.Id);

            return output;
        }

        public Order GetBuy(Transaction x)
        {
           return x.GetBuyOrderFirst();
        }
        public IQueryable<MvAccountTransaction> GenerateMarketHistoryTransaction
        (string currencyPair)
        {

            var output = _context.Transactions
                .Where(x => x.CurrencyPair == currencyPair)
                .OrderByDescending(x => x.Id)
                .Select(x => new MvAccountTransaction()
                {
                    Amount = x.Amount,
                    CreatedBy = "no_data",
                    Id = x.Id,
                    TransactionRate = x.TransactionRate
                });

            return output;

        }

        //public IQueryable<MvAccountTransaction> OldGenerateMarketHistoryTransaction
        //(string currencyPair)
        //{

        //    var output = _context.AccountTransactions
        //        .Where(x => x.CurrencyPair == currencyPair)

        //        //.Include(x => x.UserProfileDetail)
        //        //.ThenInclude(x => x.UserProfile)
        //        //.Include(x => x.Transaction)
        //        .Include(x => x.UserProfile)


        //        .OrderByDescending(x => x.Id)
        //        .Select(x => new MvAccountTransaction()
        //        {
        //            Amount = x.Amount,
        //            CreatedBy = x.UserProfile.username,
        //            Id = x.Id,
        //            TransactionRate = x.TransactionRate
        //        });

        //    return output;

        //}

    }
}
