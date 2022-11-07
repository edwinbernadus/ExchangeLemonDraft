//using System;
//using System.Collections.Generic;
//using Newtonsoft.Json.Linq;
//// ;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//// using Serilog;
//using ExchangeLemonCore.Controllers;
//using Microsoft.AspNetCore.SignalR;
////using Microsoft.AspNetCore.Mvc;
////using Microsoft.AspNetCore.Cors;
////using Microsoft.AspNetCore.Mvc;
////using Microsoft.EntityFrameworkCore;

//namespace BlueLight.Main
//{
//    [Obsolete]
//    public class ReportGraphController : Controller
//    {

//        private ApplicationContext _context;
//        private readonly GraphGeneratorService graphGeneratorService;
//        private readonly RepoGeneric repoGeneric;
//        private readonly RepoGraph repoGraph;
//        private readonly IHubContext<TransactionHub> transactionHubContext;

//        public ReportGraphController(ApplicationContext context,
//        GraphGeneratorService graphGeneratorService,
//        RepoGeneric repoGeneric,
//        RepoGraph repoGraph,
//        IHubContext<TransactionHub> transactionHubContext)
//        {
//            this._context = context;
//            this.graphGeneratorService = graphGeneratorService;
//            this.repoGeneric = repoGeneric;
//            this.repoGraph = repoGraph;
//            this.transactionHubContext = transactionHubContext;
//        }

//        // public string Index()
//        // {
//        //     return "woot";
//        // }


//        // http://localhost:5001/api/ReportGraph/btc_usd
//        [HttpGet]
//        [Route("/api/reportGraph/{id}")]
//        public async Task<List<GraphPlainData>> Get(string id)
//        //public async Task<List<Tuple<string, List<double>>>> Get()
//        {
//            var output = await repoGraph.GetItems();
//            return output;

//        }


        

//        [HttpGet]
//        [Route("/api/reportGraph/last/{id}")]
//        public async Task<GraphPlainData> GetLast(string id)
//        //public async Task<List<Tuple<string, List<double>>>> Get()
//        {
//            var output = await repoGraph.GetLastItem();
//            return output;

//        }


//    }

//}




////// http://localhost:5001/api/ReportGraph/btc_usd
////// [HttpGet]
////// [Route("/api/reportGraph/{id}")]
////public async Task<dynamic> OldGet(string id)
//////public async Task<List<Tuple<string, List<double>>>> Get()
////{
////    var date = DateTime.Now.AddHours(-1);
////    var currencyPair = id;

////    try
////    {
////        var items2a = await _context.Transactions

////            //.Where(x => date >= x.TransactionDate &&
////            .Where(x => x.TransactionDate <= date &&
////               x.CurrencyPair == currencyPair)
////            .OrderByDescending(x => x.TransactionDate)
////            .ToListAsync();
////    }
////    catch (Exception p)
////    {
////        Log.Information(p.Message);
////    }

////    List<Transaction> items = await _context.Transactions

////        //.Where(x => date >= x.TransactionDate &&
////        .Where(x => x.TransactionDate <= date &&
////           x.CurrencyPair == currencyPair)
////        .OrderByDescending(x => x.TransactionDate)
////        .ToListAsync();


////    double lastPrice = await repoGeneric.GetLastPrice(currencyPair);

////    var items2 = items.Select(x => new ReportItem()
////    {
////        CreatedDate = x.TransactionDate,
////        Id = x.Id,
////        Value = x.TransactionRate
////    }).ToList();

////    var isTraceMode = false;

////    if (isTraceMode)
////    {
////        await DebugMode(items, currencyPair, date);
////        // var debug1 = items.Select (x => new { x.TransactionDate, x.TransactionRate }).ToList ();

////        // var debug2 = await _context.Transactions
////        //     .OrderByDescending (x => x.TransactionDate)
////        //     .ToListAsync ();

////        // var debug3 = debug2.Where (x => x.CurrencyPair == currencyPair).ToList ();

////        // var debug4 = debug3.Where (x => date >= x.TransactionDate).ToList ();
////        // var debug5 = debug4.OrderByDescending (x => x.TransactionDate).ToList ();

////        // var debug6 = debug3.Where (x => x.TransactionDate >= date)
////        //     .OrderByDescending (x => x.TransactionDate)
////        //     .ToList ();

////        // var items4 = items.Select (x => new ReportItem () {
////        //     CreatedDate = x.TransactionDate,
////        //         Id = x.Id,
////        //         Value = x.TransactionRate
////        // }).ToList ();
////    }
////    // var r = new GraphGeneratorService ();

////    //r.Input = ReportGenerator.GenerateSample();
////    graphGeneratorService.Input = items2;
////    graphGeneratorService.LastPrice = lastPrice;
////    graphGeneratorService.Execute();

////    //{ "data1": [220, 240, 270, 250, 280], "data2": [180, 150, 300, 70, 120], "data3": [200, 310, 150, 100, 180]}

////    var displayPair = String.IsNullOrEmpty(currencyPair) ? "Pair1" : currencyPair;
////    var displayCollection = graphGeneratorService.Output.OrderBy(x => x.Period).Select(x => x.Value).ToList();
////    var output = new List<Tuple<string, List<double>>>();
////    output.Add(new Tuple<string, List<double>>(displayPair, displayCollection));
////    //var output3 = new MvGraphDisplay();
////    var output3 = new { displayPair = displayCollection };

////    JArray array = new JArray();
////    //array.Add(displayPair);
////    //array.Add(displayCollection);

////    //displayCollection.Add(10);
////    //displayCollection.Add(20);
////    //displayCollection.Add(30);

////    foreach (var i in displayCollection)
////    {
////        array.Add(i);
////    }

////    var o = new JObject();
////    o[displayPair] = array;

////    //string json = o.ToString();

////    return o;
////}

////async Task DebugMode(List<Transaction> items, string currencyPair, DateTime date)
////{
////    var debug1 = items.Select(x => new { x.TransactionDate, x.TransactionRate }).ToList();

////    var debug2 = await _context.Transactions
////        .OrderByDescending(x => x.TransactionDate)
////        .ToListAsync();

////    var debug3 = debug2.Where(x => x.CurrencyPair == currencyPair).ToList();

////    var debug4 = debug3.Where(x => date >= x.TransactionDate).ToList();
////    var debug5 = debug4.OrderByDescending(x => x.TransactionDate).ToList();

////    var debug6 = debug3.Where(x => x.TransactionDate >= date)
////        .OrderByDescending(x => x.TransactionDate)
////        .ToList();

////    var items4 = items.Select(x => new ReportItem()
////    {
////        CreatedDate = x.TransactionDate,
////        Id = x.Id,
////        Value = x.TransactionRate
////    }).ToList();
////}