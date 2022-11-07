namespace BlueLight.Main
{
    public class ParamRepo
    {

#if DEBUG

        // public static string SignalConnectionUrl = "http://localhost:5000";
        public static string SignalConnectionUrl = "http://localhost:53252";
        //public static string SignalConnectionUrl = "https://waterbear.azurewebsites.net";

        //public static string SignalConnectionUrl = "http://localhost:50790/signalr";

        //public static string SignalConnectionUrl = "https://exchangewavetreedev.azurewebsites.net/signalr";


        // public static string SignalConnectionUrl = "http://localhost:53252/";
        // public static string SignalConnectionUrl = "http://localhost:53230";

        // public static string SignalConnectionUrl = "https://lemoncore.azurewebsites.net";

        // public static string SignalConnectionUrl = "https://exchangewavetree.azurewebsites.net/signalr";

        public static string FunctionUrlInquiryBitcoin = "https://functionappbitcoininquiry.azurewebsites.net/api/InquiryBalance?code=SOCMEwk4P63gndL3vf1EDCZ3FRSKKiLCKIQHMhePS/DXoOOxPDZxdg==";
        public static string FunctionUrlGenerateAddress = "https://functionappbitcoininquiry.azurewebsites.net/api/GenerateAddress?code=auRDIfQqsWf4oIOlWLYkKsfzJBevmMx2y8mjUEIO9PPJIVeNl2/0SQ==";

        public static string FunctionSyncBtcUrl = "http://localhost:7074";

        //public static string SqlConnString = "Data Source=.; Initial Catalog=ExchangeLemonUat; Integrated Security=True; MultipleActiveResultSets=False;";
        // public static string SqlConnString = "Data Source=.; Initial Catalog=WaterBearDbDev; Integrated Security=True; MultipleActiveResultSets=False;";
        public static string WebApiConnString = "http://localhost:56173";

        public static readonly bool IsSaveLogEnable = true;
        public static readonly bool IsSaveDashboardEnable = true;
        public static readonly bool IsDebugStatCaptureEnable = false;

        // public static readonly bool IsSaveLogEnable = false;
        // public static readonly bool IsSaveDashboardEnable = false;
        // public static readonly bool IsDebugStatCaptureEnable = false;

#else
        // public static string SignalConnectionUrl = "https://lemoncore.azurewebsites.net";
        public static string SignalConnectionUrl = "https://waterbear.azurewebsites.net";
        

        public static string FunctionUrlInquiryBitcoin = "https://functionappbitcoininquiry.azurewebsites.net/api/InquiryBalance?code=SOCMEwk4P63gndL3vf1EDCZ3FRSKKiLCKIQHMhePS/DXoOOxPDZxdg==";
        public static string FunctionUrlGenerateAddress = "https://functionappbitcoininquiry.azurewebsites.net/api/GenerateAddress?code=auRDIfQqsWf4oIOlWLYkKsfzJBevmMx2y8mjUEIO9PPJIVeNl2/0SQ==";
        public static string FunctionSyncBtcUrl = "http://localhost:7074";

        public static string SqlConnString = "Server=tcp:mainsql2.database.windows.net,1433;Initial Catalog=orangedb;Persist Security Info=False;User ID=edwin;Password=PasswordSuper;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public static string WebApiConnString = "http://localhost:56173";

        public static readonly bool IsSaveLogEnable = true;
        public static readonly bool IsSaveDashboardEnable = true;
        public static readonly bool IsDebugStatCaptureEnable = false;

#endif
    }
}