using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BlueLight.Main;
using System.Collections.Generic;
using System;
using BackEndClassLibrary;
using System.Linq;

namespace ExchangeLemonCore.Controllers.Admin
{

    public class PendingTransferListsController : Controller
    {
        private readonly ApplicationContext _context;

        public PendingTransferListsController(ApplicationContext context,
            PendingTransferListsRepo repo, RemittanceOutgoingAdminService transferBtcService)
        {
            _context = context;
            Repo = repo;
            TransferBtcService = transferBtcService;
        }

        public PendingTransferListsRepo Repo { get; }
        public RemittanceOutgoingAdminService TransferBtcService { get; }

        // GET: PendingTransferLists
        public async Task<IActionResult> Index()
        {
            List<PendingTransferList> output = await Repo.GetPendingListAsync();
            var result = output.Select(x => new MvPendingTransferList(x)).ToList();


            var failedItems = await Repo.GetFailedSendListAsync();
            var totalFailedItems = failedItems.Count();
            ViewBag.TotalFailed = totalFailedItems;

            var total = result
                .Where(x => x.IsApprove)
                .Sum(x => x.Amount);
            ViewBag.Total = total;

            return View(result);
        }

        public async Task<IActionResult> List(long id)
        {
            
            List<PendingTransferList> output = await Repo.GetHistory(id);

          
            var result = output.Select(x => new MvPendingTransferList(x)).ToList();
            return View(result);
        }


        public async Task<IActionResult> Cancel(long id)
        {
            await this.TransferBtcService.Reject(id);
            return RedirectToAction("index");
        }

        public async Task<IActionResult> Approve(long id)
        {
            await this.TransferBtcService.Approve(id);
            return RedirectToAction("index");
        }

        public async Task<IActionResult> Send()
        {
            await this.TransferBtcService.SendAndReleaseHold();
            return RedirectToAction("index");
        }

        public async Task<IActionResult> ReSend()
        {
            await this.TransferBtcService.FixSentFailed();
            return RedirectToAction("index");
        }

        public async Task<IActionResult> SentFailed()
        {
            List<PendingTransferList> output = await Repo.GetFailedSendListAsync();
            var result = output.Select(x => new MvPendingTransferList(x)).ToList();

            var total = result
                .Sum(x => x.Amount);
            ViewBag.Total = total;

            return View(result);
        }




    }
}
