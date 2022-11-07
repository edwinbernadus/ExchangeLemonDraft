using NBitcoin;
using QBitNinja.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueLight.Main
{
    public class DummyThreeTransactionBitcoinGetBalance : IBitcoinGetBalance
    {
        public DummyThreeTransactionBitcoinGetBalance(DummyTwoTransactionBitcoinGetBalance service)
        {
            Service = service;
        }
        public static int Step { get; set; } = 1;
        public DummyTwoTransactionBitcoinGetBalance Service { get; }

        public async Task<BtcTransaction> Execute(string addr)
        {
            await Task.Delay(0);
            if (Step == 1)
            {
                var output = Service.Generate(addr);
                return output;
            }
            else
            {
                var output=  GenerateStepTwo(addr);
                return output;
            }
        }

       
        private BtcTransaction GenerateStepTwo(string addr)
        {
            

            var items = new string[] { "6b2ba04cc0277e96d2e304ea082b4315ca1dada6ce3261c508e8e46098fdadeb",
                 "b42ab2bef60c9ea1fbcad2d2f0f574f2ddbfa9fccfda5d24b9c3c3bd383c1814",
                "b83a26b41cdecdc4f95806cc2dc7e0e30b960495f30ed68994ca4a3991316ec4" };

            var output = new BtcTransaction()
            {
                Details = items.ToList(),
                Balance = 6

            };

            return output;
        }

     


    }
}