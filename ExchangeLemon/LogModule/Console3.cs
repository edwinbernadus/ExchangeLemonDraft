using Serilog;

namespace LogLibrary
{
    public class Console3
    {
        static Serilog.Core.Logger log = new Serilog.LoggerConfiguration()
                .WriteTo.Debug()
                .WriteTo.Console()
                .CreateLogger();

        public static void WriteLine(string content)
        {
            log.Information(content);
            //Install-Package Serilog.Sinks.File
        }

        public static void ResetLog()
        {
            try
            {
                log.Dispose();
            }
            catch (System.Exception)
            {
            }
        }
        public static void EnableSaveFile(string moduleName)
        {
            ResetLog();
            var logPath = $"c:\\logs\\{moduleName}\\log.txt";
                        
            log = new Serilog.LoggerConfiguration()
                            .WriteTo.File(logPath, rollingInterval: Serilog.RollingInterval.Day)
                            .WriteTo.Debug()
                            .WriteTo.Console()
                            .CreateLogger();
        }
    }
}