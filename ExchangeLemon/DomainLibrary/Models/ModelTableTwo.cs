using System;

using BlueLight.Main;
public class ModelTableTwo
{

    public long time { get; set; }
    public decimal low { get; set; }
    public decimal high { get; set; }
    public decimal open { get; set; }
    public decimal close { get; set; }
    public decimal volume { get; set; }
    public string timeDisplay { get; set; }
    public string dateDisplay { get; set; }
    public object debugDisplay { get; private set; }
    public DateTime InputTime { get; private set; }

    public static ModelTableTwo Convert(GraphDataTwo input)
    {
        var t = input.DateTimeSequence;
        // DateTime t3 = t.DateTime;
        var t2 = TimeHelper2.GetEpochSeconds(t);
        var t4 = (long)t2 * 1000;
        var output = new ModelTableTwo()
        {
            InputTime = input.DateTimeSequence.DateTime,
            volume = input.Volume,
            time = t4,
            low = input.Low,
            high = input.High,
            open = input.Open,
            close = input.Close,
            timeDisplay = TimeHelper2.FromUnixTime(t2).ToLongTimeString(),
            dateDisplay = TimeHelper2.FromUnixTime(t2).ToLongDateString()
        };

        var z = output.time;
        DateTime z2 = TimeHelper2.DebugExt(z);
        output.debugDisplay = z2.ToShortDateString();

        return output;
    }

   
}
//// http://localhost:5001/api/ReportGraphExt/btc_usd
//[HttpGet]
//[Route("/api/reportGraphExt/{id}")]
//public async Task<string> Get(string id)
////public async Task<List<Tuple<string, List<double>>>> Get()
//{

//    //string format = "d-MMM-yy";
//    //string format = "d-MMM-yy HH:mm:ss,0";
//    string format = "d-MMM-yy HH:mm:ss";
//    //24-Aug-15 08:00:00,0
//    var output = await RepoGraph.GetItems();
//    var header = "Date,Open,High,Low,Close,Volume";
//    List<string> items = output.Select((x, y) =>
//           //$"{x.DateTimeInput.ToString(format)},{x.Value}," +
//           //$"{DateTime.Now.AddDays(y).ToString(format)},{x.Value + y*5}," +

//           //$"{y},{x.Value + y * 5}," +
//           //$"{DateTime.Now.AddSeconds(y).ToString(format)},{x.Value + y * 5}," +
//           //$"{x.Value + 1 + (100 *y)},{x.Value - 1 + (100 * y)} ,{x.Value + y},100" 
//           $"{DateTime.Now.AddSeconds(y).ToString(format)}," +
//           $"{GraphDetail.Display(y + 1)}" +
//           $",100"
//    ).ToList();

//    var items2 = string.Join(Environment.NewLine, items);
//    var result = header + Environment.NewLine + items2;
//    return result;

//}


//// http://localhost:5001/api/ReportGraph/btc_usd
//// [HttpGet]
//// [Route("/api/reportGraph/{id}")]
//public async Task<dynamic> OldGet(string id)
////public async Task<List<Tuple<string, List<double>>>> Get()
//{
//    var date = DateTime.Now.AddHours(-1);
//    var currencyPair = id;

//    try
//    {
//        var items2a = await _context.Transactions

//            //.Where(x => date >= x.TransactionDate &&
//            .Where(x => x.TransactionDate <= date &&
//               x.CurrencyPair == currencyPair)
//            .OrderByDescending(x => x.TransactionDate)
//            .ToListAsync();
//    }
//    catch (Exception p)
//    {
//        Log.Information(p.Message);
//    }

//    List<Transaction> items = await _context.Transactions

//        //.Where(x => date >= x.TransactionDate &&
//        .Where(x => x.TransactionDate <= date &&
//           x.CurrencyPair == currencyPair)
//        .OrderByDescending(x => x.TransactionDate)
//        .ToListAsync();


//    double lastPrice = await repoGeneric.GetLastPrice(currencyPair);

//    var items2 = items.Select(x => new ReportItem()
//    {
//        CreatedDate = x.TransactionDate,
//        Id = x.Id,
//        Value = x.TransactionRate
//    }).ToList();

//    var isTraceMode = false;

//    if (isTraceMode)
//    {
//        await DebugMode(items, currencyPair, date);
//        // var debug1 = items.Select (x => new { x.TransactionDate, x.TransactionRate }).ToList ();

//        // var debug2 = await _context.Transactions
//        //     .OrderByDescending (x => x.TransactionDate)
//        //     .ToListAsync ();

//        // var debug3 = debug2.Where (x => x.CurrencyPair == currencyPair).ToList ();

//        // var debug4 = debug3.Where (x => date >= x.TransactionDate).ToList ();
//        // var debug5 = debug4.OrderByDescending (x => x.TransactionDate).ToList ();

//        // var debug6 = debug3.Where (x => x.TransactionDate >= date)
//        //     .OrderByDescending (x => x.TransactionDate)
//        //     .ToList ();

//        // var items4 = items.Select (x => new ReportItem () {
//        //     CreatedDate = x.TransactionDate,
//        //         Id = x.Id,
//        //         Value = x.TransactionRate
//        // }).ToList ();
//    }
//    // var r = new GraphGeneratorService ();

//    //r.Input = ReportGenerator.GenerateSample();
//    graphGeneratorService.Input = items2;
//    graphGeneratorService.LastPrice = lastPrice;
//    graphGeneratorService.Execute();

//    //{ "data1": [220, 240, 270, 250, 280], "data2": [180, 150, 300, 70, 120], "data3": [200, 310, 150, 100, 180]}

//    var displayPair = String.IsNullOrEmpty(currencyPair) ? "Pair1" : currencyPair;
//    var displayCollection = graphGeneratorService.Output.OrderBy(x => x.Period).Select(x => x.Value).ToList();
//    var output = new List<Tuple<string, List<double>>>();
//    output.Add(new Tuple<string, List<double>>(displayPair, displayCollection));
//    //var output3 = new MvGraphDisplay();
//    var output3 = new { displayPair = displayCollection };

//    JArray array = new JArray();
//    //array.Add(displayPair);
//    //array.Add(displayCollection);

//    //displayCollection.Add(10);
//    //displayCollection.Add(20);
//    //displayCollection.Add(30);

//    foreach (var i in displayCollection)
//    {
//        array.Add(i);
//    }

//    var o = new JObject();
//    o[displayPair] = array;

//    //string json = o.ToString();

//    return o;
//}

//async Task DebugMode(List<Transaction> items, string currencyPair, DateTime date)
//{
//    var debug1 = items.Select(x => new { x.TransactionDate, x.TransactionRate }).ToList();

//    var debug2 = await _context.Transactions
//        .OrderByDescending(x => x.TransactionDate)
//        .ToListAsync();

//    var debug3 = debug2.Where(x => x.CurrencyPair == currencyPair).ToList();

//    var debug4 = debug3.Where(x => date >= x.TransactionDate).ToList();
//    var debug5 = debug4.OrderByDescending(x => x.TransactionDate).ToList();

//    var debug6 = debug3.Where(x => x.TransactionDate >= date)
//        .OrderByDescending(x => x.TransactionDate)
//        .ToList();

//    var items4 = items.Select(x => new ReportItem()
//    {
//        CreatedDate = x.TransactionDate,
//        Id = x.Id,
//        Value = x.TransactionRate
//    }).ToList();
//}