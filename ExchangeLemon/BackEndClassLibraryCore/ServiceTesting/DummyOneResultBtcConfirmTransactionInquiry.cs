using System.Threading.Tasks;

namespace BackEndClassLibrary
{

    public class DummyResultBtcConfirmTransactionInquiry : IBtcConfirmTransactionInquiry
    {
        public int OutputConfirm { get; set; }
        public async Task<int> Execute(string transactionId)
        {
            await Task.Delay(0);
            return OutputConfirm;
        }
    }
}
