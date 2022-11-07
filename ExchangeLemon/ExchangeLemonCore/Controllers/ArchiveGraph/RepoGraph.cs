// using System;
// using System.Collections.Generic;
// using System.Diagnostics;
// using System.Linq;
// using System.Threading.Tasks;
// using BlueLight.Main;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.Caching.Memory;

// namespace ExchangeLemonCore.Controllers
// {
//     public class RepoGraph
//     {
//         private readonly IMemoryCache memoryCache;
//         private readonly ApplicationContext _context;
//         private readonly string currencyPair;

//         public RepoGraph(IMemoryCache memoryCache, ApplicationContext applicationContext)
//         {
//             this.memoryCache = memoryCache;
//             this._context = applicationContext;
//             this.currencyPair = "btc_usd";
//         }

//         public async Task<List<GraphPlainData>> GetItems()
//         {
//             var numbers = Enumerable.Range(0, 25).ToList();
//             numbers.Reverse();

//             var output = new List<GraphPlainData>();
//             foreach (var i in numbers)
//             {
//                 var inputDate = DateTime.Now.AddHours(-1 * i);
//                 var periodDateTime = DateTimeHelper.Convert(inputDate);
//                 var item = await GetItemDetail(periodDateTime, currencyPair);
//                 output.Add(item);
//             }

//             return output;
//         }


//         public async Task<GraphPlainData> GetLastItem()
//         {
//             var inputDate = DateTime.Now;
//             var periodDateTime = DateTimeHelper.Convert(inputDate);
//             var item = await GetItemDetail(periodDateTime, currencyPair);

//             return item;
//         }

//         public async Task<GraphPlainData> GetItemDetail(DateTime input, string currencyPair)
//         {
//             GraphPlainData output = null;
//             var _cache = this.memoryCache;


//             if (!_cache.TryGetValue(input, out output))
//             {
//                 output = await CreateItemDetail(input, currencyPair);
//                 // Key not in cache, so get data.
//                 // Set cache options.
//                 var cacheEntryOptions = new MemoryCacheEntryOptions()
//                     // Keep in cache for this time, reset time if accessed.
//                     .SetSlidingExpiration(TimeSpan.FromSeconds(300));

//                 if (output != null)
//                 {
//                     if (DateTimeHelper.PastFiveMin(input))
//                     {
//                         // Save data in cache.
//                         _cache.Set(input, output, cacheEntryOptions);
//                     }
//                 }
//             }
//             else
//             {
//                 Debug.WriteLine($"from cache - {input}");
//             }

//             return output;

//         }

//         async Task<GraphPlainData> CreateItemDetail(DateTime input, string currencyPair)
//         {
//             var dateStart = input.AddHours(-1);
//             var dateEnd = input;

//             var items1 = await this._context.Transactions
//                                 .Where(x => x.TransactionDate <= dateEnd &&
//                                     x.TransactionDate > dateStart &&
//                                     x.CurrencyPair == currencyPair)
//                                 .GroupBy(x => x.CurrencyPair)
//                                 .Select(x => new
//                                 {
//                                     Item = x.Key,
//                                     Total = x.Count(),
//                                     Avg = x.Average(y => y.TransactionRate)
//                                 })
//                                 .ToListAsync();

//             var lastRate1 = items1.First().Avg;

//             var items = await this._context.Transactions
//                     .Where(x => x.TransactionDate <= dateEnd &&
//                         x.TransactionDate > dateStart &&
//                         x.CurrencyPair == currencyPair)
//                     .ToListAsync();
//             var lastRate = items.Average(x => x.TransactionRate);

//             if (lastRate != lastRate1)
//             {
//                 throw new ArgumentException("different avg rate");
//             }

//             var item = new GraphPlainData()
//             {
//                 Value = lastRate,
//                 DateTime = input,
//                 Sequence = DateTimeHelper.GetSequence(input)
//             };
//             return item;

//         }
//     }
// }