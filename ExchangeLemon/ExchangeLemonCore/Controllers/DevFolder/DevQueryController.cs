using System.Collections.Generic;
using System.Threading.Tasks;
using BlueLight.Main;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExchangeLemonCore.Controllers
{
    // http://localhost:5000/api/devquery/GetOpenOrderFromUser
    // http://localhost:5000/devquery/GetOpenOrderFromUser
    // [ApiController]
    public class DevQueryController : Controller
    {
        RepoOpenOrder _repoOpenOrder;
        private readonly IUnitOfWork unitOfWork;

        public DevQueryController(RepoOpenOrder repoOpenOrder, IUnitOfWork unitOfWork)
        {
            this._repoOpenOrder = repoOpenOrder;
            this.unitOfWork = unitOfWork;
        }
        public async Task<dynamic> GetOpenOrderFromUser()
        {
            var items = await _repoOpenOrder.GetOpenOrdersList(1);
            return items;
        }

        // http://localhost:53252/devQuery/testSave
        public async Task<bool> TestSave()
        {
            var accountBalanceRepo = unitOfWork.GetRepository<AccountBalance>();
            AccountBalance item = new AccountBalance()
            {
                Balance = -123,
                AccountHistories = new List<AccountHistory>()
            };
            item.AccountHistories.Add(new AccountHistory()
            {
                Amount = -456
            });
            //var accountHistoryRepo = unitOfWork.GetRepository<AccountHistory>();
            await accountBalanceRepo.InsertAsync(item);
            await this.unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}