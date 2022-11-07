using System.Threading.Tasks;

namespace BlueLight.Main
{
    public interface IFunctionBitcoinService
    {
        Task<BitcoinGenerateAddress> GenerateAddress();
        Task<decimal> InquiryBalance(string address);
        Task Register(string i);
        
    }
}