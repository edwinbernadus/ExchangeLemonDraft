// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Net;
// using System.Net.Http;
// using System.Web.Http;
// using Microsoft.AspNetCore.Mvc;

// namespace BlueLight.ControllerApiGekko {
//     public class DefaultController : Controller {
//         [Route ("api/eur_usd")]
//         public string Get () {
//             return "woot";
//         }

//         [Route ("customers/{customerId}/orders")]
//         public string GetOrdersByCustomer (int customerId) {
//             var output = $"woot-{customerId}";
//             return output;
//         }

//         [Route ("api/{code}/balance")]
//         public string GetBalance (string code) {
//             var output = $"balance: {code}";
//             return output;
//         }
//     }
// }