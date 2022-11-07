using System.Linq;

namespace BlueLight.Main
{
    public class DbTestingHelper
    {
        public static int GetTotalUsers(ApplicationContext context)
        {
            var totalUsers = context.UserProfiles.Count();
            var output = totalUsers;
            return output;
        }
    }

}