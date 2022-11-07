using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
//using Microsoft.AspNetCore.SignalR;

namespace BlueLight.Main
{
    public partial class SignalDashboard
    {
        private readonly DashboardContext _db;
        private readonly ITransactionHubService transactionHubService;



        public SignalDashboard(DashboardContext context,
            ITransactionHubService serviceTransaction)
        {
            _db = context;
            this.transactionHubService = serviceTransaction;
            //TransactionHubContext = transactionHubContext;
        }

        //public IHubContext<TransactionHub> TransactionHubContext { get; }

        public async Task<Dashboard> SavePersistantAsync(string type_event)
        {
            var db = this._db;

            var item = await db.Dashboards.FirstOrDefaultAsync(x => x.TypeEvent == type_event);
            if (item == null)
            {
                item = new Dashboard()
                {
                    TypeEvent = type_event
                };
                db.Dashboards.Add(item);
                if (ParamRepo.IsSaveDashboardEnable)
                {
                    await db.SaveChangesAsync();
                }
            }

            item.Counter++;
            item.LastUpdate = DateTime.Now;
            if (ParamRepo.IsSaveDashboardEnable)
            {
                await db.SaveChangesAsync();
            }
            var output = item;
            return output;

        }



        public async Task Submit(string type_event)
        {
            var output = await SavePersistantAsync(type_event);
            var result = JsonConvert.SerializeObject(output);

            await this.transactionHubService.SubmitDashboard(result);
            //await this.TransactionHubContext.Clients.All.SendAsync("ListenDashboard", result);
        }

        public async Task SubmitMatchOrderBuy(Order order)
        {
            var botNames = GetBotNames();
            if (botNames.ToList().Contains(order.UserProfile.username))
            {
                var code = "botMatchOrderBuy";
                await Submit(code);
            }
        }

        public async Task SubmitMatchOrderSell(Order order)
        {
            var botNames = GetBotNames();
            if (botNames.ToList().Contains(order.UserProfile.username))
            {
                var code = "botMatchOrderSell";
                await Submit(code);
            }
        }

        List<String> GetBotNames()
        {
            var item1 = "bot_buy@user.com";
            var item2 = "bot_sell@user.com";
            var output = new string[] { item1, item2 };
            return output.ToList();
        }
    }
}