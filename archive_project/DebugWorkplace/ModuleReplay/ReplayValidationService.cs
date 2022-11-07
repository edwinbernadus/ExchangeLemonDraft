using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueLight.Main;
using ExchangeLemonCore;
using ExchangeLemonCore.Controllers;
using Microsoft.EntityFrameworkCore;
// using Serilog;

namespace DebugWorkplace
{
    public class ReplayValidationService : IReplayValidationService
    {


        private readonly ApplicationContext _applicationContext;

        public ReplayValidationService(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;

        }

        public async Task Execute(LogItem logItem = null, long counter = -1)
        {
            await Task.Delay(0);

            //var orderItems = await _applicationContext.Orders
            //.Where(x => x.LeftAmount > 0 && x.IsOpenOrder == false)
            //.GroupBy(x => new { x.CurrencyPair, x.IsBuy })
            //.Select(x => new
            //{
            //    Total = x.Sum(y => y.LeftAmount),
            //    CurrencyPair = x.Key.CurrencyPair,
            //    IsBuy = x.Key.IsBuy
            //}).ToListAsync();



            //var holdItems = await _applicationContext.UserProfileDetails

            //.GroupBy(x => x.CurrencyCode)
            //.Select(x => new
            //{
            //    TotalHold = x.Sum(y => y.HoldBalance),
            //    // TotalAvailable = x.Sum(y => y.AvailableBalance),
            //    TotalBalance = x.Sum(y => y.Balance),
            //    x.Key
            //})
            //.Where(x => x.TotalHold > 0)
            //.ToListAsync();

            //var x1 = orderItems.FirstOrDefault(x => x.CurrencyPair == "btc_usd" && x.IsBuy == false);
            //var x2 = holdItems.FirstOrDefault(x => x.Key == "btc");
            //if (x1?.Total != x2?.TotalHold)
            //{
            //    throw new ArgumentException("Not same");
            //}
        }


        public bool IsValidLogic(ReplayValidationItem before, ReplayValidationItem after)
        {

            var orders = after.Orders;
            var orderItems = orders
            .GroupBy(x => new { x.CurrencyPair, x.IsBuy })
            .Select(x => new
            {
                Total = x.Sum(y => y.LeftAmount),
                CurrencyPair = x.Key.CurrencyPair,
                IsBuy = x.Key.IsBuy
            }).ToList();


            var userProfiles = after.UserProfileDetails;
            var userProfileItems = userProfiles
            .GroupBy(x => x.CurrencyCode)
            .Select(x => new
            {
                TotalHold = x.Sum(y => y.HoldBalance),
                TotalBalance = x.Sum(y => y.Balance),
                x.Key
            })
            .Where(x => x.TotalHold > 0)
            .ToList();

            var x1 = orderItems.FirstOrDefault(x => x.CurrencyPair == "btc_usd" && x.IsBuy == false);
            var x2 = userProfileItems.FirstOrDefault(x => x.Key == "btc");
            if (x1?.Total != x2?.TotalHold)
            {
                // return false;
            }

            
            decimal y1 = AmountCalculator.CalcRound(x1?.Total ?? 0m);
            decimal y2 = AmountCalculator.CalcRound(x2?.TotalHold ?? 0m);
            if (y1 != y2)
            {
                return false;
            }

            return true;
        }
        public async Task<ReplayValidationItem> CaptureItems()
        {

            List<Order> orderItems = await _applicationContext.Orders
            .Where(x => x.LeftAmount > 0 && x.IsOpenOrder).ToListAsync();

            List<UserProfileDetail> holdItems = await _applicationContext.UserProfileDetails
            .Where(x => x.HoldBalance > 0)
            .ToListAsync();

            var output = new ReplayValidationItem();
            output.UserProfileDetails = holdItems;
            output.Orders = orderItems;
            return output;
        }

        public void FindDiff(ReplayValidationItem beforeItems, ReplayValidationItem afterItems)
        {
            var details = new List<UserProfileDetail>();

            {
                var before = beforeItems.UserProfileDetails;
                var after = afterItems.UserProfileDetails;


                foreach (var i in before)
                {
                    foreach (var j in after)
                    {

                        if (i.Id == j.Id)
                        {
                            if (i.HoldBalance != j.HoldBalance)
                            {
                                details.Add(i);
                            }
                        }

                    }
                }
            }
        }

     
    }
}

