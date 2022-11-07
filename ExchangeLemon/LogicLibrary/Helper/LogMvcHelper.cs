using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;

namespace ExchangeLemonCore
{
    


    public partial class LogMvcHelper 
    {

        public static void Populate(HttpRequest request,LogMvc output)
        {
            var headers = request.Headers;

            var requestQueryString = request.HttpContext.Request.QueryString;
            var headers2 = JsonConvert.SerializeObject(headers);

            var userAgent = headers[HttpRequestHeaders.UserAgent];
            //var userAgent = headers.UserAgent?.ToString() ?? "";
            //var url = request.RequestUri.ToString();
            var url = requestQueryString.Value;
            //var from = headers.From?.ToString() ?? "";
            var from = headers[HttpRequestHeaders.From];

            

            output.Headers = headers2;
            output.UserAgent = userAgent;
            output.Url = url;
            output.From = from;

        }

        public static void Populate(HttpRequest request, Exception ex, LogMvc output)
        {


            if (request != null)
            {
                Populate(request,output);
            }
            var m = ex.Message;

            
            output.ErrorMessage = m;
        }

    }
}