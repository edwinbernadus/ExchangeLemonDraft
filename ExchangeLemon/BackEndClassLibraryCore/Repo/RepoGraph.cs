//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Caching.Memory;
//using Serilog;
////using System.Data.Entity;

//namespace BlueLight.Main
//{
//    public class RepoGraph
//    {
//        string CurrencyPair = "btc_usd";
//        private readonly ApplicationContext _context;

//        public GraphDbContext Context { get; }

//        public RepoGraph(
//        ApplicationContext applicationContext,
//        GraphDbContext context)
//        {
//            this._context = applicationContext;
//            Context = context;

//        }

//        public async Task<bool> SubmitAsync(decimal lastRate, DateTime inputDate)
//        {
//            var input = inputDate.ToPeriodDate();
//            var item = await Context.GraphPlainDataExts
//                .FirstOrDefaultAsync(x => x.CurrencyPair == CurrencyPair &&
//                x.DateTimeInput == input);
//            if (item == null)
//            {
//                var newItem = new GraphPlainDataExt()
//                {
//                    CurrencyPair = CurrencyPair,
//                    DateTimeInput = input,
//                    Value = lastRate
//                };
//                this.Context.GraphPlainDataExts.Add(newItem);
//            }
//            else
//            {
//                item.Value = lastRate;
//            }
//            await Context.SaveChangesAsync();

//            return true;
//        }



//        public async Task<List<GraphPlainData>> GetItems()
//        {
//            var maxPeriod = 15;

//            var dateEnd = DateTime.Now.ToPeriodDate();
//            var dateStart = dateEnd.AdjustSchedule(-1 * maxPeriod);

//            await ClearPreviousItems(dateStart);
//            List<GraphPlainDataExt> items = await this.Context.GraphPlainDataExts
//                .Where(x => x.DateTimeInput >= dateStart &&
//                x.DateTimeInput <= dateEnd &&
//                x.CurrencyPair == CurrencyPair)
//                .ToListAsync();

//            var periodList =
//                Enumerable.Range(0, maxPeriod)
//           .Select(x => dateStart.AdjustSchedule(x - 1).ToPeriodDate())
//           .ToList();

//            List<GraphPlainDataExt> periodList2 =
//            periodList.Select(x => new GraphPlainDataExt()
//            {
//                DateTimeInput = x,
//                Value = -1
//            }).ToList();

//            foreach (var itemOutput in periodList2)
//            {
//                foreach (var item in items)
//                {
//                    if (itemOutput.DateTimeInput == item.DateTimeInput)
//                    {
//                        itemOutput.Value = item.Value;
//                    }
//                }
//            }
//            PopulateDataFromCache(periodList2, items);
//            await EnsureGetDataFromPersistant(periodList2);
//            PopulateDataFromPrevious(periodList2);




//            var output = periodList2.Select(x => new GraphPlainData()
//            {
//                DateTimeInput = x.DateTimeInput,
//                Sequence = x.DateTimeInput.ToSequence(),
//                Value = x.Value
//            }).ToList();

//            return output;

//        }

//        private async Task ClearPreviousItems(DateTime dateStart)
//        {
//            var items = await this.Context.GraphPlainDataExts
//                .Where(x => x.DateTimeInput < dateStart).ToListAsync();
//            this.Context.RemoveRange(items);
//            await this.Context.SaveChangesAsync();
//        }

//        public void PopulateDataFromPrevious(List<GraphPlainDataExt> periodList2)
//        {
//            // periodList2.Reverse();
//            decimal prevData = -1;
//            foreach (var i in periodList2)
//            {
//                if (i.Value == -1)
//                {
//                    i.Value = prevData;
//                }

//                if (i.Value != -1)
//                {
//                    prevData = i.Value;
//                }

//            }
//            // periodList2.Reverse();
//        }

//        private void PopulateDataFromCache(List<GraphPlainDataExt> periodList2, List<GraphPlainDataExt> items)
//        {
//            foreach (var itemOutput in periodList2)
//            {
//                foreach (var item in items)
//                {
//                    if (itemOutput.DateTimeInput == item.DateTimeInput)
//                    {
//                        itemOutput.Value = item.Value;
//                    }
//                }
//            }
//        }

//        public async Task EnsureGetDataFromPersistant(List<GraphPlainDataExt> periodList2)
//        {
//            foreach (var i in periodList2)
//            {
//                if (i.Value == -1)
//                {
//                    var dateInput = i.DateTimeInput;
//                    decimal data = await LoadFromPersistant(CurrencyPair, dateInput);
//                    i.Value = data;
//                    await SubmitAsync(data, dateInput);
//                }
//            }
//        }

//        private async Task<decimal> LoadFromPersistant(string currencyPair, DateTime dateTimeInput)
//        {
//            var dateEnd = dateTimeInput;
//            var dateStart = dateEnd.AdjustSchedule(-1);
//            // dateEnd.AdjustSchedule(-1 * maxPeriod);

//            var transaction = await this._context.Transactions
//                .Where(x => x.TransactionDate <= dateEnd &&
//                    x.TransactionDate > dateStart &&
//                    x.CurrencyPair == currencyPair)
//                .OrderByDescending(x => x.Id)
//                .Take(1)
//                .FirstOrDefaultAsync();

//            var output = transaction?.TransactionRate ?? -1;
//            return output;
//        }

//        public async Task<GraphPlainData> GetLastItem()
//        {

//            var items = await GetItems();
//            var item = items.LastOrDefault();


//            if (item == null)
//            {
//                item = new GraphPlainData();
//            }

//            return item;
//        }


      

       

//    }
//}


