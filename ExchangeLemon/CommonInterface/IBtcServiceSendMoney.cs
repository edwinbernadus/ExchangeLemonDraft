using System.Threading.Tasks;

namespace BlueLight.Main
{
    public interface IBtcServiceSendMoney
    {
        //Task<string> SendMoney(string privateKey, string address, long amountInSatoshi, long amountFeeSatoshi);
        Task<string> SendMoney(string address, long amountInSatoshi);
    }
}