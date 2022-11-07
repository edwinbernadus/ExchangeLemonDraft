using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;

namespace BlueLight.Main
{

    public class AccountTransaction
    {

        public int Id { get; set; }

        [Column(TypeName = "decimal(38, 8)")]
        public decimal Amount { get; set; }

        [Column(TypeName = "decimal(38, 8)")]
        public decimal Rate { get; set; }

        [Column(TypeName = "decimal(38, 8)")]
        public decimal RunningBalance { get; set; }

        //[Column(TypeName = "decimal(38, 8)")]
        //public decimal TransactionRate
        //{
        //    get
        //    {
        //        return this.Transaction?.TransactionRate ?? -1;
        //    }
        //}

        public string CurrencyPair { get; set; }

        public string CurrencyCode { get; set; }



        public string DebitCreditType { get; set; }



        public long Version { get; set; }

        [JsonIgnore]
        public virtual UserProfileDetail UserProfileDetail { get; set; }

        public static string GetName(string input)
        {
            if (input == null)
            {
                return "NoData";
            }
            var output = input.Split('@').FirstOrDefault() ?? "N/A";

            return output;
        }


        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public bool IsExternal { get; set; }
        public virtual Transaction Transaction { get; set; }

        //[Column(TypeName = "decimal(38, 8)")]
        //public decimal TransactionRate { get;  set; }
        public virtual UserProfile UserProfile { get;  set; }
    }

}