using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;

namespace BlueLight.Main
{
    public partial class Order
    {
        public bool Cancel()
        {

            if (IsCancelled == false && IsFillComplete == false)
            {
                if (IsBuy)
                {
                    this.CancelBuy();
                }
                else
                {

                    this.CancelSell();
                }

                var isHoldNegative = this.UserProfile.IsHoldBalanceNegative();
                return isHoldNegative;
            }
            else
            {
                if (IsCancelled)
                {
                    throw new ArgumentException("already cancelled");
                }
                else if (IsFillComplete)
                {
                    throw new ArgumentException("transaction complete");
                }
                else
                {
                    throw new ArgumentException("fatal error - cancel method");
                }

            }
        }

        void CancelBuy()
        {
            decimal requestRateLeftAmount = AmountCalculator.Calc(this.LeftAmount, this.RequestRate);
            
            //var secondPair = "idr";
            // var secondPair = AccountTransaction.GetSecondPair (this.CurrencyPair);
            //this.UserProfile.RemoveHold(requestRateLeftAmount, this, this.SecondPair);
            TransactionHelper.RemoveHold(this.UserProfile, requestRateLeftAmount, this, this.SecondPair);

            this.SetCancelled(true);
            this.CancelDate = DateTime.Now;
        }

        void CancelSell()
        {


            //this.UserProfile.RemoveHold(LeftAmount, this, this.FirstPair);
            TransactionHelper.RemoveHold(this.UserProfile, LeftAmount, this, this.FirstPair);

            this.SetCancelled(true);
            this.CancelDate = DateTime.Now;
        }

        public void SetCancelled(bool input)
        {
            IsCancelled = input;
            SetOpenOrderCondition();
        }

    }
}