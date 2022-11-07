using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BlueLight.Main;
using Newtonsoft.Json;

namespace BlueLight.Main {
    public class LogHelperBusiness {
        public static string Log (ICollection<Order> orders) {
            var items = orders.OrderByDescending (x => x.RequestRate).ToList ();
            var items2 = items.Select (x => Log (x, console : false)).ToList ();
            var output = string.Join (Environment.NewLine, items2);
            var result = "===========================================";
            result += Environment.NewLine + "LogHelperBusiness";
            result += Environment.NewLine + "===========================================";
            result += Environment.NewLine + output;
            result += Environment.NewLine + "===========================================";
            Serilog.Log.Information (result);
            return result;
        }

        public static string Log (Order order, bool console = true) {
            var id = order.Id;
            var createdBy = order.UserProfile?.username;
            var createdByUserId = order.UserProfile?.id;

            var amount = order.Amount;
            var leftAmount = order.LeftAmount;
            var rate = order.RequestRate;

            var isBuy = order.IsBuy ? "buy" : "sell";
            var output = $"{id} - {createdBy}/{createdByUserId} - {amount} - {leftAmount} - {rate} - {order.CurrencyPair} - {isBuy}";
            if (console) {
                Serilog.Log.Information (output);
            }
            return output;
        }

        public static string Log (InputTransactionRaw input) {

            var output = JsonConvert.SerializeObject (input);
            Serilog.Log.Information (output);
            return output;
        }
    }
}

//  services.AddDbContext<ApplicationContext> (options =>
// #if DEBUG
//                 options.UseInMemoryDatabase ()
// #else
//                 options.UseSqlServer (connString
//                     //, b => b.MigrationsAssembly("BackEndStandard"))
//                     , b => b.MigrationsAssembly ("ExchangeLemonCore"))
// #endif
//             );
// var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext> ();
// optionsBuilder.UseInMemoryDatabase ("one");
// var context = new ApplicationContext (optionsBuilder.Options);