// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;

// namespace BlueLight.Main {
//     public class ReportVolume {
//         public async Task<double> Execute (string currencyPair,
//             IEnumerable<AccountTransaction> transactions) {
//             var date = DateTime.Now.AddDays (-1);
//             var items = transactions
//                 .Where (x => x.CurrencyPair == currencyPair && x.CreatedDate >= date);
//             var total = items.Sum (x => x.Amount);

//             return total;
//         }
//     }
// }