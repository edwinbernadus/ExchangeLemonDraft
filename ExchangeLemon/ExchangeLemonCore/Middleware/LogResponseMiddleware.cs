using System;

// using Coravel;
// using Coravel.Scheduling.Schedule.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.Logging;
using BlueLight.Main;

namespace ExchangeLemonCore
{
    public class LogResponseMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LogResponseMiddleware> _logger;
        private Func<string, Exception, string> _defaultFormatter = (state, exception) => state;

        public LogResponseMiddleware(RequestDelegate next, ILogger<LogResponseMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var bodyStream = context.Response.Body;

            var responseBodyStream = new MemoryStream();
            context.Response.Body = responseBodyStream;
            // var m = context.TraceIdentifier;


            await _next(context);

            responseBodyStream.Seek(0, SeekOrigin.Begin);
            var responseBody = new StreamReader(responseBodyStream).ReadToEnd();


            _logger.Log(LogLevel.Information, 1, $"RESPONSE LOG: {responseBody}", null, _defaultFormatter);

            var sessionId = context.TraceIdentifier;
            var statusCode = context.Response.StatusCode;
            await SaveLog(sessionId, responseBody, statusCode);

            responseBodyStream.Seek(0, SeekOrigin.Begin);
            await responseBodyStream.CopyToAsync(bodyStream);
        }

        private async Task SaveLog(String sessionId, string response, int statusCode)
        {

            var httpRaw = new LogHttpRaw()
            {
                IsRequest = false,
                Content = response,
                // SessionId = sessionId,
                StatusCode = statusCode,
                SessionId = sessionId

            };
            var loggingContext = LoggingContext.Generate();
            loggingContext.LogHttpRaws.Add(httpRaw);
            await loggingContext.SaveChangesAsync();
        }
    }



}
