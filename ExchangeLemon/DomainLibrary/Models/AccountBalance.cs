using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlueLight.Main
{
    public class AccountBalance
    {
        public int Id { get; set; }
        public Guid GuidCode { get; set; }
        public double Balance { get; set; }
        public virtual ICollection<AccountHistory> AccountHistories { get; set; }
        // public int Version { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }

        [ConcurrencyCheck]
        public long Version { get; set; }

        public AccountHistory AddTransaction(double amount)
        {
            var newBalance = this.Balance + amount;
            var newVersion = this.Version + 1;


            // this.AccountHistories = this.AccountHistories ?? new List<AccountHistory>();
            // this.AccountHistories.Add(accountTransaction);
            this.Balance = newBalance;
            this.Version = newVersion;

            var accountTransaction = new AccountHistory()
            {
                Amount = amount,
                RunningBalance = newBalance,
                Version = newVersion
            };
            return accountTransaction;
        }
    }
}