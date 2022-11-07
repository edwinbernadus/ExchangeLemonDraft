using System;

namespace BlueLight.Main
{
    public class InputTransactionRawHelper
    {
        public static InputTransaction ConvertTo(InputTransactionRaw input)
        {


            var isBuy = false;
            if (string.IsNullOrEmpty(input.mode) == false)
            {
                if (input.mode.ToLower() == "buy")
                {
                    isBuy = true;
                }
            }
            var output = new InputTransaction()
            {
                Amount = input.amount,
                Rate = input.rate,
                CurrencyPair = input.current_pair,
                IsBuy = isBuy,
                GuidInput = input.GuidInput

            };
            return output;
        }

    }
}