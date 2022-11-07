using System;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;

namespace ExchangeLemonCore
{

    [AttributeUsage(AttributeTargets.Method)]
    public class LocalHostAttribute : ActionFilterAttribute
    {

        string Filter(string input)
        {
            if (input == "127.0.0.1")
            {
                return "localhost";
            }
            else
            {
                return input;
            }
        }

        public override void OnActionExecuting(ActionExecutingContext c)
        {
            var address = InquiryAddress(c.HttpContext);

            var ipClient = address.ipClient;
            var ipServer = address.ipServer;
            ipClient = Filter(ipClient);
            ipServer = Filter(ipServer);

            if (ipClient != ipServer)
            {
                c.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            }
        }

        public static (string ipClient, string ipServer) InquiryAddress(HttpContext httpContext)
        {
            var c = httpContext;
            var temp0 = c.Request;
            var ipServer = temp0.Host.Host;


            IPAddress temp2 = c.Request.HttpContext.Connection.RemoteIpAddress;
            var temp1 = temp2.MapToIPv4();
            var ipClient = temp1.ToString();
            var output = (ipClient, ipServer);
            return output;

        }
    }
}