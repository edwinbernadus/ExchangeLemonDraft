using BlueLight.Main;

namespace ExchangeLemonCore.Controllers
{
    public class GlobalParamVersion
    {
        // public static int Version = 5;

        public static int Version
        {
            get
            {
                var output = VersionItem.ParamVersion;
                return output;
            }
        }
    }
}