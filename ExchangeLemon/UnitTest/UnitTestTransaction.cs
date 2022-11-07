using System;
using System.Collections.Generic;
using BlueLight.Main;
using Serilog;
using Xunit;
public class UnitTestTransaction
{
    //[Fact]
    //public void Test1()
    //{
    //    var c = new Class1();
    //    var z = c.GetOne();
    //    Assert.Equal(4, z);
    //}

    [Fact]
    public void AddBalance()
    {
        var user = new UserProfile();
        user.PopulateCurrency();
        user.AddBalanceTesting("btc", 100);
        user.AddBalanceTesting("idr", 0);

        var amountIdr = user.GetAvailableBalanceFromCurrency("idr");
        var amountBtc = user.GetAvailableBalanceFromCurrency("btc");

        Assert.Equal(100, amountBtc);
    }

    [Fact]
    public void AddBtc()
    {
        var user = new UserProfile();
        user.PopulateCurrency();
        user.AddBalanceTesting("btc", 100);
        user.AddBalanceTesting("idr", 0);

        var transaction = new Transaction()
        {

            TransactionDate = DateTime.Now,
            Amount = 5,
            TransactionRate = 12000,
            IsBuyTaker = true,

        };

        var buyer = user;


        var currency_pair = "btc_idr";

        var service = new AccountTransactionService();
        // var accountTransactionService = new AccountTransactionService();
        var accountTransactionService = service;

        {
            var input1 = new AccountTransactionInput()
            {
                currencyPair = currency_pair,
                transaction = transaction,
                player = buyer,
            };
            input1.SetBtcModeDebit();
            accountTransactionService.Logic(input1);

        }


        {
            var input1 = new AccountTransactionInput()
            {
                currencyPair = currency_pair,
                transaction = transaction,
                player = buyer,
            };
            input1.SetAltModeCredit();
            accountTransactionService.Logic(input1);
        }



        var amountIdr = user.GetAvailableBalanceFromCurrency("idr");
        var amountBtc = user.GetAvailableBalanceFromCurrency("btc");

        Assert.Equal(-60000, amountIdr);
        Assert.Equal(105, amountBtc);
    }

    [Fact]

    public void BuyBtc()
    {
        var factory = new FactoryTest();
        factory.IsResetIdDevMode = false;
        var market = factory.Generate();

        var buyer = new UserProfile();
        buyer.PopulateCurrency();
        buyer.AddBalanceTesting("btc", 100);
        buyer.AddBalanceTesting("idr", 50000);

        var orderInput = new OrderInput()
        {
            // Transactions = new List<Transaction>(),
            //OrderTransactions = new List<OrderTransaction>(),
            Amount = 3,
            RequestRate = 11500,
            IsBuy = true,
            UserProfile = buyer
        };
        var order = OrderFactory.Generate(orderInput);

        // sell
        // id 1 - 3 - 11500
        // id 2 - 2 - 11000
        // id 3 - 3 - 10000

        var output = order.ExecuteTest(market.Orders);
        var total = (3 * 10000);

        {
            var amountIdr = buyer.GetAvailableBalanceFromCurrency("idr");
            var amountBtc = buyer.GetAvailableBalanceFromCurrency("btc");

            var total2 = 50000 - total;
            // Assert.Equal(15500, amountIdr);
            Assert.Equal(total2, amountIdr);
            Assert.Equal(103, amountBtc);
        }

        {
            var user = factory.user;
            var availableBalanceAmountIdr = user.GetAvailableBalanceFromCurrency("idr");
            var ledgerBalanceAmountIdr = user.GetLedgerBalanceFromCurrency("idr");
            var availableBalanceAmountBtc = user.GetAvailableBalanceFromCurrency("btc");
            var ledgerBalanceAmountBtc = user.GetLedgerBalanceFromCurrency("btc");

            // Assert.Equal(11500 * 3, amountIdr);

            var total2 = total - 15000;
            Assert.Equal(total2, availableBalanceAmountIdr);
            Assert.Equal(total, ledgerBalanceAmountIdr);
            Assert.Equal(-3, ledgerBalanceAmountBtc);
        }
        var r = output.CreatedOrder != null;
        Assert.False(r);

        var totalTransaction = order.Transactions.Count;
        //var totalTransaction = order.OrderTransactions.Count;
        Assert.Equal(1, totalTransaction);

        Assert.False(factory.order1.IsFillComplete);
        Assert.True(factory.order3.IsFillComplete);
        Assert.True(order.IsFillComplete);
    }

    [Fact]

    public void SellBtc()
    {
        var factory = new FactoryTest();
        factory.IsResetIdDevMode = false;
        var market = factory.Generate();

        var userTwo = new UserProfile();
        userTwo.PopulateCurrency();
        userTwo.AddBalanceTesting("btc", 100);
        userTwo.AddBalanceTesting("idr", 50000);

        var orderInput = new OrderInput()
        {
            // Transactions = new List<Transaction>(),
            //OrderTransactions = new List<OrderTransaction>(),
            Amount = 3,
            RequestRate = 3000,
            IsBuy = false,
            UserProfile = userTwo
        };
        var order = OrderFactory.Generate(orderInput);

        Log.Information("step - 1");
        LogHelperBusiness.Log(market.Orders);

        var output = order.ExecuteTest(market.Orders);

        Log.Information("step - 2");
        LogHelperBusiness.Log(market.Orders);
        {
            var amountIdr = userTwo.GetAvailableBalanceFromCurrency("idr");
            var amountBtc = userTwo.GetAvailableBalanceFromCurrency("btc");

            Assert.Equal(50000 + 4000 + 4000 + 3000, amountIdr);
            Assert.Equal(100 - 3, amountBtc);
        }

        {
            var user = factory.user;
            var amountIdr = user.GetLedgerBalanceFromCurrency("idr");
            var amountBtc = user.GetLedgerBalanceFromCurrency("btc");

            Assert.Equal(-1 * (4000 + 4000 + 3000), amountIdr);
            Assert.Equal(3, amountBtc);
        }

        var r = output.CreatedOrder != null;
        Assert.False(r);

        // var totalTransaction = order.Transactions.Count;
        //var totalTransaction = order.OrderTransactions.Count;
        var totalTransaction = order.Transactions.Count;
        Assert.Equal(2, totalTransaction);

        Assert.True(factory.order11.IsFillComplete);
        Assert.True(order.IsFillComplete);
    }
}