using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BlueLight.Main
{
    public class SessionGeneratorService
    // : IServiceSessionGenerator
    {
        private LoggingContext context;

        public SessionGeneratorService(LoggingContext context)
        {
            this.context = context;

        }
        public async Task<long> CreateAsync()
        {
            await Task.Delay(0);
            return -1;
            //var session = new OrderSession();
            //context.OrderSessions.Add(session);
            //await context.SaveChangesAsync();
            //var output = session.Id;
            //return output;
        }

        public async Task SetAsCloseAsync(long sessionId)
        {
            await Task.Delay(0);
            //var session = await context.OrderSessions.FirstAsync(x => x.Id == sessionId);
            //session.IsClosed = true;
            //await context.SaveChangesAsync();
        }
    }
}