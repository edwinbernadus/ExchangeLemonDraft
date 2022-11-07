//using ExchangeLemonNet.Controllers;
//using ExchangeLemonNet.Models;
//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Diagnostics;
using System.Linq;
//using System.Threading.Tasks;
//using System.Web.Http;
//using System.Web.Mvc;
//using Microsoft.AspNetCore.Cors;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

namespace BlueLight.Main
{

    public class OrderListInquiryQueryService {


        public IQueryable<Order> GetItemsBuyLogic (
            IQueryable<Order> queryOrders, int take, string currentPair) {
            var isBuy = true;

            var col = queryOrders
                .Where(x => x.IsBuy == isBuy &&
                   x.IsOpenOrder &&
                   x.IsCancelled == false &&
                   x.CurrencyPair == currentPair)
                .OrderByDescending(x => x.RequestRate)
                .Take(take);
             
            return col;
        }

        public IQueryable<Order> GetItemsSellLogic (
            IQueryable<Order> queryOrders, int take, string currentPair) {
            var isBuy = false;
            var collection = queryOrders
                .Where(x => x.IsBuy == isBuy && x.IsOpenOrder &&
                   x.CurrencyPair == currentPair)
                .OrderBy(x => x.RequestRate)
                .Take(take);
                //.ToList ();

            return collection;
        }

  
    }
}