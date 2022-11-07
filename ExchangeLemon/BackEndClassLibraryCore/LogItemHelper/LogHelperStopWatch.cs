using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Serilog;

namespace BlueLight.Main
{
    public class LogHelperStopWatch
    {
        private StopWatchHelper stopWatchHelper;
        private readonly string sessionId;
        private string moduleName;
        private LoggingContext context;
        private readonly DateTime startBatchDate;

        public LogHelperStopWatch(LoggingContext context)
        {

            this.stopWatchHelper = new StopWatchHelper(this.ToString());
            this.sessionId = Guid.NewGuid().ToString();
            this.context = context;
            this.startBatchDate = DateTime.Now;
        }

        public async Task Save(string item, [CallerMemberName] string callerName = "")
        {
            await Task.Delay(0);

            // var timeSpan = stopWatchHelper.Save(item);
            // var duration = timeSpan.Ticks;

            // var log = new LogItem()
            // {
            //     Content = item,
            //     UserName = "-",
            //     ClassName = "-",
            //     ModuleName = this.moduleName,
            //     CallerName = callerName,
            //     SessionId = this.sessionId,
            //     Duration = duration,
            //     StartBatchDate = this.startBatchDate

            // };

            // if (ParamRepo.IsSaveLogEnable)
            // {
            //     context.LogItems.Add(log);
            //     await context.SaveChangesAsync();
            // }

            // var c2 = $"{item} - {moduleName}";
            // Serilog.Log.Warning(c2);
            // // LogHelperConsole.SaveLogWarning (c2);
        }

        public void Start(string moduleName)
        {
            this.moduleName = moduleName;
            this.stopWatchHelper.Start();
        }

        public void End()
        {
            this.stopWatchHelper.End();
        }
    }
}