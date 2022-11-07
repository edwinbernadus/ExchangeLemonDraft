using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BlueLight.Main
{
    public class LogService
    {

        private LoggingContext _loggingContext;

        public LogService(LoggingContext loggingContext)
        {

            this._loggingContext = loggingContext;
        }

        public LogHelperBot Bot
        {
            get
            {
                var output = new LogHelperBot(this._loggingContext);
                return output;
            }
        }

        //public LogHelperMvc Mvc
        //{
        //    get
        //    {
        //        var output = new LogHelperMvc(this._loggingContext);
        //        return output;
        //    }
        //}

        public LogHelperObject ItemObject
        {
            get
            {
                var output = new LogHelperObject(this._loggingContext);
                return output;
            }
        }

        public LogHelperStopWatch StopWatch
        {
            get
            {
                var output = new LogHelperStopWatch(this._loggingContext);
                return output;
            }
        }

    }
}

// public async Task ExecuteLogBot (int id, string userName) {
//     var log = new LogHelperBot (this.context);
//     await log.Save (id, userName);
// }

// public async Task ExecuteLogMvcSaveError (object output, HttpRequest request, Exception ex, [CallerMemberName] string callerName = "") {
//     var log = new LogHelperMvc (this.context);
//     await log.SaveError (output, request, ex, callerName);

// }
// public async Task ExecuteLogMvcSaveLog (object output, HttpRequest request, [CallerMemberName] string callerName = "") {
//     var log = new LogHelperMvc (this.context);
//     await log.SaveLog (output, request, callerName);
// }