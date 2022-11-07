using System.Collections.Generic;
using BlueLight.Main;
// using Serilog;

namespace ExchangeLemonCore
{
    public class ReplayValidationItem
    {
        public List<UserProfileDetail> UserProfileDetails { get;  set; }
        public List<Order> Orders { get; set; }
    }
}

