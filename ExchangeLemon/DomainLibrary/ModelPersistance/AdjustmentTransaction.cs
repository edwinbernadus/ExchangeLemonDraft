using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlueLight.Main
{
    public class AdjustmentTransaction
    {
        public int Id { get; set; }

        [Column(TypeName = "decimal(38, 8)")]
        public decimal AdjustmentAmount { get; set; }


        public string CurrencyCode { get; set; }

        [Column(TypeName = "decimal(38, 8)")]
        public decimal RunningBalance { get; set; }


        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Column(TypeName = "decimal(38, 8)")]
        public decimal PrevHoldBalance { get; set; }
    }

}