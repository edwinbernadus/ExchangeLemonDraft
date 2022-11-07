using BlueLight.Main;
using Microsoft.EntityFrameworkCore;
//using System.Data.Entity;
using System.Threading.Tasks;
//using System.Web.Http;
//using System.Web.Mvc;

namespace BlueLight.Main
{


    public class RepoUser
    {


        private ApplicationContext db;

        public RepoUser(ApplicationContext db)
        {
            this.db = db;
        }



        public async Task<UserProfile> GetOrPopulateUserProfile(string username)
        {


            //create user profile
            //create user profile detail

            //var userName = User.Identity.Name;

            var userProfile = await GetOrDefaultUser(username);
            //var repoUser = new RepoUser();
            //var userProfile = db.UserProfiles
            //    .Include (x => x.UserProfileDetails)
            //    .FirstOrDefault (x => x.username == username);
            if (userProfile != null)
            {
                return userProfile;
            }
            else
            {

                var currencies = UserProfile.GetDefaultCurrencies();
                userProfile = UserProfile.Generate(username, currencies);

                db.UserProfiles.Add(userProfile);
                await db.SaveChangesAsync();

                return userProfile;
            }

        }


        public async Task<UserProfile> GetUser(string userName)
        {
            var context = this.db;

#if DEBUG
            if (string.IsNullOrEmpty(userName))
            {
                userName = "guest1@server.com";
            }
#endif
            var output = await context.UserProfiles

                .Include(x => x.UserProfileDetails)
                //.Include(x => x.UserProfileDetails)
                //.Include(x => x.HoldTransactions)
                //.Include(x => x.AccountTransactions)
                .FirstAsync(x => x.username == userName);
            return output;

        }

        async Task<UserProfile> GetOrDefaultUser(string userName)
        {
            var context = this.db;
#if DEBUG
            if (string.IsNullOrEmpty(userName))
            {
                userName = "guest1@server.com";
            }
#endif
            var output = await context.UserProfiles
                .Include(x => x.UserProfileDetails)
                .FirstOrDefaultAsync(x => x.username == userName);
            return output;

        }


    }
}


// public DbSet<Order> GetOrderDebugMode()
// {
//     var context = this.db;
//     var collection = context.Orders;
//     //.Include(x => x.OrderTransactions)
//     //.ThenInclude(x => x.Transaction) 
//     //.Include(x => x.OrderTransactions)
//     //.ThenInclude(x => x.Order)
//     //.ThenInclude(x => x.UserProfile)
//     //.ThenInclude(x => x.UserProfileDetails)
//     //.Include(x => x.OrderTransactions)
//     //.ThenInclude(x => x.Order)
//     //.ThenInclude(x => x.UserProfile)
//     //.ThenInclude(x => x.AccountTransactions)
//     //.Include(x => x.OrderTransactions)
//     //.ThenInclude(x => x.Order)
//     //.ThenInclude(x => x.UserProfile)
//     //.ThenInclude(x => x.HoldTransactions)
//     //.Include(x => x.UserProfile)
//     //.ThenInclude(x => x.UserProfileDetails)
//     //.Include(x => x.UserProfile)
//     //.ThenInclude(x => x.AccountTransactions)
//     //.Include(x => x.UserProfile)
//     //.ThenInclude(x => x.HoldTransactions);

//     return collection;
// }

//public static async Task<UserProfile> GetUser(DBContext context, ApiController controller)
//{
//    var userName = controller.User.Identity.Name;
//    var output = await GetUser(context, userName);
//    return output;

//}

//public static async Task<UserProfile> GetUser(DBContext context, Controller controller)
//{
//    var userName = controller.User.Identity.Name;
//    var output = await GetUser(context, userName);
//    return output;

//}