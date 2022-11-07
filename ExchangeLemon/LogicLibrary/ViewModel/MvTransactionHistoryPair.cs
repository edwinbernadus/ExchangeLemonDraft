//using System.Web.Mvc;
//using Microsoft.AspNetCore.Cors;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

using System;

namespace BlueLight.Main
{
    public class MvTransactionHistoryPair
    {
        public int Id { get; set; }
        public string CurrencyPair { get; set; }
        public double Rate { get; set; }
        public double Amount { get; set; }

        public DateTime TransactionDate { get; set; }
    }
}
