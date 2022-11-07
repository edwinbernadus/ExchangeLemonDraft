using System.Collections.Generic;
using System.Linq;

namespace BlueLight.Main
{
    public class MvDetailReportOrder : MvDetailSpotMarketItem
    {
        public bool IsComplete { get; set; }

        public static decimal GetAverageBuy(List<MvDetailReportOrder> output)
        {
            var w = output.Select(x => new { Total = x.Rate * x.Amount }).ToList();
            var w2 = w.Select(x => x.Total).Sum();

            var w3 = output.Select(x => x.Amount).Sum();
            decimal w4 = 0;
            try
            {
                w4 = w2 / w3;
            }
            catch (System.Exception)
            {

            }
                
            
            //if (double.IsNaN(w4))
            //{
            //    w4 = 0;
            //}

            return w4;
        }

        public MvDetailReportOrder()
        {

        }
        public MvDetailReportOrder(Order order)
        {
            var x = new MvOpenOrder(order);
            Amount = x.Amount;
            OrderId = x.Id;
            Rate = x.RequestRate;
            IsBuy = x.IsBuy;
            IsComplete = x.IsComplete;
            LeftAmount = x.LeftAmount;
        }
    }
}
