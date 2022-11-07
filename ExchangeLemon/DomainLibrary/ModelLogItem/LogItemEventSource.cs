using Newtonsoft.Json;
using System;

namespace BlueLight.Main
{


    public class LogItemEventSource
    {
        public int Id { get; set; }
        public string MethodName { get; set; }
        public string Content { get; set; }

        public Guid SessionId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        // public bool IsError { get; set; }

        public bool IsRequest { get; set; }
        public bool IsDelay { get; set; }
        public string ExceptionMessage { get; set; }
        public long DelayTime { get; private set; }
        public string UserName { get; set; }

        // public int LogMode { get; private set; }



        public static LogItemEventSource Request<TRequest>(TRequest request)
        {
            var name = typeof(TRequest).Name;
            var content = JsonConvert.SerializeObject(request);

            var output = new LogItemEventSource()
            {
                MethodName = name,
                SessionId = Guid.NewGuid(),
                Content = content,
                // LogMode = 0,
                IsRequest = true
            };
            return output;
        }

        public static LogItemEventSource Response<TResponse>(TResponse response,
        LogItemEventSource requestLogItem, long delayTime)
        {
            var content = JsonConvert.SerializeObject(response);
            var output = new LogItemEventSource()
            {
                MethodName = requestLogItem.MethodName,
                SessionId = requestLogItem.SessionId,
                Content = content,
                DelayTime = delayTime,
                // LogMode = 1,
            };
            return output;
        }

        public static LogItemEventSource Error(Exception exception, LogItemEventSource requestLogItem)
        {
            //var item = exception.Message;
            //var item2 = exception.Source;
            //var item3 = exception.GetType();
            //var stackTrace = exception.StackTrace;  
            var input = new
            {
                Message = exception.Message,
                Source = exception.Source,
                Type = exception.GetType(),
                StackTrace = exception.StackTrace
            };
            //var exceptionMessage = JsonConvert.SerializeObject(exception);
            var exceptionMessage = JsonConvert.SerializeObject(input);
            var content = exception.Message;
            var output = new LogItemEventSource()
            {
                MethodName = requestLogItem.MethodName,
                SessionId = requestLogItem.SessionId,
                // LogMode = 2,
                Content = content,
                ExceptionMessage = exceptionMessage
            };
            return output;
        }
    }
}