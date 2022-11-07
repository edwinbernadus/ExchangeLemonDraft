using System;
using System.Linq;
using System.Threading.Tasks;
using BlueLight.Main;
using BotWalletWatcher;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
// using Serilog;

namespace ExchangeLemonCore.Controllers
{

    public class DevAdminController : Controller
    {
        private readonly DashboardContext dashboardContext;
        private readonly OrderItemCancelAllService orderItemCancelAllService;
        private RepoUser repoUser;
        public ApplicationContext ApplicationContext { get; }
        public LoggingContext LoggingContext { get; }
        public UserManager<ApplicationUser> _userManager { get; }
        public IMediator Mediator { get; }
        public BlockChainPersistantService BlockChainPersistantService { get; }
        public SignalDashboard SignalDashboard { get; }

        // public BlockChainPositionService BlockChainPositionService { get; }

        private EnsureContextService ensureContextService;

        //public LoggingContext LoggingContext { get; }
        //public LoggingExtContext LoggingExtContext { get; }
        //public FakeAccountService FakeAccountService { get; }
        //public FakeSpotMarketService FakeSpotMarketService { get; }

        public DevAdminController(
            DashboardContext dashboardContext,
            OrderItemCancelAllService orderItemCancelAllService,
            RepoUser repoUser,
            UserManager<ApplicationUser> userManager,
            EnsureContextService ensureContextService,
            ApplicationContext applicationContext,
            LoggingContext loggingContext,
            IMediator mediator,
            BlockChainPersistantService BlockChainPersistantService,
            SignalDashboard signalDashboard
            )
        {
            this.dashboardContext = dashboardContext;
            this.orderItemCancelAllService = orderItemCancelAllService;
            this.repoUser = repoUser;
            ApplicationContext = applicationContext;
            LoggingContext = loggingContext;
            _userManager = userManager;
            //LoggingContext = loggingContext;
            //LoggingExtContext = loggingExtContext;
            //this.FakeAccountService = FakeAccountService;
            //this.FakeSpotMarketService = FakeSpotMarketService;
            this.ensureContextService = ensureContextService;
            Mediator = mediator;
            this.BlockChainPersistantService = BlockChainPersistantService;
            SignalDashboard = signalDashboard;
            // this.BlockChainPositionService = BlockChainPositionService;
        }


        // http://localhost:5000/devAdmin/testDashboard
        public async Task<bool> TestDashboard()
        {
            await this.SignalDashboard.Submit("one");
            return true;

        }

        // http://localhost:5000/devAdmin/testDashboardTwo/two
        public async Task<bool> TestDashboardTwo(string id)
        {
            await this.SignalDashboard.Submit(id);
            return true;

        }

        // devAdmin/inquiryConfig
        public string InquiryConfig()

        {
            var output = "";

            var isLock = FeatureRepo.UseTransaction;
            output += $"Transaction Lock: {isLock}";
            return output;
        }


        // devAdmin/InquiryBlockChain
        public async Task<string> InquiryBlockChain()

        {
            var output = "";
            int localBlockChain = await this.BlockChainPersistantService.InquiryLastBlock();
            // uint extBlockChain = await BlockChainPositionService.GetNodePosition();
            uint extBlockChain = 0;
            var diff = extBlockChain - localBlockChain;
            output = $"result inquiry blockChain: LOCAL-{localBlockChain} | EXT-{extBlockChain} | DIFF-{diff}";
            return output;
        }




        // /devAdmin/GetLogCount
        public async Task<long> GetLogCount()
        {
            var total = await this.LoggingContext.LogHttpRaws.CountAsync();
            return total;
        }

        // devAdmin/AdjustLoginAccount
        public async Task<bool> AdjustLoginAccount()
        {
            var userProfiles = await ApplicationContext.UserProfiles.ToListAsync(); ;
            var listUserNames = userProfiles
                        .Select(x => x.username).ToList();

            foreach (var userName in listUserNames)
            {
                var input = userName + "@server.com";
                var user = new ApplicationUser { UserName = input, Email = input };
                var password = "PasswordSuper";
                var result = await _userManager.CreateAsync(user, password);
            }

            foreach (var user in userProfiles)
            {
                user.username = user.username + "@server.com";
            }
            await ApplicationContext.SaveChangesAsync();

            return false;
        }

