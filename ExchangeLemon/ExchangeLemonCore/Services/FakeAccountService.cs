using System;
using System.Threading.Tasks;
using BlueLight.Main;
using ExchangeLemonCore.Models.AccountViewModels;
using Microsoft.AspNetCore.Identity;
// using Serilog;

namespace ExchangeLemonCore.Controllers
{
    public class FakeAccountService
    {



        public FakeAccountService(UserManager<ApplicationUser> UserManager, ApplicationContext db)
        {
            this.UserManager = UserManager;
            Db = db;
        }

        public UserManager<ApplicationUser> UserManager { get; }
        public ApplicationContext Db { get; }

        public async Task<string> InitAccount()
        {
            var db = this.Db;
            var totalError = "0";

            var userName = new string[] {
                "guest1@server.com",
                "guest2@server.com",
                "guest3@server.com",
                "bot_sync@server.com",
                "bot_trade@server.com"
            };

            foreach (var i in userName)
            {
                RegisterViewModel model = new RegisterViewModel()
                {
                    Email = i,
                    Password = "PasswordSuper",
                    ConfirmPassword = "PasswordSuper"
                };

                try
                {
                    var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                    var result = await UserManager.CreateAsync(user, model.Password);
                    totalError += "0";
                }
                catch (Exception ex)
                {
                    Console2.WriteLine(ex.Message);
                    // Log.Information(ex.Message);
                    totalError += "1";
                }

                try
                {
                    //var db = new DBContext();
                    var businessLogic = new RepoUser(db);
                    var user2 = await businessLogic.GetOrPopulateUserProfile(model.Email);
                    await InitFund(user2,db);
                    totalError += "0";
                }
                catch (Exception ex)
                {
                    // Log.Information(ex.Message);
                    Console2.WriteLine(ex.Message);
                    totalError += "1";
                }
            }

            return totalError;
            //return Content(totalError);
        }

        public static async Task InitFund(UserProfile user2,ApplicationContext db)
        {
            var usd = user2.GetUserProfileDetail("usd");
            if (usd.Balance == 0)
            {
                usd.Balance = 9 * 1000 * 20;
            }

            var btc = user2.GetUserProfileDetail("btc");
            if (btc.Balance == 0)
            {
                btc.Balance = 5;
            }
            await db.SaveChangesAsync();
        }

    }
}