//using ExchangeLemonCore.Data;
//using BackEndStandard;
//using ExchangeLemonCore.Data;

namespace ExchangeLemonCore.Controllers
{
    public class ImageUrlService
    {
        public string GetUrl(string ImageSrc)
        {
            var host = "https://waterbear.azurewebsites.net";
#if DEBUG
            host = "http://localhost:53252";
#endif
            var output = $"{host}/{ImageSrc}";
            return output;
        }
    }
}