
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
    public static class GenerateAddress
    {
        [FunctionName("GenerateAddress")]
        //public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req, TraceWriter log)
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req, TraceWriter log)
        {
            log.Info("generate address");

        

            var logic = new BtcService();
            var s1 = logic.GenerateAddress();
            var output = new BitcoinGenerateAddress()
            {
                PrivateKey = s1.Item1,
                PublicAddress = s1.Item2,
            };

            return (ActionResult)new OkObjectResult(output);
            

        }
    }
}


//// parse query parameter
//string detailId = req.GetQueryNameValuePairs()
//    .First(q => string.Compare(q.Key, "id", true) == 0)
//    .Value;

//log.Info($"detail id: {detailId}");

//var context = new DBContext();
//var id = long.Parse(detailId);
//var userProfileDetail = context.UserProfileDetails.First(x => x.Id == id);
//var helper = new BitcoinAddressHelper(context);
//await helper.EnsureHasBitcoinAddress(userProfileDetail);
//var output = userProfileDetail.PublicAddress;

//return req.CreateResponse(HttpStatusCode.OK, output);
