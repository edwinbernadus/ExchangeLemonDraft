using System.Threading.Tasks;
using BlueLight.Main;
// using Serilog;

namespace ExchangeLemonCore.Controllers
{
    public class EnsureContextService
    {
        private readonly LoggingExtContext loggingExtContext;
        private readonly ApplicationContext applicationContext;
        private readonly FakeAccountService fakeAccountService;
        private readonly FakeSpotMarketService fakeSpotMarketService;

        public EnsureContextService(LoggingExtContext loggingExtContext,
                    ApplicationContext applicationContext,
                    FakeAccountService fakeAccountService,
                    FakeSpotMarketService fakeSpotMarketService)
        {
            this.loggingExtContext = loggingExtContext;
            this.applicationContext = applicationContext;
            this.fakeAccountService = fakeAccountService;
            this.fakeSpotMarketService = fakeSpotMarketService;
        }
        public async Task<bool> Execute()
        {
            await this.applicationContext.Database.EnsureCreatedAsync();
            //await LoggingContext.Database.EnsureCreatedAsync();
            await this.loggingExtContext.Database.EnsureCreatedAsync();

            var context = this.applicationContext;

            await this.fakeSpotMarketService.EnsureDataPopulated();
            var totalError = await fakeAccountService.InitAccount();

            return true;
        }
    }
}

