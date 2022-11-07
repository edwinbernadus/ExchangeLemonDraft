//using Microsoft.AspNetCore.Cors;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

using System;
using System.Linq;
using System.Web;

namespace BlueLight.Main
{
    public class DisplayHelper

    {

        public static string RateDisplayExt(decimal input, string currency)
        {

            var items = new string[] { "usd", "idr" };
            if (items.Contains(currency))
            {
                var a = input.ToString("0.00");
                return a;
                     
            }
            else
            {
                var a = input.ToString("0.00000000");
                return a;
            }
}

        public static string RateDisplay(decimal input)
        {
            var format = "0.00000000";
            var output = input.ToString(format);
            return output;
            //var temp = (long)input;

            //if (temp == input)
            //{
            //    var output = input;
            //    return output.ToString();
            //}
            //else
            //{
            //    var format = "0.00000000";
            //    var output = input.ToString(format);
            //    return output;
            //}

        }

 

        public static string GetFirstPair(string input)
        {
            var items = input.Split('_');
            var output = items.First().ToUpper();
            return output;
        }
        public static string GetUserName(string input)
        {
            var items = input.Split('@');
            var output = items.First();
            return output;
        }

        public static string ShowPair(string input)
        {
            var items = input.Split('_');
            var output = items[0].ToUpper() + "/" + items[1].ToUpper();
            return output;
        }

        public static string ShowPairTitle(string input)
        {
            var items = input.Split('_');
            var output = items[0].ToUpper() + " / " + items[1].ToUpper();
            return output;
        }

        public static string GetController(Uri url)
        {
            var t1 = url.Segments[1];
            var t2 = t1.ToLower();
            var t3 = t2.TrimEnd('/');
            var output = t3;
            return output;
        }



          public static string GetNewUrlReportExtWithSuffixPaging (Uri url, int counter = 1) {
            var a = url.Authority;

            var suffix = "paging";
            
            var controller1 = DisplayHelper.GetController (url);
            var controller2 = controller1.ToLower().Replace(suffix,"");
            var lastSegment = url.Segments.Last ();
            var newUrl = url.Scheme + "://" + a + "/" + controller2 + suffix;
            var newUrl2 = newUrl + "/list/" + lastSegment;
            var output = newUrl2;
            return output;
        }

    }
}



        // public static string GetNewUrlReportExtWithPagingPrefix(Uri url,int counter = 1)
        // {
        //     var a = url.Authority;
        //     //var b = url.LocalPath;

        //     var controller1 = GetController(url);

        //     var newUrl = url.Scheme + "://" + a + "/" + controller1;
        //     var newUrl2 = newUrl + "Paging" + "/list";
        //     //var newUrl3 = newUrl2 + "/" + counter;
        //     var output = newUrl2;
        //     //var controller1 = DisplayHelper.GetController(url);
        //     //var newUrl2 = newUrl.ToLower().Replace(controller1, "api");
        //     //var newUrl3 = newUrl2 + "/" + controller1;
        //     //var output = newUrl3;
        //     return output;
        // }


        // public static string GetNewUrlReportExt(Uri url,int counter = 1)
        // {
        //     var a = url.Authority;
        //     //var b = url.LocalPath;

        //     var controller1 = GetController(url);

        //     var newUrl = url.Scheme + "://" + a + "/" + controller1;
        //     var newUrl2 = newUrl + "/list";
        //     //var newUrl3 = newUrl2 + "/" + counter;
        //     var output = newUrl2;
        //     //var controller1 = DisplayHelper.GetController(url);
        //     //var newUrl2 = newUrl.ToLower().Replace(controller1, "api");
        //     //var newUrl3 = newUrl2 + "/" + controller1;
        //     //var output = newUrl3;
        //     return output;
        // }

        // public static string GetNewUrlReportExtCore(Uri url, int counter = 1)
        // {
        //     var a = url.Authority;
        //     //var b = url.LocalPath;

            

        //     var hostName = url.Scheme + "://" + a;

        //     hostName = ParamRepo.WebApiConnString;
        //     var controller1 = GetController(url);
        //     var newUrl = hostName + "/" + controller1;
        //     var newUrl2 = newUrl + "/list";
        //     //var newUrl3 = newUrl2 + "/" + counter;
        //     var output = newUrl2;
        //     //var controller1 = DisplayHelper.GetController(url);
        //     //var newUrl2 = newUrl.ToLower().Replace(controller1, "api");
        //     //var newUrl3 = newUrl2 + "/" + controller1;
        //     //var output = newUrl3;
        //     return output;
        // }

        

        //internal static string GetNewUrlReportApiExt(HttpRequestBase request)
        //{
        //    var req = request;
        //    var a = req.Url.Authority;
        //    var b = req.Url.LocalPath;
        //    var newUrl = req.Url.Scheme + "://" + a + b + "/list";
        //    return newUrl;
        //}


        // public static string GetNewUrlReport(Uri url)
        // {
        //     var a = url.Authority;
        //     var b = url.LocalPath;

        //     var newUrl = url.Scheme + "://" + a + b;
        //     var controller1 = DisplayHelper.GetController(url);
        //     var newUrl2 = newUrl.ToLower().Replace(controller1, "api");
        //     return newUrl2;
        // }

        //public static string RateDisplayShort(double input)
        //{
        //    var output = input.ToString("0.00");
        //    return output;
        //}

        //public static string AmountDisplay(double input)
        //{
        //    var output = input.ToString("0.000");
        //    return output;
        //}


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