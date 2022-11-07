
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using System.Threading.Tasks;
using BlueLight.Main;

namespace FunctionAppBitcoin
{
    public static class InquiryBalance
    {
        [FunctionName("InquiryBalance")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req, TraceWriter log)
        {
            log.Info("inquiry balance");


            var address = req.Query["address"];


            // parse query parameter
            //string address = req.GetQueryNameValuePairs()
            //    .First(q => string.Compare(q.Key, "address", true) == 0)
            //    .Value;

            log.Info($"address : {address}");


            var logic = new BtcService();
            var s1 = logic.GetBalance(address);

            var output = (ActionResult)new OkObjectResult(s1);
            return output;
            //return req.CreateResponse(HttpStatusCode.OK, s1);
        }
    }
}


//var context = new DBContext();
//var id = long.Parse(detailId);
//var userProfileDetail = context.UserProfileDetails.First(x => x.Id == id);
//var helper = new BitcoinAddressHelper(context);
//await helper.EnsureHasBitcoinAddress(userProfileDetail);
//var output = userProfileDetail.PublicAddress;

//return req.CreateResponse(HttpStatusCode.OK, output);
