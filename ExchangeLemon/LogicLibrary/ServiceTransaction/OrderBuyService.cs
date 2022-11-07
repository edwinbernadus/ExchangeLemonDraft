using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace BlueLight.Main
{

    public class OrderBuyService : IOrderService
    {


        AccountTransactionService accountTransactionService;
        IContextSaveService contextSaveService;

        private IOrderMatchService orderMatchService;
        ILogMatchService logMatchService;


        public OrderBuyService(
            AccountTransactionService orderPopulateAccountService,
            IContextSaveService contextSaveService,
            IOrderMatchService orderMatchService,
            ILogMatchService logMatchService

        )
        {
            this.accountTransactionService = orderPopulateAccountService;
            this.contextSaveService = contextSaveService;
            this.orderMatchService = orderMatchService;
            this.logMatchService = logMatchService;


        }

        StopWatchHelper stopWatchHelper;
        bool isContinue = true;

        UserProfile buyer
        {
            get
            {
                var output = inputOrder.UserProfile;
                return output;
            }
        }

        bool IsSkipBalanceNegativeValidation { get; set; }

        OrderResult output = new OrderResult();
        private Order inputOrder;

        public async Task Init(Order inputOrder, string currencyPair)
        {
            stopWatchHelper = new StopWatchHelper("Buy");

            this.inputOrder = inputOrder;
            output.InputOrder = inputOrder;


            inputOrder.CurrencyPair = currencyPair;
            inputOrder.IsBuy = true;
            await logMatchService.SettingAsync("buy");
        }

        
        public async Task<OrderResult> Buy(Order inputOrder,
            string currencyPair, bool skipBalanceNegativeValidation)
        {
            this.IsSkipBalanceNegativeValidation = skipBalanceNegativeValidation;
            await Init(inputOrder, currencyPair);
            stopWatchHelper.Start();
            ValidationBalance();
            while (isContinue)
            {
                if (inputOrder.IsFillComplete)
                {
                    isContinue = false;
                    break;
                }

                var oppositeOrder = await orderMatchService.BuyMatchMaker(inputOrder);
                if (oppositeOrder != null)
                {
                    await DealOrder(oppositeOrder);
                }
                else
                {

                    await CreateNew();
                }
            }

            stopWatchHelper.End();
            return output;
        }

        private void ValidationBalance()
        {
            
            
            if (IsSkipBalanceNegativeValidation == false)
            {
                
                var userProfile = inputOrder.UserProfile;
                var currencyCode = this.inputOrder.SecondPair;
                var detail = userProfile.GetUserProfileDetail(currencyCode);
                

                var requestRateLeftAmount = AmountCalculator.Calc(inputOrder.Amount, inputOrder.RequestRate);
                var amount = requestRateLeftAmount;
                var availableBalance = UserProfileLogic.GetAvailableBalance(detail);

                if (availableBalance < amount)
                {


                    var inputUserName = userProfile.username;

                    if (TransactionHelper.usernames.ToList().Contains(inputUserName) == false)
                    {
                        //TODO: 6 balance_validation
                        throw new ArgumentException("Balance not enough");
                    }
                }

            }

        }

        public async Task DealOrder(Order oppositeOrder)
        {


            var seller = oppositeOrder.UserProfile;
            const string Module = "OrderBuy";
            DevHelper.Log($"Opposite Order Id: {oppositeOrder.Id}", Module);
            var userProfileId = oppositeOrder.UserProfile?.id ?? -1;
            DevHelper.Log($"Opposite Order User Profile Id: {userProfileId}", Module);


            var dealAmount = Math.Min(oppositeOrder.LeftAmount, inputOrder.LeftAmount);

            if (dealAmount  <= 0)
            {
                throw new ArgumentException("[input zero] buy deal order");
            }

            var transaction = new Transaction()
            {
                CurrencyPair = inputOrder.CurrencyPair,
                Amount = dealAmount,
                TransactionRate = oppositeOrder.RequestRate,
                IsBuyTaker = true,
            };

            var buyOrder = inputOrder;
            var sellOrder = oppositeOrder;



            transaction.UpdateAmount(buyOrder: buyOrder, sellOrder: sellOrder);

            var z1 = accountTransactionService.Execute(inputOrder,
            transaction, buyer, seller);





            var currencyCode = inputOrder.FirstPair;
            var sellerDetail = seller.GetUserProfileDetail(currencyCode);
            var logic = new UserProfileDetailLogic();
            var hold = logic.RemoveHold(sellerDetail,dealAmount, inputOrder, currencyCode);


            var opposite = oppositeOrder.UserProfile;
            output.OppositePlayers.Add(opposite);
            output.TransactionLastRate = transaction.TransactionRate;
            output.TransactionLastRateTransactionDate = transaction.TransactionDate;


            var buyerDetail = seller.GetUserProfileDetail(inputOrder.SecondPair);
            var userProfile = buyerDetail.UserProfile;
            var amount = dealAmount;
            var availableBalance = UserProfileLogic.GetAvailableBalance(buyerDetail);
       
            await contextSaveService.ExecuteAsync();

       
            accountTransactionService.InsertIntoCollection(z1);
            sellerDetail.HoldTransactions = sellerDetail.HoldTransactions ?? new List<HoldTransaction>();
            sellerDetail.HoldTransactions.Add(hold);
            await contextSaveService.ExecuteAsync();




            var orderHistoryOpposite = oppositeOrder.GenerateOrderHistory(transaction);
            output.OrderHistories.Add(orderHistoryOpposite);

            var orderHistoryInput = inputOrder.GenerateOrderHistory(transaction);
            output.OrderHistories.Add(orderHistoryInput);


            await contextSaveService.ExecuteAsync();


        }

        public async Task CreateNew()
        {
            stopWatchHelper.Save("not-match1");
            output.CreatedOrder = inputOrder;
            isContinue = false;
            
            if (inputOrder.LeftAmount <= 0)
            {
                throw new ArgumentException("[input zero] buy new");
            }

            var requestRateLeftAmount = AmountCalculator.Calc(inputOrder.LeftAmount, inputOrder.RequestRate);

            var currencyCode = inputOrder.SecondPair;
            TransactionHelper.AddHold(buyer, requestRateLeftAmount, 
                inputOrder, currencyCode);

            
            stopWatchHelper.Save("not-match2");

            await contextSaveService.ExecuteAsync();
            var orderHistory = inputOrder.GenerateOrderHistory();
            // var orderHistory = GenerateOrderHistory(inputOrder);
            output.OrderHistories.Add(orderHistory);
            await contextSaveService.ExecuteAsync();
        }
    }
}
