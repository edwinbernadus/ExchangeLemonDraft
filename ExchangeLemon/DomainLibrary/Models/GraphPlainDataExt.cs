//using System.Data.Entity;

using System;
using System.Diagnostics;

namespace BlueLight.Main
{

    [DebuggerDisplay("DateTimeInput = {DateTimeInput}, Value = {Value}")]
    public class GraphPlainDataExt
    {
        public int Id { get; set; }
        public DateTime DateTimeInput { get; set; }
        public decimal Value { get; set; }
        public string CurrencyPair { get; set; }
    }
}