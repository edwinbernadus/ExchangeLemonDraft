// using System;
// using System.Collections.Generic;
// using BlueLight.Main;
// using Newtonsoft.Json;
// // ;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// //using Microsoft.AspNetCore.Cors;
// //using Microsoft.AspNetCore.Mvc;
// //using Microsoft.EntityFrameworkCore;

// namespace BlueLight.Main {
//     //[EnableCors("CorsPolicy"), Route("api/[controller]")]

//     //[Route("/api/HoldTransaction")]

//     public class HoldTransactionController : Controller {
//         private ApplicationContext _context;

//         public HoldTransactionController (ApplicationContext context) {
//             this._context = context;

//         }

//         //[HttpGet("{id}")]

//         [HttpGet]
//         [Authorize]
//         [Route ("/api/holdTransaction/{id}")]
//         public async Task<List<HoldTransaction>> Get (string id) {
//             var username = User.Identity.Name;
//             var currentPair = id;

//             var userProfile = await _context.UserProfiles
//                 .FirstAsync (x => x.username == username);

//             var output = await _context.HoldTransactions
//                 //.Include(x => x.Parent)
//                 //.ThenInclude(x => x.UserProfile)

//                 //.Where(x => x.Parent.UserProfile.username == username && 
//                 .Where (x => x.Parent.UserProfile.id == userProfile.id &&
//                     x.CurrencyCode == currentPair)
//                 .OrderByDescending (x => x.Id)
//                 .Take (10)

//                 .ToListAsync ();

//             return output;

//         }
//     }
// }