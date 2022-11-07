using System.Threading.Tasks;

namespace BackEndClassLibrary
{
    public interface IBtcConfirmTransactionInquiry
    {
        Task<int> Execute(string transactionId);
    }
}
