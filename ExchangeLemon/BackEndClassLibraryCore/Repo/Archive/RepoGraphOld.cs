// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.Caching.Memory;
// //using System.Data.Entity;

// namespace BlueLight.Main
// {
//     public class RepoGraphOld
//     {
//         private readonly IMemoryCache memoryCache;
//         private readonly ApplicationContext _context;
//         private readonly RepoGeneric repoGeneric;
//         private readonly string currencyPair;



//         public RepoGraphOld(IMemoryCache memoryCache,
//         ApplicationContext applicationContext, 
//         RepoGeneric repoGeneric)
//         {
//             this.memoryCache = memoryCache;
//             this._context = applicationContext;
//             this.repoGeneric = repoGeneric;

//             this.currencyPair = "btc_usd";
//         }


//         public async Task<List<GraphPlainData>> GetItems()
//         {
//             var maxPeriod = 15;
//             var now = DateTime.Now;

//             var dateEnd = DateTime.Now;
//             var dateStart = dateEnd.AdjustSchedule(-1 * maxPeriod);


//             var inputs = this._context.Transactions
//                                 .Where(x =>
//                                     x.CurrencyPair == currencyPair)
//                                 .OrderByDescending(x => x.Id);



//             var transaction = await this._context.Transactions
//                 .Where(x => x.TransactionDate <= dateEnd &&
//                     x.TransactionDate > dateStart &&
//                     x.CurrencyPair == currencyPair)
//                 .OrderByDescending(x => x.Id)
//                 .Select(x => new TransactionLite()
//                 {
//                     //Amount = x.Amount,
//                     //Id = x.Id,
//                     TransactionDate = x.TransactionDate,
//                     TransactionRate = x.TransactionRate
//                 })
//                 .ToListAsync();

//             var itemsRaw = transaction
//                 .Select(x => new Tuple<DateTime, TransactionLite>
//                 (x.TransactionDate.ToPeriodDate(), x))
//                 .ToList();


//             List<DateTime> periodList = Enumerable.Range(0, maxPeriod)
//                 .Select(x => dateStart.AdjustSchedule(x + 1).ToPeriodDate())
//                 .ToList();

//             var itemsStaging = periodList.Select(x => new GraphPlainData()
//             {
//                 DateTimeInput = x,
//                 Sequence = x.ToSequence(),
//                 Value = GetLastRateLogic(itemsRaw, x)
//             }).ToList();


//             var lastRate = -1.0m;

//             var temp = lastRate;
//             foreach (var i in itemsStaging)
//             {
//                 if (i.Value == -1)
//                 {
//                     i.Value = temp;
//                 }
//                 else
//                 {
//                     temp = i.Value;
//                 }
//             }

//             return itemsStaging;
//         }

//         private decimal GetLastRateLogic(List<Tuple<DateTime, TransactionLite>> itemsRaw, DateTime period)
//         {
//             var items = itemsRaw.Where(x => x.Item1 == period);
//             var last = items.OrderByDescending(x => x.Item2.TransactionDate).FirstOrDefault();
//             var output = last?.Item2.TransactionRate ?? -1;
//             return output;
//         }


//         public async Task<GraphPlainData> GetLastItem()
//         {

//             var items = await GetItems();
//             var item = items.LastOrDefault();
//             //var inputDate = DateTime.Now;
//             //var periodDateTime = DateTimeHelper.Convert(inputDate);
//             //var item = await GetItemDetail(periodDateTime, currencyPair);


//             if (item == null)
//             {
//                 item = new GraphPlainData();
//             }

//             return item;
//         }

//         //DateTime Adjust(DateTime dateTime, int input)
//         //{
//         //    // var output = dateTime.AddHours(input);
//         //    var output = dateTime.AddMinutes(input);
//         //    return output;
//         //}




//     }
// }



// //public async Task<GraphPlainData> GetItemDetail(DateTime input, string currencyPair)
// //{
// //    GraphPlainData output = null;
// //    var _cache = this.memoryCache;


