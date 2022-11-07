using System;
using System.Reflection;
using BlueLight.Main;
using ExchangeLemonCore;
using ExchangeLemonCore.Controllers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BackEndClassLibrary
{
    public class DependencyHelper
    {

        public static void Init(IServiceCollection services)
        {

            
            services.AddTransient<RepoGraphExt>();
            services.AddMediatR(typeof(OrderItemCommand).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(OrderListInquiryQuery).GetTypeInfo().Assembly);
            //services.AddTransient<IMediator>();
            services.AddTransient<AccountTransactionService>();
            services.AddTransient<OrderBuyService>();
            services.AddTransient<OrderSellService>();

            services.AddTransient<PulseInsertService>();
            services.AddTransient<WebHookRoutingService>();
            services.AddTransient<WatcherService>();
            services.AddTransient<IBtcConfirmTransactionInquiry, ResultBtcConfirmTransactionInquiry>();

            services.AddTransient<IRemittanceOutgoingValidatorService, RemittanceOutgoingValidatorService>();
            //services.AddTransient<RemittanceOutgoingValidatorService>();
            services.AddTransient<ReceiveLogCaptureService>();
            services.AddTransient<WebhookInquirySentTransferService>();

            services.AddTransient<WebhookReceiveTransferService>();
            services.AddTransient<RemittanceOutgoingCheckAllManualService>();
            services.AddTransient<RemittanceOutgoingAdminService>();
            services.AddTransient<RemittanceOutgoingTransferService>();
            services.AddTransient<BtcBusinessLogicNew>();
            services.AddTransient<PendingTransferListsRepo>();
            //services.AddTransient<BtcCloudService>();
            //services.AddTransient<BtcService>();

            services.AddTransient<RemittanceOutgoingAdminService>();
            //services.AddTransient<BtcService>();
            //services.AddTransient<ReceiveLogCapture>();
            //services.AddTransient<IBitcoinGetBalance, DummyBitcoinGetBalance>();


            //services.AddTransient<BtcBusinessLogicNew>();
            services.AddTransient<OrderItemMainServiceExt>();

            services.AddTransient<OrderListInquiryQueryService>();
            ////services.AddTransient<FunctionBitcoinService>();
            services.AddTransient<IFunctionBitcoinService, FunctionBitcoinServiceReal>();


            services.AddTransient<GraphGeneratorService>();
            services.AddTransient<BitcoinAddressService>();

            services.AddTransient<OrderItemCancelAllService>();
            services.AddTransient<OrderItemCancelService>();
            services.AddTransient<OrderItemMainService>();

            services.AddTransient<OrderTransactionService>();
            services.AddTransient<OrderItemNotificationService>();

            services.AddTransient<IContextSaveService, ContextSaveService>();
            services.AddTransient<IOrderMatchService, OrderItemMatchService>();


            services.AddTransient<LogHelperObject>();
            services.AddTransient<ILogHelperMvc, DbLogHelperMvc>();
            services.AddTransient<LogHelperBot>();
            services.AddTransient<LogHelperStopWatch>();

            services.AddTransient<SignalDashboard>();
            services.AddTransient<TransactionNotificationService>();

            services.AddTransient<RepoUser>();
            services.AddTransient<RepoGeneric>();
            //services.AddTransient<RepoGraph>();
            services.AddTransient<RepoOrderList>();
            services.AddTransient<RepoOpenOrder>();
            services.AddTransient<RepoBalance>();

            services.AddTransient<BitcoinSyncJob>();

            services.AddTransient<ExtRepoUser>();
            services.AddTransient<ExtRepoOpenOrder>();

            services.AddTransient<DbConnGeneratorService>();

            services.AddTransient<SessionGeneratorService>();
            services.AddTransient<ILogMatchService, LogMatchService>();

            services.AddTransient<ReplayCaptureService>();

            services.AddTransient<FakeSpotMarketService>();

            services.AddTransient<OrderListInquiryContextService>();
            services.AddTransient<IOrderListInquiryContextService, OrderListInquiryCacheService>();

            services.AddTransient<OrderItemMainFlag>();
            services.AddTransient<OrderItemMainValidationDebug>();
            services.AddTransient<IInquiryUserService, InquiryUserService>();



            var isUseSignalRemoteMode = true;

            if (isUseSignalRemoteMode)
            {
                //services.AddSingleton<SignalFactory>();
                //services.AddTransient<ITransactionHubService, RemoteTransactionHubService>();

            }
            else
            {
                // services.AddTransient<ITransactionHubService, TransactionHubService> ();
            }

            // InitSignal (services);

            services.AddTransient<InquirySpotMarketService>();
            services.AddTransient<IInquirySpotMarketService, InquirySpotMarketCacheService>();

            //services.AddTransient<InquirySpotMarketServiceTwo>();

            //services.AddTransient<InquirySpotMarketServiceThree>();
        }

        // public static void InitSignal (IServiceCollection services) {
        //     services.AddSingleton<SignalFactory> ();
        //     services.AddTransient<ITransactionHubService, RemoteTransactionHubService> ();
        //     // services.AddTransient<ITransactionHubService, TransactionHubService> ();
        // }


        public static IServiceProvider GenerateServiceProviderForTesting()
        {
            var service = GenerateServiceCollectionForTesting();
            var output = service.BuildServiceProvider();
            return output;
        }

        public static ServiceCollection GenerateServiceCollectionForTesting()
        {
            var service = new ServiceCollection();
            Init(service);
            service.AddTransient<ILogHubService, DevLogHubService>();

            service.AddTransient<IRemittanceOutgoingValidatorService, DummyRemittanceOutgoingValidatorService>();
            service.AddTransient<IBtcConfirmTransactionInquiry, DummyResultBtcConfirmTransactionInquiry>();

            service.AddDbContext<DashboardContext>(options =>
                    options.UseInMemoryDatabase("dashboard"));
            service.AddDbContext<ApplicationContext>(options =>
                options.UseInMemoryDatabase("unit_test"))
                //.AddUnitOfWork<ApplicationContext>()
                ;

            service.AddDbContext<GraphDbContext>(options =>
                options.UseInMemoryDatabase("graph_db"));
            service.AddDbContext<LoggingContext>(options =>
                options.UseInMemoryDatabase("log_unit_test"));
            service.AddDbContext<LoggingExtContext>(options =>
                options.UseInMemoryDatabase("log_unit_test_ext"));
            service.AddTransient<ITransactionHubService, FakeTransactionHubService>();

            service.AddTransient<IReplayPlayerService, DevReplayPlayerService>();
            service.AddTransient<IReplayValidationService, DevReplayValidationService>();
            service.AddTransient<IValidationCancelOrderAllService, DevValidationCancelOrderAllService>();
            //service.AddTransient<IOrderMatchService, OrderItemMatchService>();

            //service.AddTransient<OrderItemMatchService>();

            service.AddTransient<OrderItemMainTestService>();
            service.AddTransient<IInquiryUserService, DevInquiryUserService>();
            service.AddTransient<ICustomTelemetryService, DummyTelemetryService>();
            service.AddTransient<IBitcoinGetBalance, DummyTwoTransactionBitcoinGetBalance>();
            service.AddTransient<IFunctionBitcoinService, FunctionBitcoinServiceFake>();


            InitOutgoingRemittanceDev(service);

            return service;
        }


        public static IServiceProvider GenerateServiceProviderConsole(string defaultConnString,
          string logConnString, ServiceCollection serviceCollection)
        {
            //var service = new ServiceCollection();

            //var mainContext = "Data Source=.; Initial Catalog=orangedbprod; Integrated Security=True; MultipleActiveResultSets=False;";
            //defaultConnString = mainContext;
            serviceCollection
            .AddTransient<RepoGraphExt>()
            .AddDbContext<DashboardContext>(options =>
                options.UseInMemoryDatabase("dashboard"))
            .AddDbContext<GraphDbContext>(options =>
                options.UseInMemoryDatabase("graph_db"))
            //.AddDbContext<GraphDbContext>(options =>
            //    options.UseSqlServer(graphDbContextConnString))
            .AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(defaultConnString))
            .AddDbContext<LoggingContext>(options =>
                options.UseInMemoryDatabase("log_unit_test"))
            .AddDbContext<LoggingExtContext>(options =>
                    options.UseSqlServer(logConnString))
            .AddTransient<ITransactionHubService, FakeTransactionHubService>()

             .AddTransient<DbConnGeneratorService>()
            .AddTransient<ILogMatchService, LogMatchService>()
            .AddTransient<ReplayCaptureService>()
            .AddTransient<FakeSpotMarketService>()

            .AddTransient<RepoUser>()
            .AddTransient<IFileService, DummyFileService>()
            .AddTransient<IReplayPlayerService, DevReplayPlayerService>()
            .AddTransient<IContextSaveService, ContextSaveService>()
            .AddTransient<ICustomTelemetryService, DummyTelemetryService>()
            //.AddTransient<BtcService>()
            .AddTransient<OrderItemCancelService>();


            //.AddTransient<InquirySpotMarketService>()

            //.AddTransient<InquirySpotMarketServiceTwo>()

            //.AddTransient<InquirySpotMarketServiceThree>();


            Init(serviceCollection);
            InitOutgoingRemittanceDev(serviceCollection);
            serviceCollection.AddTransient<IBtcServiceSendMoney, BtcServiceClientSendMoney>();
            var output = serviceCollection.BuildServiceProvider();
            return output;

        }

        private static void InitOutgoingRemittanceDev(IServiceCollection serviceCollection)
        {
            //serviceCollection.AddTransient<IBtcCloudServiceRegisterNotification, DevBtcCloudServiceRegisterNotification>();
            serviceCollection.AddTransient<IBtcServiceSendMoney, DummyBtcServiceSendMoney>();
            //serviceCollection.AddTransient<IBtcServiceSendMoney, DummyBtcServiceSendMoneyValidationSendTransfer>();

        }


    }
}
