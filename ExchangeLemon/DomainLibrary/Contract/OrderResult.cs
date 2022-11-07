using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace BlueLight.Main
{
    // [NotMapped]
    public class OrderResult
    {


        public int Id { get; set; }



        public virtual Order CreatedOrder { get; set; }
        public virtual Order InputOrder { get; set; }

        public decimal TransactionLastRate { get; set; } = -1;

        public bool IsDealOrder
        {
            get
            {
                var output = ((int)TransactionLastRate) != -1;
                return output;
            }
        }

        public DateTime TransactionLastRateTransactionDate { get;  set; }


        public List<OrderHistory> OrderHistories { get; set; } = new List<OrderHistory>();

        public List<UserProfile> OppositePlayers { get; set; } = new List<UserProfile>();
        public List<string> GenerateTargetNotifName(Order order)
        {
            var items = OppositePlayers.Select(x => x.username).Distinct().ToList();
            var item = order.UserProfile.username;
            items.Add(item);
            return items;
        }

        public Guid GuidId { get; set; }
        public List<string> TargetNotificationNames { get; set; }

    }
}