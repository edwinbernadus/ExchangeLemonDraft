using System;
using System.Threading.Tasks;
using BackEndClassLibrary;
using BlueLight.Main;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeLemonCore.Controllers
{
    public class DebugErrorController : Controller
    {
        public DebugErrorController(LoggingExtContext loggingExtContext)
        {
            _loggingExtContext = loggingExtContext;
        }

        public LoggingExtContext _loggingExtContext { get; }

        //http://localhost:5000/debugError
        public async Task<string> Index()
        {
            var sessionLogStart = LogSession.Start(Guid.NewGuid());
            this._loggingExtContext.Add(sessionLogStart);
            await this._loggingExtContext.SaveChangesAsync();

            var output = "start";
            try
            {
                var c = new Class1();
                var s2 = c.GenerateError();
                var sessionLogEnd = LogSession.End(startSession: sessionLogStart);
                sessionLogEnd.IsError = false;
                this._loggingExtContext.Add(sessionLogEnd);
                await this._loggingExtContext.SaveChangesAsync();
                output = "finish";
            }
            catch (Exception ex)
            {
                string errMsg = ex.Message;
                string stackTrace = ex.StackTrace;

                var sessionLogEnd = LogSession.End(startSession: sessionLogStart);
                sessionLogEnd.IsError = true;
                sessionLogEnd.ErrorMessage = errMsg;
                sessionLogEnd.StackTrace = stackTrace;
                this._loggingExtContext.Add(sessionLogEnd);
                await this._loggingExtContext.SaveChangesAsync();
                output = "end-error";

            }
            return $"error-testing-{output}";
        }
    }
}