        //localhost:5000/devadmin/ensureContext
        public async Task<bool> EnsureContext()
        {
            //await ApplicationContext.Database.EnsureCreatedAsync();
            ////await LoggingContext.Database.EnsureCreatedAsync();
            //await LoggingExtContext.Database.EnsureCreatedAsync();

            //var context = ApplicationContext;

            //await FakeSpotMarketService.EnsureDataPopulated();
            //var totalError = await FakeAccountService.InitAccount();

            await this.ensureContextService.Execute();
            return true;
        }



        public bool ForceStopEnable()
        {
            ParamSpecial.IsForceStop = true;
            return ParamSpecial.IsForceStop;
        }


        public bool ForceStopDisable()
        {
            ParamSpecial.IsForceStop = false;
            return ParamSpecial.IsForceStop;
        }


        // http://localhost:5000/devadmin/forcestopinquiry
        public bool ForceStopInquiry()
        {
            return ParamSpecial.IsForceStop;
        }

        // devAdmin/CancelAllUsers
        public async Task CancelAllUsers()
        {
            var users = await this.ApplicationContext.UserProfiles.ToListAsync();
            foreach (var user in users)
            {
                var userProfileLiteMode = user.LiteMode();
                var command = new CancelAllQueueCommand()
                {
                    UserId = userProfileLiteMode.UserId,
                    UserName = userProfileLiteMode.UserName
                };
                var isCancelAll = await Mediator.Send(command);



                //await orderItemCancelAllService.Execute(user.LiteMode());
            }

        }

        // http://localhost:5000/devAdmin/TestInsertSpeed
        public async Task<IActionResult> TestInsertSpeed()
        {
            var _context = this.dashboardContext;

            _context.LogDetails.Add(new LogDetail()
            {
                ModuleName = "TestInsert",
                Content = Guid.NewGuid().ToString()
            });
            await _context.SaveChangesAsync();

            var output = await _context.LogDetails
            .Where(x => x.ModuleName == "TestInsert")
            .OrderByDescending(x => x.Id)
            .Take(5)
            .ToListAsync();

            var result = output.Select(x => new LogDetail()
            {
                ModuleName = x.ModuleName,
                Content = x.Content,
                CreatedDate = x.CreatedDate,
                Id = x.Id

            }).ToList();



            return View(result);
            // return Content("test one");
        }

        // devAdmin/ResetHoldBalance
        public async Task<bool> ResetHoldBalance()
        {

            var listUserNames = await ApplicationContext.UserProfiles
                .Select(x => x.username)
                .ToListAsync();

            var output = false;
            foreach (var userName in listUserNames)
            {
                Console2.WriteLine($"Cancelling all orders - {userName}");
                var userProfile = await repoUser.GetUser(userName);
                //var isCancelled = await orderItemCancelAllService.Execute(userProfile.LiteMode());

                var userProfileLiteMode = userProfile.LiteMode();
                var command = new CancelAllQueueCommand()
                {
                    UserId = userProfileLiteMode.UserId,
                    UserName = userProfileLiteMode.UserName
                };
                var isCancelled = await Mediator.Send(command);

                if (isCancelled)
                {
                    output = true;
                }

                var details = userProfile.UserProfileDetails;
                foreach (var detail in details)
                {
                    detail.HoldBalance = 0;
                }

                Console2.WriteLine($"Reset hold amount - {userName}");
                await this.ApplicationContext.SaveChangesAsync();
            }

            return output;

        }


    }
}


// public bool UpdateRate(int id)
// {
//     var rate = id;
//     this.streamHub.Clients.All.SendAsync("Counter", 20, 1000);

//     var period1 = DateTimeHelper.Convert(DateTime.Now);
//     var period = DateTimeHelper.GetSequence(period1);

//     var output = repoGraph.GetItemDetail(period1, rate);
//     var result = output.Value;
//     this.transactionHub.Clients.All.SendAsync("ListenGraph", "btc_usd", period, rate);
//     return true;
// }