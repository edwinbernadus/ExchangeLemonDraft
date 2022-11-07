using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BlueLight.Main
{

    public class OrderTransactionService
    {

        private IContextSaveService contextSaveService;

        private AccountTransactionService accountTransactionService;
        private IOrderMatchService orderMatchService;
        private ILogMatchService logMatchService;

        //public bool IsSkipBalanceNegativeValidation { get; internal set; } = false;
        public OrderBuyService OrderBuyService { get; }
        public OrderSellService OrderSellService { get; }

        public OrderTransactionService(
            IContextSaveService contextSaveService,
            IOrderMatchService orderMatchService,
            ILogMatchService logMatchService,
            AccountTransactionService accountTransactionService,
            OrderBuyService orderBuyService,
            OrderSellService OrderSellService) 
        {
            this.contextSaveService = contextSaveService;
            this.orderMatchService = orderMatchService;
            this.logMatchService = logMatchService;
            this.accountTransactionService = accountTransactionService;
            this.OrderBuyService = orderBuyService;
            this.OrderSellService = OrderSellService;
        }


      
        public async Task<OrderResult> Execute(Order order,
        string currencyPair, bool skipBalanceNegativeValidation)
        {

            order.Transactions = new List<Transaction>();
            if (order.IsBuy)
            {
                var output = await OrderBuyService.Buy(order, currencyPair,skipBalanceNegativeValidation);
                return output;

            }
            else
            {
                var output = await OrderSellService.Sell(order, currencyPair, skipBalanceNegativeValidation);
                return output;

            }
        }

      
    }
}