// //    if (!_cache.TryGetValue(input, out output))
// //    {
// //        output = await CreateItemDetail(input, currencyPair);
// //        // Key not in cache, so get data.
// //        // Set cache options.
// //        var cacheEntryOptions = new MemoryCacheEntryOptions()
// //            // Keep in cache for this time, reset time if accessed.
// //            .SetSlidingExpiration(TimeSpan.FromSeconds(300));

// //        if (output != null)
// //        {
// //            if (IsCacheValid(input))
// //            {
// //                // Save data in cache.
// //                _cache.Set(input, output, cacheEntryOptions);
// //            }
// //            else
// //            {
// //                Debug.WriteLine($"skip save to cache - {input}");
// //            }
// //        }
// //    }
// //    else
// //    {
// //        Debug.WriteLine($"from cache - {input}");
// //    }

// //    return output;

// //}

// //bool IsCacheValid(DateTime input)
// //{
// //    var dateStart = input;
// //    var dateEnd = Adjust(dateStart, 1);
// //    var now = DateTime.Now;
// //    if ((now <= dateEnd) && (now > dateStart))
// //    {
// //        return false;
// //    }

// //    return true;
// //    // var adjusted = Adjust(input, 1);
// //    // var t = DateTime.Now - input;
// //    // var output = (t.Hours >= 1);
// //    // return output;
// //}


// //private double GetValue(List<object> itemsRaw, DateTime x)
// //{
// //    throw new NotImplementedException();
// //}

// //public async Task<List<GraphPlainData>> GetItemsTwo()
// //{
// //    var numbers = Enumerable.Range(0, 25).ToList();
// //    numbers.Reverse();

// //    var output = new List<GraphPlainData>();
// //    foreach (var i in numbers)
// //    {
// //        // var inputDate = DateTime.Now.AddHours(-1 * i);
// //        var inputDate = Adjust(DateTime.Now, (-1 * i));
// //        var periodDateTime = DateTimeHelper.Convert(inputDate);
// //        var item = await GetItemDetail(periodDateTime, currencyPair);
// //        output.Add(item);
// //    }

// //    return output;
// //}



// //async Task<GraphPlainData> CreateItemDetail(DateTime input, string currencyPair)
// //{
// //    // var dateStart = input.AddHours(-1);
// //    // var dateStart = Adjust(input, -1);
// //    // var dateEnd = input;

// //    var dateStart = input;
// //    var dateEnd = Adjust(dateStart, 1);


// //    var transaction = await this._context.Transactions
// //                                    .Where(x => x.TransactionDate <= dateEnd &&
// //                                        x.TransactionDate > dateStart &&
// //                                        x.CurrencyPair == currencyPair)
// //                                    .OrderByDescending(x => x.Id)
// //                                    .FirstOrDefaultAsync();
// //    var lastRate = transaction?.TransactionRate ?? 0;

// //    // var items1 = await this._context.Transactions
// //    //                     .Where(x => x.TransactionDate <= dateEnd &&
// //    //                         x.TransactionDate > dateStart &&
// //    //                         x.CurrencyPair == currencyPair)
// //    //                     .GroupBy(x => x.CurrencyPair)
// //    //                     .Select(x => new
// //    //                     {
// //    //                         Item = x.Key,
// //    //                         Total = x.Count(),
// //    //                         Avg = x.Average(y => y.TransactionRate)
// //    //                     })
// //    //                     .ToListAsync();

// //    // var lastRate1 = items1.FirstOrDefault()?.Avg ?? 0;

// //    // var items = await this._context.Transactions
// //    //         .Where(x => x.TransactionDate <= dateEnd &&
// //    //             x.TransactionDate > dateStart &&
// //    //             x.CurrencyPair == currencyPair)
// //    //         .ToListAsync();

// //    // var lastRate = 0.0d;
// //    // if (items.Any())
// //    // {
// //    //     lastRate = items.Average(x => x.TransactionRate);
// //    // }


// //    // if (lastRate != lastRate1)
// //    // {
// //    //     throw new ArgumentException("different avg rate");
// //    // }

// //    var item = new GraphPlainData()
// //    {
// //        Value = lastRate,
// //        DateTime = input,
// //        Sequence = DateTimeHelper.GetSequence(input)
// //    };
// //    return item;

// //}
