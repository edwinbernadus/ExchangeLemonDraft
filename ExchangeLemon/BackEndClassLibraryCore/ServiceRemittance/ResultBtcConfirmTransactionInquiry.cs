using BlueLight.Main;
using Info.Blockchain.API.BlockExplorer;
using System.Threading.Tasks;

namespace BackEndClassLibrary
{
    public class ResultBtcConfirmTransactionInquiry : IBtcConfirmTransactionInquiry
    {

        public async Task<int> Execute(string transactionId)
        {
            await Task.Delay(0);

            //var b = new BlockExplorer();

            //var result = await b.GetTransactionByHashAsync(transactionId);
            //var output = -1;
            //return output;


            //var p = BtcCloudService.Generate();
            //var result = await p.InquiryTransaction(transactionId);
            //int output = result.confirmations;
            //return output;

            return 0;
        }
    }
}
