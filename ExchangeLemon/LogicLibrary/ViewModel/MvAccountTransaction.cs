using System;
using System.Linq;
using Newtonsoft.Json;

namespace BlueLight.Main
{
    public class MvAccountTransaction
    {

        public MvAccountTransaction()
        {

        }
        public MvAccountTransaction(AccountTransaction accountTransaction)
        {
            var x = accountTransaction;
            Amount = x.Amount;
            CreatedBy = x.UserProfileDetail.UserProfile.username;
            Id = x.Id;
            TransactionRate = x.Transaction.TransactionRate;
        }


        //<td>{{key.TransactionRate}}</td>
        // <td>{{key.Amount}}</td>
        // <td>{{key.CreatedBy}}</td>
        // <td>{{key.Id}}</td>

        public int Id { get; set; }
        public decimal Amount { get; set; }
        public decimal TransactionRate { get; set; }
        public string CreatedBy { get; set; }

        //public string CurrencyPair { get; set; }


    }

}