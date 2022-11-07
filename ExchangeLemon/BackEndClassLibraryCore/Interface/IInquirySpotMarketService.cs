using System.Threading.Tasks;

namespace BlueLight.Main
{
    public interface IInquirySpotMarketService
    {
        Task<LastChange> GetLastChangeAsync(string currencyPair);
        Task<decimal> GetVolumeAsync(string currencyPair);
        void PopulateAvailableBalance(UserProfile userProfile, MvSpotMarketDetail input);
    }
}