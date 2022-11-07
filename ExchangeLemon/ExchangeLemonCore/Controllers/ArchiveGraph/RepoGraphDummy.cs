//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using BlueLight.Main;
//using Microsoft.Extensions.Caching.Memory;

//namespace ExchangeLemonCore.Controllers
//{
//    [Obsolete]
//    public class RepoGraphDummy
//    {
//        private readonly IMemoryCache memoryCache;

//        public RepoGraphDummy(IMemoryCache memoryCache)
//        {
//            this.memoryCache = memoryCache;
//        }

//        public List<GraphPlainData> GetItems()
//        {
//            var numbers = Enumerable.Range(0, 25).ToList();
//            numbers.Reverse();

//            var output = new List<GraphPlainData>();
//            foreach (var i in numbers)
//            {
//                var inputDate = DateTime.Now.AddMinutes(-1 * i);
//                var periodDateTime = inputDate.ToPeriodDate();
//                var item = GetOrSetItemDetail(periodDateTime, -1);
//                output.Add(item);
//            }

//            return output;
//        }

//        public GraphPlainData GetOrSetItemDetail(DateTime input, int rate)
//        {
//            GraphPlainData output = null;
//            var _cache = this.memoryCache;


//            if (!_cache.TryGetValue(input, out output))
//            {
//                output = CreateItemDetail(input, rate);
//                // Key not in cache, so get data.
//                // Set cache options.
//                var cacheEntryOptions = new MemoryCacheEntryOptions()
//                    // Keep in cache for this time, reset time if accessed.
//                    .SetSlidingExpiration(TimeSpan.FromSeconds(300));

//                if (output != null)
//                {
//                    if (PastTimeValidation(input))
//                    {
//                        // Save data in cache.
//                        _cache.Set(input, output, cacheEntryOptions);
//                    }
//                }
//            }
//            else
//            {
//                Debug.WriteLine($"from cache - {input}");
//            }

//            return output;

//        }

//        public bool PastTimeValidation(DateTime input)
//        {
//            var t = DateTime.Now - input;
//            var output = (t.Minutes >= 1);
//            return output;
//        }

//        GraphPlainData CreateItemDetail(DateTime input, int rate)
//        {
//            var random = GraphDummyFactory.random;

//            var nextNumber = random.Next(-25, 25);
//            if (rate == -1)
//            {
//                rate = nextNumber;
//            }
//            var lastRate = GraphDummyFactory.lastRate + nextNumber;
//            if (PastTimeValidation(input))
//            {
//                GraphDummyFactory.lastRate = lastRate;
//            }
//            else
//            {
//                Debug.WriteLine($"skip update last rate global - {input}");
//            }



//            var item = new GraphPlainData()
//            {
//                Value = lastRate,
//                DateTime = input,
//                Sequence = input.ToSequence()
//            };
//            return item;

//        }
//    }
//}