using NBitcoin;
using QBitNinja.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueLight.Main
{
    public class DummyTwoTransactionBitcoinGetBalance : IBitcoinGetBalance
    {
        public async Task<BtcTransaction> Execute(string addr)
        {
            
            await Task.Delay(0);
        


            var items = new string[] { "b42ab2bef60c9ea1fbcad2d2f0f574f2ddbfa9fccfda5d24b9c3c3bd383c1814",
                "b83a26b41cdecdc4f95806cc2dc7e0e30b960495f30ed68994ca4a3991316ec4" };
            var output = new BtcTransaction()
            {
                Details = items.ToList(),
                Balance = 3
                
            };
            return output;
        }

        public BtcTransaction  Generate(string addr)
        {

            var items = new string[] { "b42ab2bef60c9ea1fbcad2d2f0f574f2ddbfa9fccfda5d24b9c3c3bd383c1814",
                "b83a26b41cdecdc4f95806cc2dc7e0e30b960495f30ed68994ca4a3991316ec4" };
            var output = new BtcTransaction()
            {
                Details = items.ToList(),
                Balance = 3

            };
            return output;
        }

    }
}