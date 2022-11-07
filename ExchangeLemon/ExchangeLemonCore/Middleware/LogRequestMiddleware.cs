using System;

// using Coravel;
// using Coravel.Scheduling.Schedule.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http.Extensions;
using BlueLight.Main;

namespace ExchangeLemonCore
{
    public class LogRequestMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<LogRequestMiddleware> _logger;
        private Func<string, Exception, string> _defaultFormatter = (state, exception) => state;

        public LogRequestMiddleware(RequestDelegate next, ILogger<LogRequestMiddleware> logger)
        {
            this.next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var requestBodyStream = new MemoryStream();
            var originalRequestBody = context.Request.Body;

            await context.Request.Body.CopyToAsync(requestBodyStream);
            requestBodyStream.Seek(0, SeekOrigin.Begin);

            var url = UriHelper.GetDisplayUrl(context.Request);
            var requestBodyText = new StreamReader(requestBodyStream).ReadToEnd();
            _logger.Log(LogLevel.Information, 1, $"REQUEST METHOD: {context.Request.Method}, REQUEST BODY: {requestBodyText}, REQUEST URL: {url}", null, _defaultFormatter);

            requestBodyStream.Seek(0, SeekOrigin.Begin);
            context.Request.Body = requestBodyStream;


            var httpRaw = new LogHttpRaw()
            {
                IsRequest = true,
                Content = requestBodyText,
                SessionId = context.TraceIdentifier,
                Path = url,
                Method = context.Request.Method,
                UserName = context.User.Identity.Name
            };
            await SaveLog(httpRaw);




            await next(context);
            context.Request.Body = originalRequestBody;
        }


        private async Task SaveLog(LogHttpRaw httpRaw)
        {
            var loggingContext = LoggingContext.Generate();
            loggingContext.LogHttpRaws.Add(httpRaw);
            await loggingContext.SaveChangesAsync();
        }

        //class LogHttpInput : LogHttpBase
        //{
        //    public string userName;
        //}


        //class LogHttpBase
        //{
        //    public string data;
        //    public string path;
        //    public string method;
        //    public string sessionId;

        //}
    }



}
