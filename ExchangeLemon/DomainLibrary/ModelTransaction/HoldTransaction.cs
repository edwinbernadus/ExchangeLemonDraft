using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace BlueLight.Main
{
    public class HoldTransaction
    {
        public int Id { get; set; }

        public long Version { get; set; }

        [Column(TypeName = "decimal(38, 8)")]
        public decimal Amount { get; set; }

        [Column(TypeName = "decimal(38, 8)")]
        public decimal RunningHoldBalance { get; set; }

        [JsonIgnore]

        public virtual UserProfileDetail Parent { get; set; }




        public DateTime TransactionDate { get; set; }



        //[Required]
        //[JsonIgnore]
        public virtual Order Order { get; set; }



    

        public string CurrencyCode { get; set; }


        public static bool IsExternal(HoldTransaction holdTransaction)
        {
            var output = holdTransaction.Order == null;
            return output;
        }
    }
}


