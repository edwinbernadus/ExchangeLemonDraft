//using System.Threading.Tasks;
//using BlueLight.Main;
//using System.Collections.Concurrent;

//namespace BackEndClassLibrary
//{
//    public class DevBtcCloudServiceRegisterNotification : IBtcCloudServiceRegisterNotification
//    {
//        public ConcurrentQueue<string> _Queue = new ConcurrentQueue<string>();
//        public Task RegisterNotifyTransfer(string transactionId)
//        {
//            _Queue.Enqueue(transactionId);
//            return Task.Delay(0);
//        }

//        public string QueueNotifyTransfer()
//        {
//            _Queue.TryDequeue(out string transactionId);
//            return transactionId;
//        }
//    }
//}


////public static IServiceProvider GenerateServiceProviderOld()
////{
////    IServiceProvider serviceProvider = new ServiceCollection()
////        .AddDbContext<DashboardContext>(options =>
////            options.UseInMemoryDatabase("dashboard"))
////        .AddDbContext<ApplicationContext>(options =>
////            options.UseInMemoryDatabase("unit_test"))
////        .AddDbContext<LoggingContext>(options =>
////            options.UseInMemoryDatabase("log_unit_test"))
////        .AddDbContext<LoggingExtContext>(options =>
////            options.UseInMemoryDatabase("log_unit_test_ext"))
////        .AddTransient<ITransactionHubService, FakeTransactionHubService>()

////        .AddTransient<LogHelperObject>()
////        .AddTransient<IContextSaveService, ContextSaveService>()
////        .AddTransient<OrderItemMainService>()
////        .AddTransient<OrderItemMatchService>()
////        .AddTransient<SignalDashboard>()
////        .AddTransient<NotificationService>()

////        .AddTransient<BitcoinSyncJob>()

////        .AddTransient<OrderTransactionService>()
////        .AddTransient<OrderItemNotificationService>()
////        .AddTransient<ILogHelperMvc, DbLogHelperMvc>()

////        .AddTransient<RepoOrderList>()
////        .AddTransient<RepoOpenOrder>()
////        .AddTransient<RepoBalance>()
////        .AddTransient<SessionGeneratorService>()
////        .AddTransient<IOrderMatchService, OrderItemMatchService>()

////        .AddTransient<ExtRepoUser>()
////        .AddTransient<ExtRepoOpenOrder>()

////        .AddTransient<DbConnGeneratorService>()
////        .AddTransient<ILogMatchService, LogMatchService>()
////        .AddTransient<ReplayCaptureService>()
////        .AddTransient<FakeSpotMarketService>()
////        .AddTransient<IReplayPlayerService, DevReplayPlayerService>()
////        .AddTransient<IReplayValidationService, DevReplayValidationService>()
////        .AddTransient<IValidationCancelOrderAllService, DevValidationCancelOrderAllService>()
////        .AddTransient<IOrderListInquiryContextService, OrderListInquiryContextService>()
////        .AddTransient<OrderListInquiryService>()

////        .BuildServiceProvider();
////    return serviceProvider;

////}


////public static IServiceProvider GenerateServiceProviderConsoleOld(string defaultConnString,
////    string logConnString, ServiceCollection serviceCollection)
////{

////    serviceCollection.AddDbContext<DashboardContext>(options =>
////        options.UseInMemoryDatabase("dashboard"))
////    .AddDbContext<ApplicationContext>(options =>
////        options.UseSqlServer(defaultConnString))
////    .AddDbContext<LoggingContext>(options =>
////        options.UseInMemoryDatabase("log_unit_test"))
////    .AddDbContext<LoggingExtContext>(options =>
////            options.UseSqlServer(logConnString))
////    .AddTransient<ITransactionHubService, FakeTransactionHubService>()

////    .AddTransient<LogHelperObject>()
////    .AddTransient<IContextSaveService, ContextSaveService>()
////    .AddTransient<OrderItemMainService>()
////    .AddTransient<OrderItemMatchService>()
////    .AddTransient<SignalDashboard>()
////    .AddTransient<NotificationService>()

////    .AddTransient<BitcoinSyncJob>()

////    .AddTransient<OrderTransactionService>()
////    .AddTransient<OrderItemNotificationService>()
////    .AddTransient<ILogHelperMvc, DbLogHelperMvc>()

////    .AddTransient<RepoOrderList>()
////    .AddTransient<RepoOpenOrder>()
////    .AddTransient<RepoBalance>()
////    .AddTransient<SessionGeneratorService>()
////    .AddTransient<IOrderMatchService, OrderItemMatchService>()

////    .AddTransient<ExtRepoUser>()
////    .AddTransient<ExtRepoOpenOrder>()

////    .AddTransient<DbConnGeneratorService>()
////    .AddTransient<ILogMatchService, LogMatchService>()
////    .AddTransient<ReplayCaptureService>()
////    .AddTransient<FakeSpotMarketService>()

////    .AddTransient<RepoUser>()
////    .AddTransient<IFileService, DummyFileService>()
////    .AddTransient<OrderItemCancelService>();


////    var output = serviceCollection.BuildServiceProvider();
////    return output;

////}