////using Microsoft.AspNetCore.Cors;
////using Microsoft.AspNetCore.Mvc;
////using Microsoft.EntityFrameworkCore;

//using Microsoft.AspNetCore.Http;
//using System;
//using System.Linq;
//using System.Web;

//namespace BlueLight.Main
//{
//    public static class WebHelper
//    {
//        public static string GetUrl(HttpRequest request)
//        {
//            var url = Microsoft.AspNetCore.Http.Extensions.UriHelper.GetEncodedUrl(request);
//            return url;
//        }

//        public static Uri GetUrlToUri(HttpRequest request)
//        {
//            var url = GetUrl(request);
//            var uri = new Uri(url);
//            return uri;
//        }

        

//    }
//}



////public static string RateDisplayExt(double input,string currency)
////{
////    var items = new string[] { "btc", "idr" };
////    if (items.Contains(currency))
////    {

////    }
////    else
////    {

////    }

////    return input.ToString();
////    //var temp = (long)input;

////    //if (temp == input)
////    //{
////    //    var output = input;
////    //    return output.ToString();
////    //}
////    //else
////    //{
////    //    var format = "0.0000000000000";
////    //    var output = input.ToString(format);
////    //    return output;
////    //}


////}