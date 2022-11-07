//using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;

namespace ExchangeLemonCore
{

    public partial class LogMvc
    {


        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string CallerName { get; set; }
        public string Content { get; set; }
        public string Headers { get; set; }
        public string Url { get; set; }

        public string From { get; set; }
        public string UserAgent { get; set; }
        public string ErrorMessage { get; set; }

        public bool IsValid
        {
            get
            {
                var output = ErrorMessage == null;
                return output;
            }
        }



        //public void Populate(HttpRequest request)
        //{





        //    var headers = request.Headers;

        //    var requestQueryString = request.HttpContext.Request.QueryString;
        //    var headers2 = JsonConvert.SerializeObject(headers);

        //    var userAgent = headers[HttpRequestHeaders.UserAgent];
        //    //var userAgent = headers.UserAgent?.ToString() ?? "";
        //    //var url = request.RequestUri.ToString();
        //    var url = requestQueryString.Value;
        //    //var from = headers.From?.ToString() ?? "";
        //    var from = headers[HttpRequestHeaders.From];


        //    this.Headers = headers2;
        //    this.UserAgent = userAgent;
        //    this.Url = url;
        //    this.From = from;

        //}

        //public void Populate(HttpRequest request, Exception ex)
        //{


        //    if (request != null)
        //    {
        //        Populate(request);
        //    }
        //    var m = ex.Message;
        //    this.ErrorMessage = m;
        //}

    }
}