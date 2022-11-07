
using FluentCache;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueLight.Main
{
    public class OrderListInquiryCacheService : IOrderListInquiryContextService
    {
        private readonly OrderListInquiryQueryService _orderListInquiryService;

        static ICache myCache = new FluentCache.Simple.FluentDictionaryCache();
        public Cache<OrderListInquiryContextService> myRepositoryCache { get; private set; }

        private readonly OrderListInquiryContextService _orderListInquiryContextService;

        public OrderListInquiryCacheService(
                    OrderListInquiryQueryService orderListInquiryService,
                    OrderListInquiryContextService orderListInquiryContextService)
        {
            _orderListInquiryContextService = orderListInquiryContextService;
            this._orderListInquiryService = orderListInquiryService;
            myRepositoryCache = myCache.WithSource(_orderListInquiryContextService);
        }
        public async Task<List<Order>> GetItemsBuy(
        int take, string currentPair)
        {
            var output = await myRepositoryCache.Method(r => r.GetItemsBuy(take, currentPair))
                                            .ExpireAfter(TimeSpan.FromMinutes(30))
                                            .GetValueAsync();

            //var output = await this._orderListInquiryContextService
            //.GetItemsBuy(take, currentPair);
            return output;
        }

        public async Task<List<Order>> GetItemsSell(
        int take, string currentPair)
        {
            var output = await myRepositoryCache.Method(r => r.GetItemsSell(take, currentPair))
                                            .ExpireAfter(TimeSpan.FromMinutes(30))
                                            .GetValueAsync();
            //var output = await this._orderListInquiryContextService
            //.GetItemsSell(take, currentPair);
            return output;
        }

        public async Task<List<Order>> GetItemsKirin(
        string currencyPair)
        {
            var output = await myRepositoryCache.Method(r => r.GetItemsKirin(currencyPair))
                                            .ExpireAfter(TimeSpan.FromMinutes(30))
                                            .GetValueAsync();
            //var output = await this._orderListInquiryContextService
            //.GetItemsKirin(currencyPair);
            return output;
        }

        public async Task ClearCache(string currencyPair)
        {
            await Task.Delay(0);

            var take = RepoOrderList.take;
            myRepositoryCache.Method(r => r.GetItemsKirin(currencyPair)).ClearValue();
            myRepositoryCache.Method(r => r.GetItemsSell(take, currencyPair)).ClearValue();
            myRepositoryCache.Method(r => r.GetItemsBuy(take, currencyPair)).ClearValue();

        }
    }

}