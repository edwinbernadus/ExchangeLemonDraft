using System.Collections.Generic;
using System.Threading.Tasks;
using BlueLight.Main;
using Microsoft.AspNetCore.Mvc;
// ;
using System;
using System.Linq;
//using BlueLight.Main.ViewModel;
using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackEndClassLibrary;
using MediatR;

namespace BlueLight.Main
{
    [Authorize]

    public partial class BitcoinController : Controller
    {
        //private readonly DBContext context;

        //public BitcoinController()
        //{
        //    this.context = new DBContext();
        //}

        private ApplicationContext _context;
        private readonly BitcoinAddressService _bitcoinAddressService;
        private readonly IFunctionBitcoinService _functionBitcoinService;
        private readonly RepoUser repoUser;
        private readonly BitcoinSyncJob _bitcoinSyncJob;

        //public BtcCloudService BitcoinCloudService { get; }
        public RemittanceOutgoingAdminService PendingTransferBtc { get; }
        public RemittanceOutgoingCheckAllManualService RemittanceOutgoingCheckAllManualService { get; }
        public IMediator Mediator { get; }


        //private DBContext _context = new DBContext();

        public BitcoinController(ApplicationContext context,
            BitcoinAddressService bitcoinAddressService,
            IFunctionBitcoinService functionBitcoinService,
            RepoUser repoUser, BitcoinSyncJob bitcoinSyncJob,
            //BtcCloudService bitcoinCloudService,
            RemittanceOutgoingAdminService withdrawalBtcService,
            RemittanceOutgoingCheckAllManualService remittanceOutgoingCheckAllManualService,
            IMediator mediator)
        {
            this._context = context;
            this._bitcoinAddressService = bitcoinAddressService;
            this._functionBitcoinService = functionBitcoinService;
            this.repoUser = repoUser;
            _bitcoinSyncJob = bitcoinSyncJob;
            //BitcoinCloudService = bitcoinCloudService;
            PendingTransferBtc = withdrawalBtcService;
            RemittanceOutgoingCheckAllManualService = remittanceOutgoingCheckAllManualService;
            Mediator = mediator;
        }



        public ActionResult InsertManual(string id)
        {
            var currency = id;
            var output = new MvDepositInsert()
            {
                Currency = currency
            };
            return View(output);
        }
        [HttpPost]

        public async Task<ActionResult> InsertManual(MvDepositInsert insert)
        {
            var db = this._context;

            var userName = User.Identity.Name;
            var userProfile = await db.UserProfiles
                .Include(x => x.UserProfileDetails)
                .FirstAsync(x => x.username == userName);

            var amount = insert.Amount;
            var currency = insert.Currency;

            // userProfile.AddExternalManually(amount, currency);
            // await db.SaveChangesAsync();

            var command = new AddExternalManuallyCommand()
            {
                amount2 = amount,
                currencyCode = currency,
                userName = userProfile.username
                //userProfile = userProfile
            };
            var result = await Mediator.Send(command);

            return RedirectToAction("Deposit", "Report");

            //return Content($"done - {currency}");

        }



        public async Task<ActionResult> Withdrawal(string id)
        {

            string userName = User.Identity.Name;
            var items = await _context.PendingTransferLists
            .Where(x => x.UserProfileDetail.UserProfile.username == userName)
            .OrderByDescending(x => x.CreatedDate)
            .Take(7)
            .ToListAsync();

            ViewBag.Transactions = items;


            var output = new MvDepositInsert()
            {
                Amount = 0.000006m,
                Address = "mmpvS8nchp7LS3ABmRUt8MHjZA8tuaqAtx"
            };
            ViewBag.Title = id.ToUpper();
            return View(output);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Withdrawal(MvDepositInsert deposit)

        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(deposit.Currency))
                {
                    deposit.Currency = "btc";
                }
                var userName = User.Identity.Name;
                var user = await this.repoUser.GetUser(userName);
                var detail = user.GetUserProfileDetail("btc");
                await PendingTransferBtc.SubmitHold(detail, deposit);
                //return RedirectToAction("Deposit", "Report", null);
                return RedirectToAction("Withdrawal", "Bitcoin", new { id = "btc" });

            }

            return View(deposit);
        }

        public async Task<ActionResult> Deposit(string id)
        {
            var db = this._context;

            var userName = User.Identity.Name;
            //var userProfile = await db.UserProfiles.Include (x => x.UserProfileDetails).FirstAsync (x => x.username == userName);
            var userProfile = await repoUser.GetUser(userName);

            var detail = userProfile.UserProfileDetails.First(x => x.CurrencyCode == "btc");

            // var helper = new RepoBitcoin (db);
            var item = await _bitcoinAddressService.GetOrCreatePublicAddress(detail);

            var publicAddress = item.Item1;
            var isNewAddressGenerated = item.Item2;
            if (isNewAddressGenerated)
            {
                //await this.BitcoinCloudService.Register(publicAddress);
            }

            //var repo = new BitcoinRepo();
            //string publicAddress = await repo.GetOrCreatePublicAddress(detail.Id);

            //ViewBag.Address = detail.PublicAddress;
            ViewBag.Address = publicAddress;
            ViewBag.Title = id.ToUpper();
            return View();

        }



        [HttpPost]
        public async Task<ActionResult> Deposit(string id, MvDepositInsert deposit)
        {
            var db = this._context;

            var userName = User.Identity.Name;
            //var userProfile = await db.UserProfiles.Include (x => x.UserProfileDetails).FirstAsync (x => x.username == userName);
            var userProfile = await repoUser.GetUser(userName);

            var detail = userProfile.UserProfileDetails.First(x => x.CurrencyCode == "btc");

            // var helper = new RepoBitcoin (db);
            var item = await _bitcoinAddressService.GetOrCreatePublicAddress(detail);
            var publicAddress = item.Item1;

            //var repo = new BitcoinRepo();
            //string publicAddress = await repo.GetOrCreatePublicAddress(detail.Id);

            //ViewBag.Address = detail.PublicAddress;
            ViewBag.Address = publicAddress;
            return View();

        }

        public async Task<ActionResult> SyncBtc(string id)
        {
            ViewBag.Title = id.ToUpper();
            var event1 = this._bitcoinSyncJob;

            await event1.ExecuteAsync(User.Identity.Name);
            var result = event1.ResultDiffAmount;

            var info = "Balance is up to date";
            if (result > 0)
            {
                info = $"New transaction detected. Amount: {result} BTC";
            }
            ViewBag.Info = info;

            return View();
        }

        public async Task<ActionResult> CheckSend(string id)
        {
            ViewBag.Title = id.ToUpper();
            var event1 = this.RemittanceOutgoingCheckAllManualService;

            var userName = User.Identity.Name;
            var userProfile = await _context.UserProfiles.FirstAsync(x => x.username == userName);
            var detail = await _context.UserProfileDetails.FirstAsync(x => x.UserProfile.id == userProfile.id && x.CurrencyCode == "btc");
            var result = await event1.Execute(detail.Id);
            //var result = event1.ResultDiffAmount;


            ViewBag.Info = result;

            return Content(result);
        }



    }




}