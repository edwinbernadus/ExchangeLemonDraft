using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Serilog;
//using System.Data.Entity;

namespace BlueLight.Main
{

    public class RepoGraphHelper
    {
        static DateTime GenerateKey(DateTime s)
        {
            var newStart = new DateTime(s.Year, s.Month, s.Day, s.Hour, 0, 0);
            return newStart;
        }

        public static List<DateTime> GeneratePeriod(DateTime startFromNow, int getPreviousTotalSequence)
        {
            var output = new List<DateTime>();
            // var s = startFromNow.AddHours(1);
            //var s = startFromNow.AddDays(1);
            var s = startFromNow.AddMinutes(1);

            var newStart = new DateTime(s.Year, s.Month, s.Day, s.Hour, 0, 0);
            for (int i = 0; i < getPreviousTotalSequence; i++)
            {
                // var item = newStart.AddHours(-1 * i);
                //var item = newStart.AddDays(-1 * i);
                var item = newStart.AddMinutes(-1 * i);
                output.Add(item);
            }
            return output;

        }
    }
}


