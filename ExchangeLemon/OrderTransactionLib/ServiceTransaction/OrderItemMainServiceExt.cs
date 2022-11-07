using ExchangeLemonCore.Controllers;

namespace BlueLight.Main
{
    public class OrderItemMainServiceExt
    {
        public OrderItemMainServiceExt(OrderTransactionService orderTransactionService,
                    OrderItemNotificationService orderItemNotificationService,
                    ReplayCaptureService replayCaptureService,
                    IReplayValidationService replayValidationService,
                    IOrderListInquiryContextService orderListInquiryContextService,
                    OrderItemMainFlag orderItemMainFlag,
                    OrderItemMainValidationDebug orderItemMainValidationDebug,
                    IValidationCancelOrderAllService validationCancelOrderAllService,
                    
                    LogHelperObject logHelperObject,
                    SignalDashboard signalDashboard,

                    RepoUser repoUser,
                    IInquiryUserService inquiryUserService,
                    ICustomTelemetryService customTelemetryService)
        {
            OrderTransactionService = orderTransactionService;
            OrderItemNotificationService = orderItemNotificationService;
            ReplayCaptureService = replayCaptureService;
            ReplayValidationService = replayValidationService;
            OrderListInquiryContextService = orderListInquiryContextService;
            OrderItemMainFlag = orderItemMainFlag;
            OrderItemMainValidationDebug = orderItemMainValidationDebug;
            ValidationCancelOrderAllService = validationCancelOrderAllService;
            LogHelperObject = logHelperObject;
            SignalDashboard = signalDashboard;
            RepoUser = repoUser;
            InquiryUserService = inquiryUserService;
            CustomTelemetryService = customTelemetryService;
        }

        public OrderTransactionService OrderTransactionService { get; }
        public OrderItemNotificationService OrderItemNotificationService { get; }
        public ReplayCaptureService ReplayCaptureService { get; }
        public IReplayValidationService ReplayValidationService { get; }
        public IOrderListInquiryContextService OrderListInquiryContextService { get; }
        public OrderItemMainFlag OrderItemMainFlag { get; }
        public OrderItemMainValidationDebug OrderItemMainValidationDebug { get; }
        public IValidationCancelOrderAllService ValidationCancelOrderAllService { get; }
        public LogHelperObject LogHelperObject { get; }
        public SignalDashboard SignalDashboard { get; }
        public RepoUser RepoUser { get; }
        public IInquiryUserService InquiryUserService { get; }
        public ICustomTelemetryService CustomTelemetryService { get; }
    }
}