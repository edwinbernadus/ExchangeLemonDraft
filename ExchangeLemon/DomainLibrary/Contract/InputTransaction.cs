using System;

namespace BlueLight.Main
{

    public class InputTransaction
    {
        public string Rate { get; set; }
        public string Amount { get; set; }
        
        public string CurrencyPair { get; set; }

        public bool IsBuy { get; set; }
        public Guid GuidInput { get; internal set; }

        public string GetModuleName()
        {
            if (IsBuy)
            {
                var ModuleName = "BuyModule";
                return ModuleName;
            }
            else
            {
                var ModuleName = "SellModule";
                return ModuleName;
            }
        }
       
    }
}
