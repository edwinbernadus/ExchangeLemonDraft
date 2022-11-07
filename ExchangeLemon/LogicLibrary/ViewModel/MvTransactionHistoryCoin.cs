//using System.Web.Mvc;
//using Microsoft.AspNetCore.Cors;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

using System;

namespace BlueLight.Main
{
    public class MvTransactionHistoryCoin
    {
        public int Id { get; set; }
        public string Currency { get; set; }
        public double Rate { get; set; }
        public double Amount { get; set; }

        public double RunningBalance { get; set; }

        public DateTime TransactionDate { get; set; }
    }
}
