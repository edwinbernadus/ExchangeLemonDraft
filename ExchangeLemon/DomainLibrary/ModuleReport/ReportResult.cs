using System;
using System.Threading.Tasks;

namespace BlueLight.Main
{
    public class ReportResult
    {
        public DateTime Period { get; set; }
        public double Value { get; set; }

        public override string ToString()
        {
            var output = $"{Period.ToShortTimeString()} : {Value}";
            return output;
        }
    }
}