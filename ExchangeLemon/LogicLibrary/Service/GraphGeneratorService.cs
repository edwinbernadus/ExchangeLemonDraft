using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Serilog;

namespace BlueLight.Main {
    public class GraphGeneratorService {
        public double LastPrice { get; set; }
        public List<ReportResult> Output { get; set; } = new List<ReportResult> ();
        public List<ReportItem> Input { get; set; }
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }

        public void CallSample () {

            var r = new GraphGeneratorService ();
            r.Input = GraphGeneratorService.GenerateSample ();

            r.Execute ();
            var result = r.Output;
            Log.Information ($"Total Items: {result.Count}");
            Console.ReadLine ();

        }

        public void Execute () {

            //var timeTables = GenerateTimeTable();

            if (Input.Any () == false) {
                return;
            }

            PopulateCriteria ();
            var timeTables = GenerateTimeTablePlain ();

            var CONS_PERIOD = 5;

            var t = Input.
            Select (x => new {
                    x.CreatedDate.Hour,
                        x.CreatedDate.Minute,
                        //GroupMin = x.CreatedDate.Minute / 5,
                        GroupMin = x.CreatedDate.Second / CONS_PERIOD,
                        x.CreatedDate.Second
                })
                .ToList ();

            var t2 = Input.Select (x => new {
                Periode = CreatePeriode (x.CreatedDate),
                    Value = x.Value
            }).ToList ();

            var t3 = t2.GroupBy (x => x.Periode)
                .Select (x => new {
                    Period = x.Key,
                        Average = x.Average (p => p.Value),
                        Total = x.Count (),
                        Sum = x.Sum (p => p.Value)
                }).ToList ();

            var prev = 0.0;
            foreach (var i in timeTables) {

                foreach (var j in t3) {
                    if (i.Period == j.Period) {
                        var input = j.Average;
                        i.Value = input;
                        prev = input;
                    }
                }
                if (i.Value == -1) {
                    i.Value = prev;
                }
            }

            this.Output = timeTables;

            var last = timeTables.Last ();
            var newItem = new ReportResult () {
                Period = last.Period.AddSeconds (CONS_PERIOD),
                Value = LastPrice
            };

            timeTables.Add (newItem);

            var log = String.Join ("|", timeTables.Select (x => x.ToString ()));
            Log.Information ($"Graph Log: {log}");
        }

        List<ReportResult> GenerateTimeTablePlain () {
            var items = new List<DateTime> ();

            var t = Start;
            while (t <= End) {

                if (t.Minute % 5 == 0) {
                    items.Add (t);
                }
                t = t.AddMinutes (1);
            }

            var result = items
                .Select (x => new ReportResult () {
                    Period = x,
                        Value = -1
                }).ToList ();

            return result;
        }

        //private List<ReportResult> GenerateTimeTable()
        //{
        //    PopulateCriteria();
        //    var result2 = GenerateTimeTablePlain();

        //    return result2;
        //}

        private void PopulateCriteria () {
            var DateInputSorted = Input.OrderBy (x => x.CreatedDate)
                .Select (x => x.CreatedDate).ToList ();
            var start1 = DateInputSorted.First ();
            this.Start = RoundUp (start1);
            var end1 = DateInputSorted.Last ();
            this.End = RoundUp (end1);
        }

        private DateTime RoundUp (DateTime start) {
            var temp = start;

            while (temp.Minute % 5 != 0) {
                temp = temp.AddMinutes (1);
            }
            var output = GenerateTime (temp, temp.Hour, temp.Minute, 0);

            return output;
        }

        private static DateTime GenerateTime (DateTime temp, int hour, int minute, int seconds) {
            var output = new DateTime (temp.Year, temp.Month, temp.Day, hour, minute, seconds);
            return output;
        }

        DateTime CreatePeriode (DateTime input) {
            var periode = (input.Minute / 5) + 1;

            //var t2 = new DateTime(input.Year, input.Month, input.Day,
            //    input.Hour, 0, 0);
            var t2 = GenerateTime (input, input.Hour, 0, 0);
            var t3 = t2.AddMinutes (periode * 5);

            return t3;
        }

        long CreatePeriodeInNumber (DateTime input) {
            var periode = (input.Minute / 5) + 1;

            //var t2 = new DateTime(input.Year, input.Month, input.Day,
            //    input.Hour, 0, 0);
            var t2 = GenerateTime (input, input.Hour, 0, 0);
            t2 = t2.AddMinutes (periode * 5);

            var t3 = t2.ToString ("yyyymmddHHmm");
            var t4 = long.Parse (t3);

            return t4;
        }

        [Obsolete]
        internal static List<ReportItem> GenerateOutputInHour (DateTime input) {
            List<ReportItem> output = new List<ReportItem> ();

            var items = Enumerable.Range (0, 12);
            foreach (var i in items) {
                output.Add (new ReportItem () {

                    CreatedDate = GenerateTime (input, input.Hour, 5 * i, 0),
                        //CreatedDate = new DateTime(input.Year, input.Month, input.Day, input.Hour, 5 * i, 0),
                });
            }

            return output;
        }

        public static List<ReportItem> GenerateSample () {
            List<ReportItem> output = new List<ReportItem> ();
            output.Add (new ReportItem () {
                CreatedDate = new DateTime (2010, 01, 5, 22, 1, 5),
                    Value = 10
            });
            output.Add (new ReportItem () {
                CreatedDate = new DateTime (2010, 01, 5, 22, 3, 6),
                    Value = 30
            });
            output.Add (new ReportItem () {
                CreatedDate = new DateTime (2010, 01, 5, 22, 7, 7),
                    Value = 20
            });
            output.Add (new ReportItem () {
                CreatedDate = new DateTime (2010, 01, 5, 22, 9, 8),
                    Value = 40
            });

            output.Add (new ReportItem () {
                CreatedDate = new DateTime (2010, 01, 5, 22, 15, 9),
                    Value = 10
            });
            output.Add (new ReportItem () {
                CreatedDate = new DateTime (2010, 01, 5, 22, 17, 10),
                    Value = 10
            });

            output.Add (new ReportItem () {
                CreatedDate = new DateTime (2010, 01, 5, 22, 59, 10),
                    Value = 14
            });
            return output;
        }
    }
}