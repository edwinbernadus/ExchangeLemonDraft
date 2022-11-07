//using System;
//using System.Diagnostics;
//using System.Threading.Tasks;

//namespace BlueLight.Main
//{
//    public class StopWatchHelper
//    {
//        Stopwatch stopWatch = new Stopwatch();
//        string guidId = Guid.NewGuid().ToString();
//        public string ModuleName = "NoModule";

//        public StopWatchHelper(string moduleName)
//        {
//            this.ModuleName = moduleName;
//            this.stopWatch.Start();
//        }

//        public void End()
//        {
//            stopWatch.Stop();
//        }

//        public TimeSpan Log(string v)
//        {
//            var elapsed = stopWatch.Elapsed;
//            var log = $"[StopWatch Log] [{this.ModuleName}] - [{guidId}] - {v} - [{elapsed}]";
//            Log.Information(log);
//            Log.Information(log);
//            return elapsed;
//        }

       
//    }
//}