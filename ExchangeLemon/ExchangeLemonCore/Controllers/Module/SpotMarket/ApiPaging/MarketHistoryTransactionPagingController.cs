using System;
using System.Linq;
using System.Threading.Tasks;
using BlueLight.Main;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class MarketHistoryTransactionPagingController : Controller
{
    

    public MarketHistoryTransactionPagingController(RepoGeneric repoGeneric)
    {
        
        RepoGeneric = repoGeneric;
    }

    public RepoGeneric RepoGeneric { get; }

    // [Route("/api/marketHistoryTransaction/{id}")]
    // [Route("/api/marketHistoryTransaction/{id}&counter={counter}")]

    // [Route ("/marketHistoryTransactionPaging/list/{id}")]
    // [Route ("/marketHistoryTransactionPaging/list/{id}&counter={counter}")]

    public async Task<PagingClass<MvAccountTransaction>> List(string id, int counter = 1)
    {
        var currencyPair = id;
        var uri = WebHelper.GetUrlToUri(Request);
        var newUrl = DisplayHelper.GetNewUrlReportExtWithSuffixPaging(uri, counter);
        var output = this.RepoGeneric.GenerateMarketHistoryTransaction(currencyPair);

        var result = new PagingClass<MvAccountTransaction>();

        var itemPerPage = 3;
        await result.PopulateQuery(output, counter, newUrl, itemPerPage: itemPerPage);
        return result;

    }
    


 
    

}