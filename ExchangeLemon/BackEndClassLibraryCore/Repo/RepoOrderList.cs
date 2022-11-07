using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueLight.Main;
using Microsoft.EntityFrameworkCore;

public class RepoOrderList
{
    private readonly IOrderListInquiryContextService orderListInquiryContextService;

    public RepoOrderList(
          IOrderListInquiryContextService orderListInquiryContextService)
    {
        this.orderListInquiryContextService = orderListInquiryContextService;
    }



    public static int take = 10;
    public async Task<MvDetailSpotMarketItemContent> GetOrderList(string id)
    {

        var isAddFakeNumberMode = false;
        var currentPair = id;
        var result = new List<MvDetailSpotMarketItem>();
        {

            var col1 = await orderListInquiryContextService.GetItemsBuy(take, currentPair);

            var outputBuy = col1
                .Select(x => new MvDetailSpotMarketItem()
                {
                    Amount = x.Amount,
                    LeftAmount = x.LeftAmount,
                    OrderId = x.Id,
                    Rate = x.RequestRate,
                    IsBuy = true,
                    IsShow = true,
                    UserName = x.UserProfile.username
                })

                .ToList();

            if (isAddFakeNumberMode)
            {
                while (outputBuy.Count() < take)
                {
                    var dummy = new MvDetailSpotMarketItem()
                    {
                        Rate = 0.01m,
                        // Rate = 1000,
                        IsBuy = true,
                        Amount = 0.01m,
                        LeftAmount = 0.01m,
                    };

                    outputBuy.Add(dummy);
                }
            }


            result.AddRange(outputBuy);
        }

        {

          
            var col1 = await orderListInquiryContextService.GetItemsSell(take, currentPair);

            var outputSell = col1
                .Select(x => new MvDetailSpotMarketItem()
                {
                    Amount = x.Amount,
                    LeftAmount = x.LeftAmount,
                    OrderId = x.Id,
                    Rate = x.RequestRate,
                    UserName = x.UserProfile.username
                }).ToList();
            if (isAddFakeNumberMode)
            {
                while (outputSell.Count < take)
                {
                    var dummy = new MvDetailSpotMarketItem()
                    {
                        Rate = 99999,
                        // Rate = 2000,
                        Amount = 0.01m,
                        LeftAmount = 0.01m,
                    };

                    outputSell.Add(dummy);
                }
            }


            result.AddRange(outputSell);
        }

        var output = result.OrderByDescending(x => x.Rate).ToList();


     
        long lastSequenceHistory = -2;
        var output2 = new MvDetailSpotMarketItemContent()
        {
            Items = output,
            LastSequenceHistory = lastSequenceHistory
        };
        return output2;
    }

    
}