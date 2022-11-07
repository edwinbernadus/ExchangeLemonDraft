using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BlueLight.Main
{

    public class LoggingExtContext : DbContext
    {
        // private LoggerFactory _loggerFactory;

        public LoggingExtContext(DbContextOptions<LoggingExtContext> options) : base(options)
        {

        }

        //public DbSet<WatcherSendLog> WatcherSendLogs { get; set; }
        //public DbSet<WatcherReceiveLog> WatcherReceiveLogs { get; set; }
        public DbSet<LogItem> LogItems { get; set; }
        public DbSet<LogSession> LogSessions { get; set; }

        public DbSet<LogItemEventSource> LogItemEventSources { get; set; }

    }


}

