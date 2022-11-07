using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueLight.Main
{

    public partial class OrderSellService : IOrderService
    {


        AccountTransactionService accountTransactionService;
        IContextSaveService contextSaveService;
        IOrderMatchService orderMatchService;
        ILogMatchService logMatchService;


        public OrderSellService(
            AccountTransactionService accountTransactionService,
            IContextSaveService contextSaveService,
            IOrderMatchService orderMatchService,
            ILogMatchService logMatchService

            )
        {

            this.accountTransactionService = accountTransactionService;
            this.contextSaveService = contextSaveService;
            this.orderMatchService = orderMatchService;
            this.logMatchService = logMatchService;
            // this.unitOfWork = dont_use_unitOfWork;
        }

        StopWatchHelper stopWatchHelper;
        bool isContinue = true;

        OrderResult output = new OrderResult();
        private Order inputOrder;


        UserProfile seller
        {
            get
            {
                var output = inputOrder.UserProfile;
                return output;
            }
        }

        bool IsSkipBalanceNegativeValidation { get; set; }

        public async Task Init(Order inputOrder, string currencyPair)

        {
            stopWatchHelper = new StopWatchHelper("Sell");

            this.inputOrder = inputOrder;
            output.InputOrder = inputOrder;
            inputOrder.CurrencyPair = currencyPair;
            inputOrder.IsBuy = false;
            await logMatchService.SettingAsync("sell");

        }
        public async Task<OrderResult> Sell(Order inputOrder,
        string currencyPair,
        bool skipBalanceNegativeValidation)
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

                var oppositeOrder = await orderMatchService.SellMatchMaker(inputOrder);

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
                var currencyCode = this.inputOrder.FirstPair;
                var detail = userProfile.GetUserProfileDetail(currencyCode);


                var requestRateLeftAmount = inputOrder.Amount;
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
          
            var buyer = oppositeOrder.UserProfile;

            const string Module = "OrderSell";
            DevHelper.Log($"Opposite Order Id: {oppositeOrder.Id}", Module);
            var userProfileId = oppositeOrder.UserProfile?.id ?? -1;
            DevHelper.Log($"Opposite Order User Profile Id: {userProfileId}", Module);



            var dealAmount = Math.Min(oppositeOrder.LeftAmount, inputOrder.LeftAmount);


            if (dealAmount <= 0)
            {
                throw new ArgumentException("[input zero] sell deal order");
            }

            var transaction = new Transaction()
            {

                CurrencyPair = inputOrder.CurrencyPair,
                Amount = dealAmount,
                TransactionRate = oppositeOrder.RequestRate,
                IsBuyTaker = false,
            };

            
            transaction.UpdateAmount(buyOrder: oppositeOrder, sellOrder: inputOrder);

            var z1 = accountTransactionService.Execute(inputOrder,
                        transaction, buyer, seller);

            var transactionRateDealAmount = dealAmount * transaction.TransactionRate;
            

            var orderRateDealAmount = AmountCalculator.Calc(dealAmount, oppositeOrder.RequestRate);
       
            var currencyCode = inputOrder.SecondPair;
            var buyerDetail = buyer.GetUserProfileDetail(currencyCode);
            var logic = new UserProfileDetailLogic();
            var hold = logic.RemoveHold(buyerDetail,orderRateDealAmount, inputOrder, currencyCode);


            var opposite = oppositeOrder.UserProfile;
            output.OppositePlayers.Add(opposite);

            output.TransactionLastRate = transaction.TransactionRate;
            output.TransactionLastRateTransactionDate = transaction.TransactionDate;



            var sellerDetail = seller.GetUserProfileDetail(inputOrder.SecondPair);
            var userProfile = sellerDetail.UserProfile;
            var amount = dealAmount;
            var availableBalance = UserProfileLogic.GetAvailableBalance(sellerDetail);

            await contextSaveService.ExecuteAsync();



            //var detail = seller.UserProfileDetails.First(x => x.CurrencyCode == inputOrder.FirstPair);
            buyerDetail.HoldTransactions = buyerDetail.HoldTransactions ?? new List<HoldTransaction>();
            buyerDetail.HoldTransactions.Add(hold);
            accountTransactionService.InsertIntoCollection(z1);

            await contextSaveService.ExecuteAsync();

          
            var orderHistoryOpposite = oppositeOrder.GenerateOrderHistory(transaction);
            output.OrderHistories.Add(orderHistoryOpposite);

            var orderHistoryInput = inputOrder.GenerateOrderHistory(transaction);
            output.OrderHistories.Add(orderHistoryInput);


            await contextSaveService.ExecuteAsync();


        }

        public async Task CreateNew()
        {
            output.CreatedOrder = inputOrder;


            if (inputOrder.LeftAmount <= 0)
            {
                throw new ArgumentException("[input zero] sell new");
            }

            isContinue = false;

          
            var currencyCode = inputOrder.FirstPair;
            
            
            TransactionHelper.AddHold(seller, inputOrder.LeftAmount, 
                inputOrder, currencyCode);


            await contextSaveService.ExecuteAsync();
            
            var orderHistory = inputOrder.GenerateOrderHistory();
            output.OrderHistories.Add(orderHistory);
            await contextSaveService.ExecuteAsync();
        }
    }
}
