using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


namespace BlueLight.Main
{
    
    [Obsolete]
    public class OrderHelper
    {

        public static void IsValid(List<Order> rawItems2, List<Order> rawItems)
        {
            var left = String.Join("-", rawItems2.Select(x => x.Id));
            var right = String.Join("-", rawItems.Select(x => x.Id));
            if (left != right)
            {
                throw new ArgumentException("not same logic!");
            }
        }


        //public static IQueryable<Order> IsOpenOrder(IQueryable<Order> items, bool isTrue = true)
        //{
        //    var items2 = items.Where(order =>
        //        ((((order.Amount - order.TotalTransactions) == 0) == false) &&
        //        order.IsCancelled == false) == isTrue);
        //    return items2;
        //}

        //public static IEnumerable<Order> IsOpenOrderListVersion(List<Order> items, bool isTrue = true)
        //{
        //    var items2 = items.Where(order =>
        //        (((order.LeftAmount == 0) == false) &&
        //        order.IsCancelled == false) == isTrue);
        //    return items2;
        //}
    }
}