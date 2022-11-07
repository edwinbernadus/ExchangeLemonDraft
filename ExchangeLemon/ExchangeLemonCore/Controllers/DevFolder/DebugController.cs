using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//using ExchangeLemonCore.Data;
//using BackEndStandard;
using BlueLight.Main;
//using ExchangeLemonCore.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Razor.TagHelpers;
// using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using MediatR;

namespace ExchangeLemonCore.Controllers
{
    public class DebugController : Controller
    {
        // private readonly TransactionHub transactionHub;
        // private readonly IHubContext<TransactionHub> transactionHubContext;
        // private readonly ITransactionHubService serviceTransaction;

        private readonly LoggingExtContext _loggingExtContext;



        private readonly ReceiveLogCaptureService _receiveLogCapture;

        public DebugController(ApplicationContext applicationDbContext,
                        LoggingExtContext loggingExtContext,
                        ICustomTelemetryService customTelemetryService,
                        ReceiveLogCaptureService receiveLogCapture,
                        IHttpClientFactory httpClientFactory,
                        IMediator mediator)
        {
            _receiveLogCapture = receiveLogCapture;
            HttpClientFactory = httpClientFactory;
            Mediator = mediator;
            _loggingExtContext = loggingExtContext;
            CustomTelemetryService = customTelemetryService;
            this._context = applicationDbContext;
            // this.transactionHubContext = transactionHubContext;
            // this.serviceTransaction = serviceTransaction;
            // this.transactionHub = transactionHub;
        }

        public ApplicationContext _context { get; }
        public ICustomTelemetryService CustomTelemetryService { get; }
        public IHttpClientFactory HttpClientFactory { get; }
        public IMediator Mediator { get; }

        // https://localhost:44343/debug/testgroupby
        //public async Task<string> TestGroupBy()
        //{
        //    //var input = await _context.Students.ToListAsync();
        //    //var result = input.GroupBy(x => x.StudentName).Select(x => new { x.Key, Total = x.Count() });

        //    var result = await _context.Students.GroupBy(x => x.StudentName).Select(x => new { Output = x.Key, Total = x.Count() }).ToListAsync();
        //    var result2 = result.Select(x => x.Output + "-" + x.Total);
        //    var output = String.Join("|", result2);
        //    return output;
        //}

        // http://localhost:5000/debug/GetUrlRabbit
        public async Task<string> GetUrlRabbit(){
            await Task.Delay(0);
            var output = RpcBackEnd.GetHostName();
            return output;
        }
        // http://localhost:5000/debug/testCqrs
        public async Task<int> TestCqrs()
        {

            var commandOne = new SendOneCommand()
            {
                Input = 1
            };
            var result = await this.Mediator.Send(commandOne);
            return result;

        }
        // http://localhost:50727/debug/callApi
        public async Task<string> CallApi()
        {
            HttpClient client = this.HttpClientFactory.CreateClient();

            string hostName = "http://localhost:50727";
            string api = "orderItemMainFunction";
            var UriString = $"{hostName}/api/{api}";

            // var UriString2 = "http://localhost:50727/api/orderItemMainTwo";

            var svm = new
            {
                rate = "1",
                amount = "2",
                mode = "3",
                current_pair = "4"
            };
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(UriString),
                Headers = {
                { HttpRequestHeader.ContentType.ToString(), "application/json" },
            },
                Content = new StringContent(JsonConvert.SerializeObject(svm))
            };

            var response = client.SendAsync(httpRequestMessage).Result;
            var output = await response.Content.ReadAsStringAsync();
            return output;
        }


        public async Task<long> CallApiOld()
        {
            HttpClient client = this.HttpClientFactory.CreateClient();

            // var header = "application/json";
            string hostName = "http://localhost:50727";
            string api = "orderItemMain";
            //client.DefaultRequestHeaders.Add("Content-Type",header);

            //client.DefaultRequestHeaders.Accept
            //    .Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            //client.DefaultRequestHeaders.Add("Accept", "application/json");
            //client.DefaultRequestHeaders.Add("Content-Type", "application/json; charset=utf-8");

            //cl.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
            //cl.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", token));

            //public string rate { get; set; }
            //public string amount { get; set; }

            //public string mode { get; set; }
            //public string current_pair { get; set; }

            var input = new
            {
                rate = "1",
                amount = "2",
                mode = "3",
                current_pair = "4"
            };
            var input2 = JsonConvert.SerializeObject(input);
            var requestUri = $"{hostName}/api/{api}";

            var result = await client.PostAsync(requestUri, new StringContent(input2));
            var result2 = await result.Content.ReadAsStringAsync();
            var total = result2.Count();
            return total;
        }

        // http://localhost:50727/debug/LogCosmos
        public async Task<string> LogCosmos()
        {
            var sessionId = Guid.NewGuid();

            var content1 = "content1";
            var event1 = "event1";

            var items = new List<string>();
            items.Add("item01");
            items.Add("item02");

            await this._receiveLogCapture.SaveRaw(sessionId, content1, event1);
            await this._receiveLogCapture.SaveAddress(sessionId, items, event1);


            return "woot";
        }

        // // http://localhost:5000/debug/TestTelemetry
        // public string TestTelemetry()
        // {
        //     TelemetryClient telemetry = new TelemetryClient();
        //
        //     var except1 = new ArgumentException("woot-error");
        //     telemetry.TrackException(except1);
        //     telemetry.TrackEvent("one");
        //     telemetry.TrackTrace("trace-1");
        //     return "woot1-123";
        // }

        // http://localhost:5000/debug/TestTelemetryTwo
        public string TestTelemetryTwo()
        {
            var except1 = new ArgumentException("woot-error");
            this.CustomTelemetryService.Submit(except1);
            return "woot1-1234567";
        }



