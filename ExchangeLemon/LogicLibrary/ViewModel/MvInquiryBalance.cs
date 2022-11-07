//using System.Linq;
//using System.Web.Mvc;
//using System.Web.Mvc;
//using Microsoft.AspNetCore.Cors;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

namespace BlueLight.Main
{
    public class MvInquiryBalance
    {
        public string CurrencyCode { get; set; }
        public decimal Amount { get; set; }

        public int Id { get; set; }
        public decimal Balance { get;  set; }
        public decimal HoldBalance { get;  set; }
        public decimal AvailableBalance { get;  set; }
        public string Address { get;  set; }
    }
}
