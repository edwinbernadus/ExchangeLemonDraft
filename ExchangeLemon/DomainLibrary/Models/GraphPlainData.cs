using System;
using System.Diagnostics;
//using System.Data.Entity;

namespace BlueLight.Main
{

    [DebuggerDisplay("Value= {Value},DateTime = {DateTime}")]
    //[DebuggerDisplay("Value = {Value}")]
    public class GraphPlainData
    {
        
        public long Sequence { get; set; }

        public DateTimeOffset DateTimeInput { get; set; }
        public decimal Value { get; set; }

        public string DateTime { get
            {
                return DateTimeInput.ToString();
            }
        }
    }
}