using System;
//using System.Data.Entity;

namespace BlueLight.Main
{
    public static class DateTimeHelperExtensionMethods
    {

        //public class DateTimeHelper
        //{
        //    // public DateTime PeriodOutput;
        //    public static DateTime Convert(DateTime inputDate)
        //    {
        //        var periodDateTime = new DateTime(inputDate.Year, inputDate.Month,
        //                inputDate.Day, inputDate.Hour, inputDate.Minute, 0);
        //        return periodDateTime;
        //    }

        //    public static long GetSequence(DateTime inputDate)
        //    {
        //        // "yyyy'-'MM'-'dd'T'HH':'mm':'ss
        //        var t1 = inputDate.ToString("yyyyMMddHHmm");
        //        var output = long.Parse(t1);
        //        return output;
        //    }



        //}

        public static DateTime AdjustSchedule(this DateTime dateTime, int input)
        {

            var output = dateTime.AddMinutes(input);
            //var output = dateTime.AddDays(input);
            //var output = dateTime.AddHours(input);
            return output;
        }
        public static DateTime ToPeriodDate(this DateTime inputDate)
        {
            var periodDateTime = new DateTime(inputDate.Year, inputDate.Month,
                    inputDate.Day, inputDate.Hour, inputDate.Minute, 0);
            return periodDateTime;
        }
        public static long ToSequence(this DateTime inputDate)
        {
            // "yyyy'-'MM'-'dd'T'HH':'mm':'ss
            var t1 = inputDate.ToString("yyyyMMddHHmm");
            var output = long.Parse(t1);
            return output;
        }
    }
}



//public async Task<GraphPlainData> GetItemDetail(DateTime input, string currencyPair)
//{
//    GraphPlainData output = null;
//    var _cache = this.memoryCache;


//    if (!_cache.TryGetValue(input, out output))
//    {
//        output = await CreateItemDetail(input, currencyPair);
//        // Key not in cache, so get data.
//        // Set cache options.
//        var cacheEntryOptions = new MemoryCacheEntryOptions()
//            // Keep in cache for this time, reset time if accessed.
//            .SetSlidingExpiration(TimeSpan.FromSeconds(300));

//        if (output != null)
//        {
//            if (IsCacheValid(input))
//            {
//                // Save data in cache.
//                _cache.Set(input, output, cacheEntryOptions);
//            }
//            else
//            {
//                Debug.WriteLine($"skip save to cache - {input}");
//            }
//        }
//    }
//    else
//    {
//        Debug.WriteLine($"from cache - {input}");
//    }

//    return output;

//}

//bool IsCacheValid(DateTime input)
//{
//    var dateStart = input;
//    var dateEnd = Adjust(dateStart, 1);
//    var now = DateTime.Now;
//    if ((now <= dateEnd) && (now > dateStart))
//    {
//        return false;
//    }

//    return true;
//    // var adjusted = Adjust(input, 1);
//    // var t = DateTime.Now - input;
//    // var output = (t.Hours >= 1);
//    // return output;
//}


//private double GetValue(List<object> itemsRaw, DateTime x)
//{
//    throw new NotImplementedException();
//}

//public async Task<List<GraphPlainData>> GetItemsTwo()
//{
//    var numbers = Enumerable.Range(0, 25).ToList();
//    numbers.Reverse();

//    var output = new List<GraphPlainData>();
//    foreach (var i in numbers)
//    {
//        // var inputDate = DateTime.Now.AddHours(-1 * i);
//        var inputDate = Adjust(DateTime.Now, (-1 * i));
//        var periodDateTime = DateTimeHelper.Convert(inputDate);
//        var item = await GetItemDetail(periodDateTime, currencyPair);
//        output.Add(item);
//    }

//    return output;
//}



//async Task<GraphPlainData> CreateItemDetail(DateTime input, string currencyPair)
//{
//    // var dateStart = input.AddHours(-1);
//    // var dateStart = Adjust(input, -1);
//    // var dateEnd = input;

//    var dateStart = input;
//    var dateEnd = Adjust(dateStart, 1);


//    var transaction = await this._context.Transactions
//                                    .Where(x => x.TransactionDate <= dateEnd &&
//                                        x.TransactionDate > dateStart &&
//                                        x.CurrencyPair == currencyPair)
//                                    .OrderByDescending(x => x.Id)
//                                    .FirstOrDefaultAsync();
//    var lastRate = transaction?.TransactionRate ?? 0;

//    // var items1 = await this._context.Transactions
//    //                     .Where(x => x.TransactionDate <= dateEnd &&
//    //                         x.TransactionDate > dateStart &&
//    //                         x.CurrencyPair == currencyPair)
//    //                     .GroupBy(x => x.CurrencyPair)
//    //                     .Select(x => new
//    //                     {
//    //                         Item = x.Key,
//    //                         Total = x.Count(),
//    //                         Avg = x.Average(y => y.TransactionRate)
//    //                     })
//    //                     .ToListAsync();

//    // var lastRate1 = items1.FirstOrDefault()?.Avg ?? 0;

//    // var items = await this._context.Transactions
//    //         .Where(x => x.TransactionDate <= dateEnd &&
//    //             x.TransactionDate > dateStart &&
//    //             x.CurrencyPair == currencyPair)
//    //         .ToListAsync();

//    // var lastRate = 0.0d;
//    // if (items.Any())
//    // {
//    //     lastRate = items.Average(x => x.TransactionRate);
//    // }


//    // if (lastRate != lastRate1)
//    // {
//    //     throw new ArgumentException("different avg rate");
//    // }

//    var item = new GraphPlainData()
//    {
//        Value = lastRate,
//        DateTime = input,
//        Sequence = DateTimeHelper.GetSequence(input)
//    };
//    return item;

//}
