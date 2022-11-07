using Microsoft.Extensions.DependencyInjection;

namespace BotWalletWatcher
{
    public class DependencyWatcherHelper
    {
        public static void Populate(IServiceCollection services)
        {

            services.AddTransient<HttpSendService>();
            services.AddTransient<BlockChainUploadServiceExt>();
            
            

            //services.AddTransient<BlockChainDownloadServiceExt>();
            services.AddTransient<FileSaveService>();
            //services.AddTransienst<IBlockChainPositionService, BlockChainPositionService>();
        }
        public static ServiceCollection Create()
        {
            var serviceCollection = new ServiceCollection();
            Populate(serviceCollection);
            return serviceCollection;
        }

        public static ServiceCollection CreateForTesting()
        {
            var serviceCollection = new ServiceCollection();
            Populate(serviceCollection);
            //serviceCollection.AddTransient<INotificationWatcherService, NotificationWatcherClientServiceExt>();

        
            ServiceCollection output = serviceCollection;
            return output;
        }

        public static ServiceProvider GenerateService()
        {
            var serviceCollection = Create();
            ServiceProvider service = serviceCollection.BuildServiceProvider();
            return service;
        }

        public static ServiceProvider GenerateServiceTesting()
        {
            var serviceCollection = CreateForTesting();
            ServiceProvider service = serviceCollection.BuildServiceProvider();
            return service;
        }

       
    }
}
