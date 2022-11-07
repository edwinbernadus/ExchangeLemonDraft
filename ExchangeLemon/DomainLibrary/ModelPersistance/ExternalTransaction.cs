using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlueLight.Main
{

    public class RemittanceTransaction
    {
        public int Id { get; set; }
        public bool IsIncoming { get; set; }


        [Column(TypeName = "decimal(38, 8)")]
        public decimal Amount { get; set; }
        //public string CurrencyPair { get; set; }

        public string CurrencyCode { get; set; }
        //public double Rate { get; private set; }
        //public string DebitCreditType { get; set; }


        [Column(TypeName = "decimal(38, 8)")]
        public decimal RunningBalance { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public long Version { get; set; }
    }

}