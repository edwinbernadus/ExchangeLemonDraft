using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace BlueLight.Main
{
    public partial class UserProfile
    {
        public int id { get; set; }
        public string username { get; set; }

        public virtual ICollection<UserProfileDetail> UserProfileDetails { get; set; }



        public double balance
        {
            get
            {
                return -100;
            }
        }




        public static UserProfile Generate(string username, string[] currency)
        {

            var userProfile = new UserProfile()
            {
                username = username,
            };

            var details = currency.Select(x =>
           new UserProfileDetail()
           {
               CurrencyCode = x,

           }).ToList();

            userProfile.UserProfileDetails = details;
            return userProfile;

        }

        public static string[] GetDefaultCurrencies()
        {
            var currencies = new string[] { "btc", "idr", "eth", "ltc", "usd", "xlm" };
            return currencies;
        }
    }

}