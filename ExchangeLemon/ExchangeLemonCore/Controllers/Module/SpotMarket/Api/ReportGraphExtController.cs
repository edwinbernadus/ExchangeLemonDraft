using System;
using System.Collections.Generic;
// ;
using System.Linq;
using System.Threading.Tasks;
using BlueLight.Main;
using Microsoft.AspNetCore.Mvc;
// using Serilog;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Cors;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

namespace BlueLight.Main
{

    public class ReportGraphExtController : Controller
    {
      
        public ReportGraphExtController(RepoGraphExt repoGraph)
        {
            RepoGraph = repoGraph;
        }

        public RepoGraphExt RepoGraph { get; }

        

        [HttpGet]
        [Route("/api/reportGraphExt/{id}")]
        // http://localhost:50727/api/reportGraphExt/btc_usd
        public async Task<List<ModelTableTwo>> GetTwo(string id, string from, string to)
        {
            var from2 = long.Parse(from);
            var to2 = long.Parse(to);

            var from3 = TimeHelper2.FromUnixTime(from2);
            var to3 = TimeHelper2.FromUnixTime(to2);
            List<GraphDataTwo> output = await RepoGraph.GetItemsDraft();
            
            var output2 = output.Where(x =>
           x.DateTimeSequence >= from3 &&
           x.DateTimeSequence <= to3).ToList();

            var result = output2.Select(x => ModelTableTwo.Convert(x)).ToList();
            var result2 = result.OrderBy(x => x.InputTime).ToList();
          
            return result2;
        }
        
    }

}