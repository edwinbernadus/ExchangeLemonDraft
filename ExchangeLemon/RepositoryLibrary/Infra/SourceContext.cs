using Microsoft.EntityFrameworkCore;

namespace BlueLight.Main
{
    public class SourceContext : DbContext
    {
        // private LoggerFactory _loggerFactory;

        public SourceContext(DbContextOptions<SourceContext> options) : base(options)
        {

        }


        public DbSet<LogItemEventSource> LogItemEventSources { get; set; }

    }
}