using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BlueLight.Main
{
    public class SpotMarket
    {
        public int Id { get; set; }
        public string CurrencyPair { get; set; }
        public double Volume { get; set; }

        [Column(TypeName = "decimal(38, 8)")]
        public decimal LastRate { get; set; }
        public double PercentageMovement { get; set; }


    }
}