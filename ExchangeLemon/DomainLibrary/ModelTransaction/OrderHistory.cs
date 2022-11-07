using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace BlueLight.Main
{

    public class OrderHistory
    {

        public int Id { get; set; }
        public long Version { get; set; }


        [Column(TypeName = "decimal(38, 8)")]
        public decimal RunningAmount { get; set; }

        [Column(TypeName = "decimal(38, 8)")]
        public decimal RunningLeftAmount { get; set; }

        [Column(TypeName = "decimal(38, 8)")]
        public decimal RequestRate { get; set; }


       
        public string CurrencyPair { get; set; }
        public bool IsBuy { get; set; }

        // public int OrderId { get; set; }
        // public int TransactionId { get; set; }

        [JsonIgnore]
        public virtual Order Order { get; set; }

        [JsonIgnore]
        public virtual Transaction Transaction { get; set; }



    }
}