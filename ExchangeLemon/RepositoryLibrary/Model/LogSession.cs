using System;

namespace BlueLight.Main
{
    public class LogSession
    {
        public int Id { get; set; }
        public Guid GuidSession { get; set; }
        public bool IsStart { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string ErrorMessage { get; set; }
        public bool IsError { get; set; }
        public string StackTrace { get; set; }

        public static LogSession Start(Guid sessionId)
        {
            if (sessionId == new Guid())
            {
                sessionId = Guid.NewGuid();
            }
            var output = new LogSession()
            {
                GuidSession = sessionId,
                IsStart = true,
            };
            return output;
        }


        public static LogSession EndError(LogSession startSession, string errorMessage)
        {
            var output = new LogSession()
            {
                GuidSession = startSession.GuidSession,
                IsStart = false,
                ErrorMessage = errorMessage,
                IsError = true
            };
            return output;
        }
        public static LogSession End(LogSession startSession)
        {
            var output = new LogSession()
            {
                GuidSession = startSession?.GuidSession ?? new Guid(),
                IsStart = false,
            };
            return output;
        }

    }


}

