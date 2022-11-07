using System;
using System.Threading.Tasks;
using BlueLight.Main;

namespace BackEndClassLibrary
{

    public class DummySendMoneyWebHookSent : IBtcServiceSendMoney
    {
        //public static string transactionId = "e79669f7-57d5-4854-a5b3-a982ac83624d";
        public static string transactionId = "04dd3b5f71b23d66e673a21c9192663371a18997137a7ec908883ca23e788dcd";
        
        public bool IsInit { get; set; }

        public async Task<string> SendMoney(string address, long amountInSatoshi)
        {
            await Task.Delay(0);
            if (IsInit == false)
            {
                IsInit = true;
                return transactionId;
            }

            string output = Guid.NewGuid().ToString();
            return output;
        }
    }
}

