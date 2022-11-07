using System;
using System.Linq;
// using System.Net.Http;
using System.Threading.Tasks;
// using System.Web.Http;
using BittrexBalancesSchema;
using BittrexOrderInquirySchema;
using Microsoft.AspNetCore.Mvc;

namespace BlueLight.Main
{
    public partial class BittrexLogicAccountController : Controller
    {
        private ApplicationContext _context;
        private readonly RepoGeneric repoGeneric;
        LogHelperMvc LogHelperMvc;

        public BittrexLogicAccountController(ApplicationContext context,
        RepoGeneric repoGeneric)
        {
            this._context = context;
            this.repoGeneric = repoGeneric;
            this.LogHelperMvc = new LogHelperMvc(_context);
        }
        //DBContext _context = new DBContext();

        [HttpGet]
        //x http://localhost:5000/api/v1/account/getbalances
        [Route("api/v1/account/getbalances")]
        public async Task<Balances> GetBalance()
        {

            var output = new Balances();

            try
            {
                if (BittrexHelper.UseOldSystem)
                {
                    var userName = BittrexHelper.GetUserName(Request);
                    output = await OldLogicGetBalance(userName);
                }
                else
                {
                    var userName = BittrexHelper.GetUserName(Request);

                    //var repo = new RepoGeneric();
                    //var userProfile = _context.UserProfiles.First(x => x.username == userName);

                    // var repo = new RepoGeneric (_context);
                    var output1 = await repoGeneric.GetCurrentWallet(userName);

                    //var output1 = await BlueLight.Main.ReportGenerator.GetCurrentWallet(userName, this._context);

                    var result2 = output1.Select(x => new BittrexBalancesSchema.Result()
                    {
                        Available = x.AvailableBalance,
                        Balance = x.Balance,
                        CryptoAddress = x.Address,
                        Currency = ConvertCurrency(x.CurrencyCode),
                        Pending = x.HoldBalance
                    }).ToList();

                    output = new Balances()
                    {
                        Result = result2,
                        Success = true,
                        Message = ""
                    };
                    await LogHelperMvc.SaveLog(output, Request);
                }

            }
            catch (Exception ex)
            {
                await LogHelperMvc.SaveError(output, Request, ex);
            }
            return output;

        }

        private string ConvertCurrency(string currencyCode)
        {
            if (currencyCode == "usd")
            {
                currencyCode = "usdt";
            }
            var output = currencyCode.ToUpper();
            return output;
        }

        [HttpGet]

        //http://localhost:52494/api/v1/account/getorder?apikey=a&nonce=1522954831&uuid=54a55ce7-562a-4af6-ac0c-b06559bc3dc8
        //54a55ce7-562a-4af6-ac0c-b06559bc3dc8
        //x http://localhost:5000/api/v1/account/getorder?apikey=a&nonce=1522954831&uuid=252a0faa-06a7-4bf9-81ad-c3617fb808be
        [Route("api/v1/account/getorder")]
        public async Task<OrderInquiry> GetOrder(string apikey, long nonce, string uuid)
        {

            var output = new OrderInquiry();

            try
            {

                if (BittrexHelper.UseOldSystem)
                {
                    output = await OldLogicGetOrder(uuid);
                }
                else
                {
                    var userName = BittrexHelper.GetUserName(Request);
                    //var order = context.Orders.First(x => x.Id == int.Parse(uuid));
                    var order = _context.Orders.First(x => x.GuidId == uuid);
                    var result2 = new BittrexOrderInquirySchema.Result()
                    {
                        //OrderUuid = order.Id.ToString(),
                        OrderUuid = order.GuidId,
                        Exchange = order.CurrencyPair,
                        Type = BittrexHelper.LimitDisplay(order.IsBuy),
                        Quantity = order.Amount,
                        QuantityRemaining = order.LeftAmount,
                        Limit = order.RequestRate,
                        Reserved = order.Amount,
                        ReserveRemaining = order.LeftAmount
                    };

                    output = new OrderInquiry()
                    {
                        Result = result2,
                        Success = true,
                        Message = ""
                    };
                    await LogHelperMvc.SaveLog(output, Request);

                }
            }
            catch (Exception ex)
            {
                await LogHelperMvc.SaveError(output, Request, ex);
            }

            return output;
        }

    }

