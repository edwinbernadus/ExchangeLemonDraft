using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueLight.Main;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//
//using Microsoft.AspNetCore.Cors;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

namespace BlueLight.Main
{

    // [Route("api/[controller]")]
    // [ApiController]
    //[EnableCors("CorsPolicy"), Route("api/[controller]")]
    public class CancelController : Controller
    {

        private ApplicationContext _context;
        private readonly OrderItemCancelService event1;
        private readonly RepoUser repoUser;

        private readonly LogHelperBot _logHelperBot;

        public CancelController(ApplicationContext context,
                    OrderItemCancelService event1, RepoUser repoUser,
                    LogHelperBot logHelperBot,
                    IMediator mediator)
        {
            _logHelperBot = logHelperBot;
            this._context = context;
            this.event1 = event1;
            this.repoUser = repoUser;
            Mediator = mediator;
        }

        public IMediator Mediator { get; }

        // GET api/values
        //[HttpGet]
        [HttpPost]
        [Route("api/cancel/{id}")]
        public async Task<bool> Post(int id)
        {

            var userProfile = await repoUser.GetUser(User.Identity.Name);
            var userName = userProfile.username;

            var LogHelperBot = this._logHelperBot;
            await LogHelperBot.Save(id, userName);


            var orderId = id;
            //await event1.ExecuteFromOrder(orderId, User.Identity.Name);

            var command = new CancelByIdQueueCommand()
            {
                orderId = orderId,
                userNameLogCapture = User.Identity.Name,
                //includeLog = true
            };
            await Mediator.Send(command);
            return true;
        }

    }
}