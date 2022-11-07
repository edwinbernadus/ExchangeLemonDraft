using System;
using System.Threading.Tasks;
using BlueLight.Main;

namespace BackEndClassLibrary
{
    public class DummyBtcServiceSendMoney : IBtcServiceSendMoney
    {
        
        public async Task<string> SendMoney(string address, long amountInSatoshi)
        {
            await Task.Delay(0);
            string output = Guid.NewGuid().ToString();
            return output;
        }
    }
}

