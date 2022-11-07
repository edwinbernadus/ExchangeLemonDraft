using System;
using System.Threading.Tasks;

namespace BlueLight.Main
{
    public class LogMatchService : ILogMatchService
    {
        public string _sessionId { get; private set; }
        public LoggingExtContext LoggingContext;
        private string _mode;
        private string _globalSessionId;

        public LogMatchService(LoggingExtContext loggingContext)
        {
            this.LoggingContext = loggingContext;
        }

        public async Task CaptureAsync(string content, TimeSpan duration)
        {
            var logItem = new LogItem()
            {
                SessionId = this._sessionId,

                ModuleName = "LogMatchService",
                AddtionalContent = this._mode,

                Content = content,
                

                //Content = "LogMatchService",
                //AddtionalContent = content,
                //ModuleName = this._mode,
                CallerName = this._globalSessionId,
                Duration = (long)duration.TotalMilliseconds
            };
            LoggingContext.LogItems.Add(logItem);
            await LoggingContext.SaveChangesAsync();
        }

        public async Task StartAsync()
        {
            await Task.Delay(0);
            this._sessionId = Guid.NewGuid().ToString();
        }

        public async Task SettingAsync(string mode)
        {
            await Task.Delay(0);
            this._mode = mode;
            this._globalSessionId = Guid.NewGuid().ToString();
        }
    }
}