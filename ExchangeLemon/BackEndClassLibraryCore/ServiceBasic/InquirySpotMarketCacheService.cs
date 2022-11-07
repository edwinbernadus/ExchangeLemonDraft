// ;
using FluentCache;
using System;
using System.Threading.Tasks;

namespace BlueLight.Main
{
    public class InquirySpotMarketCacheService : IInquirySpotMarketService
    {
        static ICache myCache = new FluentCache.Simple.FluentDictionaryCache();
        public Cache<InquirySpotMarketService> myRepositoryCache { get; private set; }
        public InquirySpotMarketService Service { get; }

        public InquirySpotMarketCacheService(InquirySpotMarketService service)
        {
            Service = service;
            myRepositoryCache = myCache.WithSource(service);
        }

        

        public async Task<LastChange> GetLastChangeAsync(string currencyPair)
        {
            var output = await myRepositoryCache.Method(r => r.GetLastChangeAsync(currencyPair))
                                           .ExpireAfter(TimeSpan.FromMinutes(30))
                                           .GetValueAsync();
            return output;
        }

        public async Task<decimal> GetVolumeAsync(string currencyPair)
        {
            var output = await myRepositoryCache.Method(r => r.GetVolumeAsync(currencyPair))
                                           .ExpireAfter(TimeSpan.FromMinutes(30))
                                           .GetValueAsync();
            return output;
        }

        public void PopulateAvailableBalance(UserProfile userProfile, MvSpotMarketDetail input)
        {
            Service.PopulateAvailableBalance(userProfile, input);
        }
    }
}