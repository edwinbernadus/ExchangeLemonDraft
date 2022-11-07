using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Serilog;
using System.Collections.Concurrent;
//using System.Data.Entity;

namespace BlueLight.Main
{
    public class RepoGraphExt
    {
        private string CurrencyPair = CurrencyParam.BtcPair;

        static ConcurrentDictionary<DateTime, GraphDataTwo> Items =
            new ConcurrentDictionary<DateTime, GraphDataTwo>();
        public ApplicationContext _context { get; }


        public RepoGraphExt(ApplicationContext applicationContext)
        {
            this._context = applicationContext;
        }

        public async Task<List<GraphDataTwo>> GetItemsDraft()
        {

            var output = new List<GraphDataTwo>();

            var startFromNow = DateTime.Now;
            //var startFromNow = DateTime.Today;
            var getPreviousTotalSequence = 500;
            List<DateTime> periodList = RepoGraphHelper.GeneratePeriod(startFromNow,
                getPreviousTotalSequence);
            foreach (var item in periodList)
            {
                GraphDataTwo data = await GetItem(item);
                output.Add(data);
            }
            var result = output.Where(x => x.Open != 0).ToList();
            //var m1 = result.First().DateTimeSequence;
            //var m2 = result.Last().DateTimeSequence;

            //var m1a = result.First();
            //var m2a = result.Last();

            return result;
        }

        private async Task<GraphDataTwo> GetItem(DateTime periodItem)
        {
            GraphDataTwo output;

            if (Items.ContainsKey(periodItem))
            {
                output = Items[periodItem];
            }
            else
            {
                var data = await GenerateData(periodItem);
                var isLastPeriod = (DateTime.Now < periodItem);
                if (isLastPeriod == false)
                {
                    Items[periodItem] = data;
                }
                output = data;
            }
            return output;
        }

        private async Task<GraphDataTwo> GenerateData(DateTime item)
        {
            var dateEnd = item;
            //var dateStart = item.AddHours(-1);
            var dateStart = item.AddMinutes(-1);
            //var currencyPair = "btc_usd";
            var transactions = await this._context.Transactions
                .Where(x => x.TransactionDate <= dateEnd &&
                    x.TransactionDate > dateStart &&
                    x.CurrencyPair == CurrencyPair)
                .OrderByDescending(x => x.Id).ToListAsync();

            var newItem = new GraphDataTwo()
            {
                DateTimeSequence = dateEnd,

            };

            if (transactions.Count == 0)
            {
                return newItem;
            }


            {
                newItem.Open = transactions.First().TransactionRate;
                newItem.Close = transactions.Last().TransactionRate;
                newItem.High = transactions.Max(x => x.TransactionRate);
                newItem.Low = transactions.Min(x => x.TransactionRate);


            };
            return newItem;
        }


    }
}


