//using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
//using System.Web.Http;
//using System.Web.Mvc;
//using System.Data.Entity;

using Dapper;
//using System.Web.Http;
//using System.Web.Mvc;



namespace BlueLight.Main
{
    public class ExtRepoUser
    {

        private readonly DbConnGeneratorService _dbConnGeneratorService;

        public ExtRepoUser(DbConnGeneratorService dbConnGeneratorService)
        {
            _dbConnGeneratorService = dbConnGeneratorService;
        }



        public async Task<UserProfile> GetUserId(string userName)
        {
            await Task.Delay(0);
            string sQuery = "select * FROM UserProfiles"
                    + " where username = @userName";
            UserProfile output = null;
            using (var Connection = _dbConnGeneratorService.Generate())
            {
                output = Connection.Query<UserProfile>
                        (sQuery, new { userName = userName }).FirstOrDefault();
            }

            return output;
        }
    }
}
