//using Microsoft.AspNetCore.Cors;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

using System;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace BlueLight.Main
{

    public static class WebHelper
    {

        public static string GetId(HttpContext context)
        {
            try
            {
                string p = context.Request.Path.ToString();
                // var p2 = context.Request.Path.ToUriComponent();
                // var p3 = context.Request.Path.Value;
                var p4 = p.Split('/').ToList();
                var p5 = p4.Last();
                return p5;
            }
            catch (System.Exception)
            {

                return "0";

            }

        }

        public static string GetUrl(HttpRequest request)
        {
            var url = Microsoft.AspNetCore.Http.Extensions.UriHelper.GetEncodedUrl(request);
            return url;
        }

        public static Uri GetUrlToUri(HttpRequest request)
        {
            var url = GetUrl(request);
            var uri = new Uri(url);
            return uri;
        }

        public static string GetHostName(HttpRequest req)
        {

            var Request = req;

            var scheme = Request.HttpContext.Request.Scheme;
            var host = Request.HttpContext.Request.Host;

            var fullPath = $"{scheme}://{host}";
            return fullPath;
        }

    }
}

//public static string RateDisplayExt(double input,string currency)
//{
//    var items = new string[] { "btc", "idr" };
//    if (items.Contains(currency))
//    {

//    }
//    else
//    {

//    }

//    return input.ToString();
//    //var temp = (long)input;

//    //if (temp == input)
//    //{
//    //    var output = input;
//    //    return output.ToString();
//    //}
//    //else
//    //{
//    //    var format = "0.0000000000000";
//    //    var output = input.ToString(format);
//    //    return output;
//    //}

//}