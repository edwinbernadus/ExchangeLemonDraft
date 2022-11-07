using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json;

namespace BlueLight.Main
{

    public static class OrderFactory
    {

        public static Order Generate(OrderInput input)
        {
            var order = new Order()
            {

                Id = input.Id,
                Amount = input.Amount,
                LeftAmount = input.Amount,
                IsBuy = input.IsBuy,
                RequestRate = input.RequestRate,
                // Transactions = new List<Transaction>(),
                // OrderTransactions = new List<OrderTransaction>(),
                UserProfile = input.UserProfile,
                Transactions = new List<Transaction>(),
                CreatedDate = DateTime.Now,
                CurrencyPair = input.CurrencyPair,

                IsFillComplete = false,
                IsOpenOrder = true,
                IsCancelled = false,
                GuidId = input.GuidInput
            };
            //order.DecreaseLeftAmount(input.Amount);

            //order.SetAmount(input.Amount);

            return order;
        }

        public static Order Generate(InputTransaction input,
            UserProfile user, bool isBuy)
        {

            var inputRate = decimal.Parse(input.Rate);
            var inputAmount  = decimal.Parse(input.Amount);
            var orderInput = new OrderInput()
            {
                RequestRate = inputRate,
                Amount = inputAmount,
                IsBuy = isBuy,
                UserProfile = user,
                CurrencyPair = input.CurrencyPair,
                GuidInput = input.GuidInput
            };
            var roundAmount  = AmountCalculator.CalcRound(orderInput.Amount,precision:4);
            var roundRequestRate = AmountCalculator.CalcRound(orderInput.RequestRate);

            if (roundAmount != orderInput.Amount)
            {
                Debug.WriteLine("Not same round amount");
            }

            if (roundRequestRate!= orderInput.RequestRate)
            {
                Debug.WriteLine("Not same request rate");
            }

            orderInput.Amount = roundAmount;
            orderInput.RequestRate = roundRequestRate;

            var output = Generate(orderInput);
            // output.contextSaveService = service;
            // output.transactionLogicService = new TransactionLogicService();
            return output;
        }

   

       
    }
}