        // http://localhost:5000/debug/TestRollBackCreate
        public async Task<ActionResult> TestRollBackCreate()
        {
            this._loggingExtContext.LogSessions.Add(new LogSession());
            await _loggingExtContext.SaveChangesAsync();
            var t = await this._context.Database.BeginTransactionAsync();
            this._context.AccountBalances.Add(new AccountBalance());
            await _context.SaveChangesAsync();
            t.Rollback();

            var errMsg = "woot 123";
            var sessionLogEnd = new LogSession();
            sessionLogEnd.IsError = true;
            sessionLogEnd.ErrorMessage = errMsg;

            this._loggingExtContext.Add(sessionLogEnd);
            await this._loggingExtContext.SaveChangesAsync();

            return Content("ok");
        }

        // http://localhost:5000/debug/TestRollBackInquiry
        public async Task<ActionResult> TestRollBackInquiry()
        {
            var t1 = await this._context.AccountBalances.CountAsync();
            var t2 = await this._loggingExtContext.LogSessions.CountAsync();
            var output = $"{t1} - {t2}";
            return Content(output);
        }

        public async Task<string> GetCollectionOrderHistories()
        {
            var items = await this._context.OrderHistories.ToListAsync();
            var output = JsonConvert.SerializeObject(items);
            return output;
        }

        public async Task<int> InsertOrderHistories()
        {

            var orderHistory = new OrderHistory()
            {
                RunningAmount = -2
            };
            _context.OrderHistories.Add(orderHistory);
            await _context.SaveChangesAsync();
            var total = await this._context.OrderHistories.CountAsync();
            return total;
        }

        // /debug/insertTwo

        public async Task<string> Version()
        {
            var localVersion = 5;
            var globalVersion = BlueLight.Main.VersionItem.ParamVersion;
            var totalRow = await GetTotalRowsOrder();
            var output = $"core - {localVersion} - {globalVersion} - totalRows: {totalRow}";
            return output;
        }

        // http://localhost:5000/debug/versiondetail
        public async Task<string> VersionDetail()
        {
            var localVersion = 5;
            var globalVersion = BlueLight.Main.VersionItem.ParamVersion;

            var totalRow = await GetTotalRowsOrder();


            var output = $"core - {localVersion} - {globalVersion} - totalRows: {totalRow}";
            return output;
        }

        private async Task<string> GetTotalRowsOrder()
        {
            try
            {

                var total = await this._context.Orders.CountAsync();
                return total.ToString();
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return message;
            }
        }

        public async Task<int> GetTotal()
        {
            var total = await this._context.Orders.CountAsync();
            return total;
        }

        public bool Error()
        {
            throw new ArgumentException("woot 123");
        }


        [Authorize]
        // http:localhost:5000/debug/inquiryToken
        public string InquiryToken()
        {
            var accessToken = Request.Headers["Authorization"];
            return accessToken;
        }


        public async Task DevQueryThree()
        {
            await Task.Delay(0);

            ICollection<Order> rawData = Enumerable.Range(0, 5).Select(x => new Order()
            {
                Id = x,
                IsOpenOrder = true
            }).ToList();

            var item0 = rawData.Where(x => x.IsOpenOrder)
                .OrderByDescending(x => x.Id);
            IQueryable<Order> item = item0.AsQueryable();

            var itemLoaded = item.First();
            itemLoaded.IsOpenOrder = false;
            var id = itemLoaded.Id;
            var isOpen = itemLoaded.IsOpenOrder;

            // await this._context.SaveChangesAsync();

            var item20 = rawData.Where(x => x.IsOpenOrder)
                .OrderByDescending(x => x.Id);
            var item2 = item20.AsQueryable();
            var itemLoaded2 = item2.First();
            var id2 = itemLoaded2.Id;
            var isOpen2 = itemLoaded2.IsOpenOrder;

        }

        public async Task DevQueryTwo()
        {
            await Task.Delay(0);
            // var item = await  this._context.Orders.FirstAsync(x => x.Id == 98);
            // item.IsOpenOrder = true;
            // await _context.SaveChangesAsync();
        }

        public async Task DevQueryOne()
        {
            var item = this._context.Orders.Where(x => x.IsOpenOrder)
                .OrderByDescending(x => x.Id);

            var itemLoaded = await item.FirstAsync();
            itemLoaded.IsOpenOrder = false;
            var id = itemLoaded.Id;
            var isOpen = itemLoaded.IsOpenOrder;

            // await this._context.SaveChangesAsync();

            var item2 = this._context.Orders.Where(x => x.IsOpenOrder)
                .OrderByDescending(x => x.Id);
            var itemLoaded2 = await item2.FirstAsync();
            var id2 = itemLoaded2.Id;
            var isOpen2 = itemLoaded2.IsOpenOrder;

            //98
            //77
        }

        public async Task DevQueryFour()
        {
            await Task.Delay(0);
            var item = this._context.Orders.Where(x => x.IsOpenOrder)
                .OrderByDescending(x => x.Id);

            var itemLoaded = item.First();
            itemLoaded.IsOpenOrder = false;
            var id = itemLoaded.Id;
            var isOpen = itemLoaded.IsOpenOrder;

            // await this._context.SaveChangesAsync();

            var item2 = this._context.Orders.Where(x => x.IsOpenOrder)
                .OrderByDescending(x => x.Id);
            // var itemLoaded2 = await item2.FirstAsync();
            var itemLoaded2 = item2.First();
            var id2 = itemLoaded2.Id;
            var isOpen2 = itemLoaded2.IsOpenOrder;

            //98
            //77
        }
    }
}