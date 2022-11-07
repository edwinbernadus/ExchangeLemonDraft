// using System;

// namespace ExchangeLemonCore.Controllers
// {
//     public class DateTimeHelper
//     {
//         // public DateTime PeriodOutput;
//         public static DateTime Convert(DateTime inputDate)
//         {
//             var periodDateTime = new DateTime(inputDate.Year, inputDate.Month,
//                     inputDate.Day, inputDate.Hour, inputDate.Minute, 0);
//             return periodDateTime;
//         }

//         public static long GetSequence(DateTime inputDate)
//         {
//             // "yyyy'-'MM'-'dd'T'HH':'mm':'ss
//             var t1 = inputDate.ToString("yyyyMMddHHmm");
//             var output = long.Parse(t1);
//             return output;
//         }

//         public static bool PastFiveMin(DateTime input)
//         {
//             var t = DateTime.Now - input;
//             var output = (t.Minutes >= 1);
//             return output;
//         }

//     }
// }