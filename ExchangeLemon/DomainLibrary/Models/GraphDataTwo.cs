using System;
using System.Diagnostics;
//using System.Data.Entity;

namespace BlueLight.Main
{

    [DebuggerDisplay("Open= {Open},DateTime = {DateTimeSequence}")]
    //[DebuggerDisplay("Value = {Value}")]
    public class GraphDataTwo
    {
        public DateTimeOffset DateTimeSequence { get; set; }
        //public decimal Value { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public decimal Volume { get; set; }

        public string GetDisplay()
        {
            var p = this;
            return $"{p.Open},{p.High},{p.Low},{p.Close}";
        }
    }
}