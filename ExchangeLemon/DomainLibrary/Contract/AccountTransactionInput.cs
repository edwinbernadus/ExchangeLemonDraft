using System;
using System.Collections.Generic;
using System.Linq;

namespace BlueLight.Main
{
    public class AccountTransactionInput
    {

        public Transaction transaction { get; set; }
        public UserProfile player { get; set; }

        public string currencyPair { get; set; }
        public string currencyCode { get; private set; }
        public bool isMultiplyRate { get; private set; }

        public bool isNegativeAmount { get; private set; }

        public AccountTransaction accountTransaction { get; private set; } //= new AccountTransaction();

        void SetBtcMode()
        {
            // this.accountTransaction = new AccountTransaction();
            this.currencyCode = PairHelper.GetFirstPair(currencyPair);
            this.isMultiplyRate = false;
        }


        public void SetBtcModeDebit()
        {
            SetBtcMode();
            this.isNegativeAmount = false;
        }

        public void SetBtcModeCredit()
        {
            SetBtcMode();
            this.isNegativeAmount = true;
        }

        void SetAltMode()
        {
            // this.accountTransaction = new AccountTransaction();
            this.currencyCode = PairHelper.GetSecondPair(currencyPair);
            this.isMultiplyRate = true;
        }


        public void SetAltModeDebit()
        {
            SetAltMode();
            this.isNegativeAmount = false;
        }

        public void SetAltModeCredit()
        {
            SetAltMode();
            this.isNegativeAmount = true;
        }
    }

}