    public partial class BittrexLogicAccountController
    {

        async Task<Balances> OldLogicGetBalance(string userName)
        {
            var output = new Balances();
            var option = BittrexHelper.GenerateCredential();

            using (var client = new BittrexClient(option))
            {

                var result3 = await client.GetBalancesAsync();

                var details = result3.Data.ToList();
                {
                    string currencyCode = "BTC";
                    var itemDetail = details.FirstOrDefault(x => x.Currency == currencyCode);
                    if (itemDetail != null)
                    {
                        var combineKey = TempRepo.GenerateKey(userName, currencyCode);
                        var fakeAccount = TempRepo.GetFakeBalance(combineKey);
                        itemDetail.Balance = itemDetail.Balance + fakeAccount;
                        itemDetail.Available = itemDetail.Available + fakeAccount;

                        var amount = 2;
                        itemDetail.Balance = itemDetail.Balance + amount;
                        itemDetail.Available = itemDetail.Available + amount;
                    }
                }

                {
                    string currencyCode = "USDT";
                    var itemDetail = details.FirstOrDefault(x => x.Currency == currencyCode);
                    if (itemDetail == null)
                    {
                        itemDetail = new BittrexBalance();
                        var combineKey = TempRepo.GenerateKey(userName, currencyCode);
                        var fakeAccount = TempRepo.GetFakeBalance(combineKey);

                        itemDetail.Balance = itemDetail.Balance ?? 0;
                        itemDetail.Available = itemDetail.Available ?? 0;
                        itemDetail.Balance = itemDetail.Balance + fakeAccount;
                        itemDetail.Available = itemDetail.Available + fakeAccount;

                        var amount = 8000;
                        itemDetail.Balance = itemDetail.Balance + amount;
                        itemDetail.Available = itemDetail.Available + amount;

                        itemDetail.Currency = currencyCode;

                        itemDetail.Pending = 0;
                        itemDetail.CryptoAddress = "";
                        details.Add(itemDetail);
                    }
                }

                output = new Balances()
                {
                    Message = "",
                    Success = true,
                    Result = details.Select(x => new BittrexBalancesSchema.Result()
                    {
                        Available = (double)x.Available,
                        Balance = (double)x.Balance,
                        CryptoAddress = x.CryptoAddress,
                        Currency = x.Currency,
                        Pending = (double)x.Pending
                    }).ToList()
                };

                await LogHelperMvc.SaveLog(output, Request);

            }

            return output;
        }

        async Task<OrderInquiry> OldLogicGetOrder(string uuid)
        {
            var output = new OrderInquiry();
            var option = BittrexHelper.GenerateCredential();

            using (var client = new BittrexClient(option))
            {

                var result3 = await client.GetOrderAsync(Guid.Parse(uuid));
                var output2 = result3.RawContent;
                output = OrderInquiry.FromJson(output2);
                await LogHelperMvc.SaveLog(output, Request);

            }
            return output;
        }

        //                  {
        //                      "success": true,
        //"message": "",
        //"result": {
        //                          "AccountId": null,
        //  "OrderUuid": "54a55ce7-562a-4af6-ac0c-b06559bc3dc8",
        //  "Exchange": "BTC-ADA",
        //  "Type": "LIMIT_SELL",
        //  "Quantity": 2000,
        //  "QuantityRemaining": 2000,
        //  "Limit": 0.00003853,
        //  "Reserved": 2000,
        //  "ReserveRemaining": 2000,
        //  "CommissionReserved": 0,
        //  "CommissionReserveRemaining": 0,
        //  "CommissionPaid": 0,
        //  "Price": 0,
        //  "PricePerUnit": null,
        //  "Opened": "2018-04-16T07:05:18.023",
        //  "Closed": null,
        //  "IsOpen": true,
        //  "Sentinel": "4bff0949-5efd-4600-8831-cdcca9ffa8a0",
        //  "CancelInitiated": false,
        //  "ImmediateOrCancel": false,
        //  "IsConditional": false,
        //  "Condition": "NONE",
        //  "ConditionTarget": null
        //}
        //   
    }

}