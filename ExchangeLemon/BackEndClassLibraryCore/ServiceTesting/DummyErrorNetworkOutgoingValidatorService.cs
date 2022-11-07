using System;
using System.Threading.Tasks;

namespace BackEndClassLibrary
{
 

    public class DummyNetworkBtcConfirmTransactionInquiry : IBtcConfirmTransactionInquiry
    {
        
        public async Task<int> Execute(string transactionId)
        {
            await Task.Delay(0);
            throw new ArgumentException("error-message");
        }
    }
}
