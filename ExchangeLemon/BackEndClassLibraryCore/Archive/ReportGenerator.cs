// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using BlueLight.Main;
// using BlueLight.Main;
// using Microsoft.EntityFrameworkCore;

// namespace BlueLight.Main {
//     public class ReportGenerator {

//         public static async Task<List<MvPendingOrder>> GetOpenOrders (string userName,
//             ApplicationContext _context, string currencyPair = null) {
//             var user = await InquiryUserService.GetUser (_context, userName);

//             var conditionCollection =
//                 //FactoryOrders.Generate(_context)
//                 _context.Orders
//                 .Where (x => x.IsCancelled == false && x.UserProfile.id == user.id);
//             if (currencyPair != null) {
//                 conditionCollection = conditionCollection.Where (x => x.CurrencyPair == currencyPair);
//             }

//             var collection = conditionCollection
//                 .OrderByDescending (x => x.RequestRate);
//             //.ToListAsync();

//             var result = collection
//                 .Where (x => x.IsFillComplete == false)
//                 .Select (x => new MvOpenOrder (x)).ToList ();
//             var output = result.Select (x => new MvPendingOrder () {
//                 Amount = x.Amount,
//                     Id = x.Id,
//                     Rate = x.RequestRate,
//                     IsBuy = x.IsBuy,
//                     CurrencyPair = x.CurrencyPair,
//                     Order = x.Order,
//                     LeftAmount = x.LeftAmount
//             });

//             var result2 = output.ToList ();

//             return result2;
//         }

//         public static async Task<double> GetLastPrice (string currencyPair, ApplicationContext _context) {
//             var spotMarket = await _context.SpotMarkets.FirstAsync (x => x.CurrencyPair == currencyPair);
//             var output = spotMarket.LastRate;
//             return output;
//         }

//         public static async Task<List<MvInquiryBalance>> GetCurrentWallet (string userName, ApplicationContext db) {
//             var userProfile = await db.UserProfiles.FirstAsync (x => x.username == userName);
//             var output = userProfile.UserProfileDetails.
//             Select (x => new MvInquiryBalance () {
//                 Amount = x.Balance,
//                     CurrencyCode = x.CurrencyCode,
//                     Balance = x.Balance,
//                     HoldBalance = x.HoldBalance,
//                     AvailableBalance = x.AvailableBalance,
//                     Address = x.PublicAddress
//             }).ToList ();

//             return output;
//         }

//         public static async Task<IEnumerable<MvDetailReportOrder>> GetHistoryOrders (
//             string userName, string currency_pair, bool transactionOnly, ApplicationContext _context) {
//             var user = await InquiryUserService.GetUser (_context, userName);

//             var coll0 =
//                 //FactoryOrders.Generate(_context)
//                 _context.Orders
//                 .Where (x => x.UserProfile.id == user.id && x.CurrencyPair == currency_pair);

//             if (transactionOnly) {
//                 coll0 = coll0
//                     //.Where(x => x.HasTransactions);
//                     .Where (x => x.TotalTransactions > 0);
//             }

//             var collection = coll0.OrderByDescending (x => x.RequestRate);

//             var result = collection
//                 .Select (x => new MvOpenOrder (x));
//             var output = result.Select (x => new MvDetailReportOrder () {
//                 Amount = x.Amount,
//                     Id = x.Id,
//                     Rate = x.RequestRate,
//                     IsBuy = x.IsBuy,
//                     IsComplete = x.IsComplete,
//                     LeftAmount = x.LeftAmount
//             });

//             return output;
//         }
//     }
// }