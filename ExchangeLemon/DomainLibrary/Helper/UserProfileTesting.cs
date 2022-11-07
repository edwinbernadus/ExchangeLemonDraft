
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace BlueLight.Main
{

    public partial class UserProfile
    {
        public UserProfileLite LiteMode()
        {
            var output = new UserProfileLite()
            {
                UserId = this.id,
                UserName = this.username
            };
            return output;
        }

        public decimal holdBalanceIdrTesting
        {
            get
            {
                var item = GetUserProfileDetailTesting("idr");
                return item?.HoldBalance ?? -1;
            }
        }

        public decimal holdBalanceBtcTesting
        {
            get
            {
                var item = GetUserProfileDetailTesting("btc");
                return item?.HoldBalance ?? -1;
            }
        }

        public decimal balanceIdrTesting
        {
            get
            {
                var item = GetUserProfileDetailTesting("idr");
                return item?.Balance ?? -1;
            }
        }

        public decimal balanceBtcTesting
        {
            get
            {
                var item = GetUserProfileDetailTesting("btc");
                return item?.Balance ?? -1;
            }
        }

        public decimal availableBalanceIdrTesting
        {
            get
            {
                var output = balanceIdrTesting - holdBalanceIdrTesting;
                return output;
            }
        }


        public decimal availableBalanceBtcTesting
        {
            get
            {
                var output = balanceBtcTesting - holdBalanceBtcTesting;
                return output;
            }
        }

        

        public UserProfileDetail GetUserProfileDetailTesting(string code)
        {
            if (this.UserProfileDetails == null)
            {
                return null;

            }
            var item = this.UserProfileDetails.FirstOrDefault(x => x.CurrencyCode == code);
            if (item != null)
            {
                return item;
            }
            else
            {
                return null;
            }
        }

    }
}