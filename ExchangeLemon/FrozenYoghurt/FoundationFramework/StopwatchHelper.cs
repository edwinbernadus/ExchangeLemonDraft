using Serilog;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BlueLight.Main
{
    public class StopWatchHelper
    {
        Stopwatch stopWatch = new Stopwatch();
        string guidId = Guid.NewGuid().ToString();
        public string ModuleName = "NoModule";

        public StopWatchHelper(string moduleName)
        {
            this.ModuleName = moduleName;
            //this.stopWatch.Start();
        }

        public void Start()
        {
            stopWatch.Stop();
            stopWatch.Reset();
            stopWatch.Start();
        }

        public void End()
        {
            stopWatch.Stop();
        }

        public TimeSpan Save(string input)
        {
            var elapsed = stopWatch.Elapsed;
            var content = $"[StopWatch Log] [{this.ModuleName}] - [{guidId}] - {input} - [{elapsed}]";
            Log.Information(content);

            return elapsed;
        }

       
    }
}