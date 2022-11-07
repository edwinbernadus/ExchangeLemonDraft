//using gekko_bot.BittrexTrade;
//using gekko_bot.WelcomeBittrexTicker;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlueLight.KirinLogic
{
    internal class TempRepoOld {

        static Dictionary<string, LogTransaction> items =
            new Dictionary<string, LogTransaction> ();

        static Dictionary<string, double> fakeBalance =
            new Dictionary<string, double> ();

        static List<FakeTransaction> FakeTransactions = new List<FakeTransaction> ();

        internal static void AddFakeTransaction (LogTransaction item, string newGuid) {
            // var newGuid = Guid.NewGuid().ToString().ToLower();
            items.Add (newGuid, item);
        }

        internal static void RemoveFakeTransaction (string guid) {
            items.Remove (guid);
        }
        internal static WelcomeBittrexOpenOrders GetAll () {

            var items2 = items.Select (x => new Result () {
                OrderUuid = x.Key,
                    Price = x.Value.rate,
                    Quantity = x.Value.quantity,
                    QuantityRemaining = x.Value.quantity,
                    OrderType = ConvertTypeTransaction (x.Value.typeTransaction)
            }).ToList ().ToArray ();

            var output = new WelcomeBittrexOpenOrders () {

                Success = true,
                Result = items2
            };
            return output;
        }

        public static string GenerateKey (string userName, string currency) {
            var key = userName + "-" + currency.ToLower ();
            return key;
        }
        internal static double GetFakeBalance (string combineKey) {
            //var combineKey = GenerateKey(userName:userName,currency:currency);
            if (fakeBalance.ContainsKey (combineKey)) {
                var output = fakeBalance[combineKey];
                return output;
            } else {
                return 0;
            }

            //var real = -1;
            //var fake = -1;
            //var total = real + fake;
            //throw new NotImplementedException();
        }

        internal static void AdjustBalance (string userName, double amount, string currencyCode) {
            var combineKey = GenerateKey (userName: userName, currency: currencyCode);
            var balance = GetFakeBalance (combineKey);
            var newBalance = balance + amount;
            var fakeTransaction = new FakeTransaction () {
                Amount = amount,
                CreatedDate = DateTime.Now,
                CurrencyCode = currencyCode,
                RunningBalance = newBalance,
                UserName = userName,
                Id = -1,
            };
            FakeTransactions.Add (fakeTransaction);

            fakeBalance[combineKey] = newBalance;
        }

        private static string ConvertTypeTransaction (string typeTransaction) {
            // "OrderType" : "LIMIT_SELL",
            if (typeTransaction == "buy") {
                return "LIMIT_BUY";
            } else if (typeTransaction == "sell") {
                return "LIMIT_SELL";
            } else {
                return "NA";
            }
        }

        internal static void ClearAll () {
            items.Clear ();
        }

    }
}