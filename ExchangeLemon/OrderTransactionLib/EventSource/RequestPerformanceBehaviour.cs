using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BlueLight.Main
{

    public class RequestPerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly Stopwatch _timer;
        private readonly ILogger<TRequest> _logger;

        public RequestPerformanceBehaviour(ILogger<TRequest> logger,
        LoggingExtContext loggingExtContext,
        IHttpContextAccessor httpContextAccessor)
        {
            _timer = new Stopwatch();

            _logger = logger;
            _loggingExtContext = loggingExtContext;
            HttpContextAccessor = httpContextAccessor;
        }

        public LoggingExtContext _loggingExtContext { get; }
        public IHttpContextAccessor HttpContextAccessor { get; }

        public async Task<TResponse> Handle(TRequest request,
        CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var name = typeof(TRequest).Name;
            var content = JsonConvert.SerializeObject(request);

            var userName = this.HttpContextAccessor.HttpContext.User.Identity.Name;


            //var t = new LogItem()
            //{
            //    ModuleName = "ReplayMode",
            //    ClassName = name,

            //    Content = content,
            //    //CallerName = propertyName,
            //    //UserName = userNameLogCapture,
            //    AddtionalContent = "0"
            //};
            //_loggingExtContext.LogItems.Add(t);
            //await _loggingExtContext.SaveChangesAsync();

            LogItemEventSource requestLogItem = LogItemEventSource.Request(request);
            requestLogItem.UserName = userName;

            this._loggingExtContext.LogItemEventSources.Add(requestLogItem);
            await this._loggingExtContext.SaveChangesAsync();

            

            _logger.LogInformation("Request: {Name} {@Request}", name, request);

            _timer.Start();

            try
            {
                var response = await next();

                _timer.Stop();

                long delayTime = 0;
                if (_timer.ElapsedMilliseconds > 500)
                {
                    delayTime = _timer.ElapsedMilliseconds;
                    // var name = typeof(TRequest).Name;

                    

                    _logger.LogWarning("Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@Request}", name, _timer.ElapsedMilliseconds, request);
                }

                LogItemEventSource responseLogItem = LogItemEventSource.Response(response, requestLogItem, delayTime);
                this._loggingExtContext.LogItemEventSources.Add(responseLogItem);
                await this._loggingExtContext.SaveChangesAsync();
                return response;
            }
            catch (System.Exception ex)
            {
                var m = ex.Message;
                _logger.LogError(m);
                LogItemEventSource errorLogItem = LogItemEventSource.Error(ex, requestLogItem);
                this._loggingExtContext.LogItemEventSources.Add(errorLogItem);
                await this._loggingExtContext.SaveChangesAsync();
                throw ex;

            }

        }
    }
}