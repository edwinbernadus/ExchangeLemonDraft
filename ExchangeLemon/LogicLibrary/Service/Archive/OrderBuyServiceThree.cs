//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Threading.Tasks;
//using Newtonsoft.Json;

//namespace BlueLight.Main
//{
//    public class OrderBuyServiceThree
//    {

//        // public interface IContextSaveTwoService
//        // {
//        //     void CreateSession();
//        //     void UpdateMain(Order inputOrder, Order oppositeOrder, UserProfile userProfile1, UserProfile userProfile2);
//        //     void UpdateTransaction();
//        // }

//        OrderPopulateService orderPopulateService;
//        AccountTransactionService accountTransactionService;
//        IContextSaveService contextSaveService;
//        // IContextSaveTwoService context;
//        private IOrderMatchService orderMatchService;

//        public OrderBuyServiceThree(OrderPopulateService orderPopulateService,
//        AccountTransactionService orderPopulateAccountService,
//        IContextSaveService contextSaveService,
//        IOrderMatchService orderMatchService)
//        {
//            this.orderPopulateService = orderPopulateService;
//            this.accountTransactionService = orderPopulateAccountService;
//            this.contextSaveService = contextSaveService;
//            this.orderMatchService = orderMatchService;
//        }
//        StopWatchHelper stopWatchHelper;
//        bool isContinue = true;

//        UserProfile buyer
//        {
//            get
//            {
//                var output = inputOrder.UserProfile;
//                return output;
//            }
//        }

//        OrderResult output = new OrderResult();
//        private Order inputOrder;

//        public async Task<OrderResult> Buy(Order inputOrder,
//        IQueryable<Order> orders, string currencyPair)
//        {
//            stopWatchHelper = new StopWatchHelper("Buy");
//            stopWatchHelper.Start();
//            this.inputOrder = inputOrder;

//            inputOrder.CurrencyPair = currencyPair;
//            inputOrder.IsBuy = true;

//            while (isContinue)
//            {
//                if (inputOrder.IsFillComplete)
//                {
//                    isContinue = false;
//                    break;
//                }

//                var oppositeOrder = await orderMatchService.BuyMatchMaker(inputOrder);

//                if (oppositeOrder != null)
//                {
//                    await MatchOrder(oppositeOrder);
//                }
//                else
//                {

//                    await CreateNew();
//                }
//            }
//            stopWatchHelper.End();
//            return output;
//        }


//        private void UpdateOrder(Transaction transaction, Order buyOrder, Order sellOrder)
//        {
//            var amount = transaction.Amount;
//            buyOrder.UpdateAmount(amount);
//            sellOrder.UpdateAmount(amount);
//        }

//        async Task MatchOrder(Order oppositeOrder)
//        {
//            var seller = oppositeOrder.UserProfile;
//            const string Module = "OrderBuy";
//            DevHelper.Log($"Opposite Order Id: {oppositeOrder.Id}", Module);
//            var userProfileId = oppositeOrder.UserProfile?.id ?? -1;
//            DevHelper.Log($"Opposite Order User Profile Id: {userProfileId}", Module);

//            stopWatchHelper.Save("match1");

//            var dealAmount = Math.Min(oppositeOrder.LeftAmount, inputOrder.LeftAmount);
//            var transaction = new Transaction()
//            {
//                CurrencyPair = inputOrder.CurrencyPair,
//                Amount = dealAmount,
//                TransactionRate = oppositeOrder.RequestRate,
//                IsBuyTaker = true,
//            };

//            var buyOrder = inputOrder;
//            var sellOrder = oppositeOrder;
//            orderPopulateService.Populate(transaction, buyOrder: buyOrder, sellOrder: sellOrder);

//            stopWatchHelper.Save("match2");

//            accountTransactionService.Execute(inputOrder, transaction, buyer, seller);
//            seller.RemoveHold(dealAmount, inputOrder, inputOrder.FirstPair);
//            stopWatchHelper.Save("match3");

//            var opposite = oppositeOrder.UserProfile;
//            output.OppositePlayers.Add(opposite);
//            output.LastRate = transaction.TransactionRate;
//            output.TransactionRate = transaction.TransactionRate;

//            stopWatchHelper.Save("match4");

//            // context?.CreateSession();
//            // context?.UpdateMain(inputOrder,
//            // oppositeOrder,
//            // inputOrder.UserProfile,
//            // oppositeOrder.UserProfile);
//            // context?.UpdateTransaction();

//            await contextSaveService.ExecuteAsync();
//            // var orderHistory = GenerateOrderHistory(oppositeOrder, transaction);
//            var orderHistory = oppositeOrder.GenerateOrderHistory(transaction);
//            output.OrderHistories.Add(orderHistory);


//            await contextSaveService.ExecuteAsync();
//        }

//        async Task CreateNew()
//        {
//            stopWatchHelper.Save("not-match1");
//            output.CreatedOrder = inputOrder;
//            isContinue = false;

//            // var buyer = this.UserProfile;


//            var requestRateLeftAmount = AmountCalculator.Calc(inputOrder.LeftAmount, inputOrder.RequestRate);

//            buyer.AddHold(requestRateLeftAmount, inputOrder, inputOrder.SecondPair);
//            stopWatchHelper.Save("not-match2");
//            await contextSaveService.ExecuteAsync();
//            var orderHistory = inputOrder.GenerateOrderHistory();
//            // var orderHistory = GenerateOrderHistory(inputOrder);
//            output.OrderHistories.Add(orderHistory);
//            await contextSaveService.ExecuteAsync();
//        }
//    }


//}
