using System.Collections.Generic;
using System.Linq;
using BlueLight.Main;
using Xunit;
public class UnitTestHoldMixed
{

    [Fact]
    public void ScenarioOneBuySell()
    {

        // var factory = new FactoryTest();
        // factory.IsResetIdDevMode = false;
        var market = new Market()
        {
            Orders = new List<Order>()
        };

        var user = new UserProfile();
        user.PopulateCurrency();

        var orderInputBuy = new OrderInput()
        {
            // Transactions = new List<Transaction>(),
            //OrderTransactions = new List<OrderTransaction>(),
            Amount = 5,
            RequestRate = 9000,
            IsBuy = true,
            UserProfile = user
        };
        var orderBuy = OrderFactory.Generate(orderInputBuy);
        var outputBuy = orderBuy.ExecuteTest(market.Orders);
        var r = outputBuy != null;
        Assert.True(r);

        Assert.False(orderBuy.IsFillComplete);

        Assert.Equal(5, orderBuy.LeftAmount);

        Assert.Equal(-45000, user.availableBalanceIdrTesting);
        Assert.Equal(0, user.balanceIdrTesting);
        Assert.Equal(45000, user.holdBalanceIdrTesting);

        Assert.Equal(0, user.availableBalanceBtcTesting);
        Assert.Equal(0, user.balanceBtcTesting);
        Assert.Equal(0, user.holdBalanceBtcTesting);

        var orderInputSell = new OrderInput()
        {
            // Transactions = new List<Transaction>(),
            //OrderTransactions = new List<OrderTransaction>(),
            Amount = 5,
            RequestRate = 10000,
            IsBuy = false,
            UserProfile = user
        };
        var orderSell = OrderFactory.Generate(orderInputSell);
        var outputSell = orderSell.ExecuteTest(market.Orders);
        var r2 = outputSell != null;
        Assert.True(r2);

        Assert.False(orderSell.IsFillComplete);

        Assert.Equal(5, orderSell.LeftAmount);

        Assert.Equal(-45000, user.availableBalanceIdrTesting);
        Assert.Equal(0, user.balanceIdrTesting);
        Assert.Equal(45000, user.holdBalanceIdrTesting);

        Assert.Equal(-5, user.availableBalanceBtcTesting);
        Assert.Equal(0, user.balanceBtcTesting);
        Assert.Equal(5, user.holdBalanceBtcTesting);
    }

    [Fact]
    public void ScenarioTwoBuySell()
    {

        // var factory = new FactoryTest();
        // factory.IsResetIdDevMode = false;
        var market = new Market()
        {
            Orders = new List<Order>()
        };

        var user01 = new UserProfile();
        user01.username = "test01";
        user01.PopulateCurrency();

        var user02 = new UserProfile();
        user02.username = "test02";
        user02.PopulateCurrency();
        user02.AddBalanceTesting("btc", 5);

        var orderHistories = new List<OrderHistory>();

        var orderInputBuy = new OrderInput()
        {
            // Transactions = new List<Transaction>(),
            //OrderTransactions = new List<OrderTransaction>(),
            Amount = 1,
            RequestRate = 10000,
            IsBuy = true,
            UserProfile = user01,
            Id = 1
        };
        var orderBuy = OrderFactory.Generate(orderInputBuy);

        // transaction 1
        var outputBuy = orderBuy.ExecuteTest(market.Orders);
        market.Orders.Add(orderBuy);
        {
            var total = outputBuy.OrderHistories.Count();
            Assert.Equal(1, total);
            orderHistories.AddRange(outputBuy.OrderHistories);
        }

        var r = outputBuy != null;
        Assert.True(r);

        Assert.False(orderBuy.IsFillComplete);

        Assert.Equal(1, orderBuy.LeftAmount);

        Assert.Equal(-10000, user01.availableBalanceIdrTesting);
        Assert.Equal(0, user01.balanceIdrTesting);
        Assert.Equal(10000, user01.holdBalanceIdrTesting);

        Assert.Equal(0, user01.availableBalanceBtcTesting);
        Assert.Equal(0, user01.balanceBtcTesting);
        Assert.Equal(0, user01.holdBalanceBtcTesting);

        var orderInputSell = new OrderInput()
        {
            // Transactions = new List<Transaction>(),
            //OrderTransactions = new List<OrderTransaction>(),
            Amount = 2,
            RequestRate = 10000,
            IsBuy = false,
            UserProfile = user02,
            Id = 2
        };
        var orderSell = OrderFactory.Generate(orderInputSell);

        Assert.Equal(5, user02.availableBalanceBtcTesting);

        // transaction 2
        var outputSell = orderSell.ExecuteTest(market.Orders);
        market.Orders.Add(orderSell);
        {
            var total = outputSell.OrderHistories.Count();
            Assert.Equal(3, total);
            orderHistories.AddRange(outputSell.OrderHistories);
        }

        var r2 = outputSell != null;
        Assert.True(r2);

        LogHelperBusiness.Log(market.Orders);
        LogHelperBusiness.Log(market.Orders.First());
        LogHelperBusiness.Log(orderSell);

        Assert.False(orderSell.IsFillComplete);

        Assert.Equal(1, orderSell.LeftAmount);

        Assert.Equal(-10000, user01.availableBalanceIdrTesting);
        Assert.Equal(-10000, user01.balanceIdrTesting);
        Assert.Equal(0, user01.holdBalanceIdrTesting);

        Assert.Equal(1, user01.availableBalanceBtcTesting);
        Assert.Equal(1, user01.balanceBtcTesting);
        Assert.Equal(0, user01.holdBalanceBtcTesting);

        Assert.Equal(10000, user02.availableBalanceIdrTesting);
        Assert.Equal(10000, user02.balanceIdrTesting);
        Assert.Equal(0, user02.holdBalanceIdrTesting);

        Assert.Equal(3, user02.availableBalanceBtcTesting);
        Assert.Equal(4, user02.balanceBtcTesting);
        Assert.Equal(1, user02.holdBalanceBtcTesting);

        var orderInputBuy2 = new OrderInput()
        {
            // Transactions = new List<Transaction>(),
            //OrderTransactions = new List<OrderTransaction>(),
            Amount = 2,
            RequestRate = 10000,
            IsBuy = true,
            UserProfile = user01,
            Id = 3
        };
        var orderBuy2 = OrderFactory.Generate(orderInputBuy2);

        // transaction 3
        var outputBuy2 = orderBuy2.ExecuteTest(market.Orders);
        market.Orders.Add(orderBuy2);
        {
            var total = outputBuy2.OrderHistories.Count();
            Assert.Equal(3, total);
            orderHistories.AddRange(outputBuy2.OrderHistories);
        }

        LogHelperBusiness.Log(market.Orders);

        var r3 = orderBuy2 != null;
        Assert.True(r3);

        Assert.False(orderBuy2.IsFillComplete);

        Assert.Equal(-30000, user01.availableBalanceIdrTesting);
        Assert.Equal(-20000, user01.balanceIdrTesting);
        Assert.Equal(10000, user01.holdBalanceIdrTesting);

        Assert.Equal(2, user01.availableBalanceBtcTesting);
        Assert.Equal(2, user01.balanceBtcTesting);
        Assert.Equal(0, user01.holdBalanceBtcTesting);

        Assert.Equal(20000, user02.availableBalanceIdrTesting);
        Assert.Equal(20000, user02.balanceIdrTesting);
        Assert.Equal(0, user02.holdBalanceIdrTesting);

        Assert.Equal(3, user02.availableBalanceBtcTesting);
        Assert.Equal(3, user02.balanceBtcTesting);
        Assert.Equal(0, user02.holdBalanceBtcTesting);

        var totalOrderHistories = orderHistories.Count();
        Assert.Equal(7, totalOrderHistories);

    }


    [Fact]
    public void ScenarioThreeBuySell()
    {

        // var factory = new FactoryTest();
        // factory.IsResetIdDevMode = false;
        var market = new Market()
        {
            Orders = new List<Order>()
        };

        var user01 = new UserProfile();
        user01.username = "test01";
        user01.PopulateCurrency();

        var user02 = new UserProfile();
        user02.username = "test02";
        user02.PopulateCurrency();
        user02.AddBalanceTesting("btc", 5);

        var orderHistories = new List<OrderHistory>();

        var orderInputBuy = new OrderInput()
        {
            // Transactions = new List<Transaction>(),
            //OrderTransactions = new List<OrderTransaction>(),
            Amount = 1,
            RequestRate = 10000,
            IsBuy = true,
            UserProfile = user01,
            Id = 1
        };
        var orderBuy = OrderFactory.Generate(orderInputBuy);

        // transaction 1
        var outputBuy = orderBuy.ExecuteTest(market.Orders);
        market.Orders.Add(orderBuy);
        {
            var total = outputBuy.OrderHistories.Count();
            Assert.Equal(1, total);
            orderHistories.AddRange(outputBuy.OrderHistories);
        }

        var r = outputBuy != null;
        Assert.True(r);

        Assert.False(orderBuy.IsFillComplete);

        Assert.Equal(1, orderBuy.LeftAmount);

        Assert.Equal(-10000, user01.availableBalanceIdrTesting);
        Assert.Equal(0, user01.balanceIdrTesting);
        Assert.Equal(10000, user01.holdBalanceIdrTesting);

        Assert.Equal(0, user01.availableBalanceBtcTesting);
        Assert.Equal(0, user01.balanceBtcTesting);
        Assert.Equal(0, user01.holdBalanceBtcTesting);

        var orderInputSell = new OrderInput()
        {
            // Transactions = new List<Transaction>(),
            //OrderTransactions = new List<OrderTransaction>(),
            Amount = 2,
            RequestRate = 9000,
            IsBuy = false,
            UserProfile = user02,
            Id = 2
        };
        var orderSell = OrderFactory.Generate(orderInputSell);

        Assert.Equal(5, user02.availableBalanceBtcTesting);

        // transaction 2
        var outputSell = orderSell.ExecuteTest(market.Orders);
        market.Orders.Add(orderSell);
        {
            var total = outputSell.OrderHistories.Count();
            Assert.Equal(3, total);
            orderHistories.AddRange(outputSell.OrderHistories);
        }

        var r2 = outputSell != null;
        Assert.True(r2);

        LogHelperBusiness.Log(market.Orders);
        LogHelperBusiness.Log(market.Orders.First());
        LogHelperBusiness.Log(orderSell);

        Assert.False(orderSell.IsFillComplete);

        Assert.Equal(1, orderSell.LeftAmount);

        Assert.Equal(-10000, user01.availableBalanceIdrTesting);
        Assert.Equal(-10000, user01.balanceIdrTesting);
        Assert.Equal(0, user01.holdBalanceIdrTesting);

        Assert.Equal(1, user01.availableBalanceBtcTesting);
        Assert.Equal(1, user01.balanceBtcTesting);
        Assert.Equal(0, user01.holdBalanceBtcTesting);

        Assert.Equal(10000, user02.availableBalanceIdrTesting);
        Assert.Equal(10000, user02.balanceIdrTesting);
        Assert.Equal(0, user02.holdBalanceIdrTesting);

        Assert.Equal(3, user02.availableBalanceBtcTesting);
        Assert.Equal(4, user02.balanceBtcTesting);
        Assert.Equal(1, user02.holdBalanceBtcTesting);

        var orderInputBuy2 = new OrderInput()
        {
            // Transactions = new List<Transaction>(),
            //OrderTransactions = new List<OrderTransaction>(),
            Amount = 2,
            RequestRate = 10000,
            IsBuy = true,
            UserProfile = user01,
            Id = 3
        };
        var orderBuy2 = OrderFactory.Generate(orderInputBuy2);

        // transaction 3
        var outputBuy2 = orderBuy2.ExecuteTest(market.Orders);
        market.Orders.Add(orderBuy2);
        {
            var total = outputBuy2.OrderHistories.Count();
            Assert.Equal(3, total);
            orderHistories.AddRange(outputBuy2.OrderHistories);
        }

        LogHelperBusiness.Log(market.Orders);

        var r3 = orderBuy2 != null;
        Assert.True(r3);

        Assert.False(orderBuy2.IsFillComplete);

        Assert.Equal(-29000, user01.availableBalanceIdrTesting);
        Assert.Equal(-19000, user01.balanceIdrTesting);
        Assert.Equal(10000, user01.holdBalanceIdrTesting);

        Assert.Equal(2, user01.availableBalanceBtcTesting);
        Assert.Equal(2, user01.balanceBtcTesting);
        Assert.Equal(0, user01.holdBalanceBtcTesting);

        Assert.Equal(19000, user02.availableBalanceIdrTesting);
        Assert.Equal(19000, user02.balanceIdrTesting);
        Assert.Equal(0, user02.holdBalanceIdrTesting);

        Assert.Equal(3, user02.availableBalanceBtcTesting);
        Assert.Equal(3, user02.balanceBtcTesting);
        Assert.Equal(0, user02.holdBalanceBtcTesting);

        var totalOrderHistories = orderHistories.Count();
        Assert.Equal(7, totalOrderHistories);

    }


    [Fact]
    public void ScenarioFourBuySell()
    {

        // var factory = new FactoryTest();
        // factory.IsResetIdDevMode = false;
        var market = new Market()
        {
            Orders = new List<Order>()
        };

        var user01 = new UserProfile();
        user01.username = "test01";
        user01.PopulateCurrency();

        var user02 = new UserProfile();
        user02.username = "test02";
        user02.PopulateCurrency();
        user02.AddBalanceTesting("btc", 5);

        var orderHistories = new List<OrderHistory>();

        var orderInputBuy = new OrderInput()
        {
            // Transactions = new List<Transaction>(),
            //OrderTransactions = new List<OrderTransaction>(),
            Amount = 1,
            RequestRate = 10000,
            IsBuy = true,
            UserProfile = user01,
            Id = 1
        };
        var orderBuy = OrderFactory.Generate(orderInputBuy);

        // transaction 1
        var outputBuy = orderBuy.ExecuteTest(market.Orders);
        market.Orders.Add(orderBuy);
        {
            var total = outputBuy.OrderHistories.Count();
            Assert.Equal(1, total);
            orderHistories.AddRange(outputBuy.OrderHistories);
        }

        var r = outputBuy != null;
        Assert.True(r);

        Assert.False(orderBuy.IsFillComplete);

        Assert.Equal(1, orderBuy.LeftAmount);

        Assert.Equal(-10000, user01.availableBalanceIdrTesting);
        Assert.Equal(0, user01.balanceIdrTesting);
        Assert.Equal(10000, user01.holdBalanceIdrTesting);

        Assert.Equal(0, user01.availableBalanceBtcTesting);
        Assert.Equal(0, user01.balanceBtcTesting);
        Assert.Equal(0, user01.holdBalanceBtcTesting);

        var orderInputSell = new OrderInput()
        {
            // Transactions = new List<Transaction>(),
            //OrderTransactions = new List<OrderTransaction>(),
            Amount = 5,
            RequestRate = 9000,
            IsBuy = false,
            UserProfile = user02,
            Id = 2
        };
        var orderSell = OrderFactory.Generate(orderInputSell);

        Assert.Equal(5, user02.availableBalanceBtcTesting);

        // transaction 2
        var outputSell = orderSell.ExecuteTest(market.Orders);
        market.Orders.Add(orderSell);
        {
            var total = outputSell.OrderHistories.Count();
            Assert.Equal(3, total);
            orderHistories.AddRange(outputSell.OrderHistories);
        }

        var r2 = outputSell != null;
        Assert.True(r2);

        LogHelperBusiness.Log(market.Orders);
        LogHelperBusiness.Log(market.Orders.First());
        LogHelperBusiness.Log(orderSell);

        Assert.False(orderSell.IsFillComplete);

        Assert.Equal(4, orderSell.LeftAmount);

        Assert.Equal(-10000, user01.availableBalanceIdrTesting);
        Assert.Equal(-10000, user01.balanceIdrTesting);
        Assert.Equal(0, user01.holdBalanceIdrTesting);

        Assert.Equal(1, user01.availableBalanceBtcTesting);
        Assert.Equal(1, user01.balanceBtcTesting);
        Assert.Equal(0, user01.holdBalanceBtcTesting);

        Assert.Equal(10000, user02.availableBalanceIdrTesting);
        Assert.Equal(10000, user02.balanceIdrTesting);
        Assert.Equal(0, user02.holdBalanceIdrTesting);

        Assert.Equal(0, user02.availableBalanceBtcTesting);
        Assert.Equal(4, user02.balanceBtcTesting);
        Assert.Equal(4, user02.holdBalanceBtcTesting);

        var orderInputBuy2 = new OrderInput()
        {
            // Transactions = new List<Transaction>(),
            //OrderTransactions = new List<OrderTransaction>(),
            Amount = 2,
            RequestRate = 10000,
            IsBuy = true,
            UserProfile = user01,
            Id = 3
        };
        var orderBuy2 = OrderFactory.Generate(orderInputBuy2);

        // transaction 3
        var outputBuy2 = orderBuy2.ExecuteTest(market.Orders);
        market.Orders.Add(orderBuy2);
        {
            var total = outputBuy2.OrderHistories.Count();
            Assert.Equal(2, total);
            orderHistories.AddRange(outputBuy2.OrderHistories);
        }

        LogHelperBusiness.Log(market.Orders);

        var r3 = orderBuy2 != null;
        Assert.True(r3);

        Assert.True(orderBuy2.IsFillComplete);
        Assert.False(orderSell.IsFillComplete);
        Assert.Equal(2, orderSell.LeftAmount);


        Assert.Equal(-28000, user01.availableBalanceIdrTesting);
        Assert.Equal(-28000, user01.balanceIdrTesting);
        Assert.Equal(0, user01.holdBalanceIdrTesting);

        Assert.Equal(3, user01.availableBalanceBtcTesting);
        Assert.Equal(3, user01.balanceBtcTesting);
        Assert.Equal(0, user01.holdBalanceBtcTesting);



        Assert.Equal(28000, user02.availableBalanceIdrTesting);
        Assert.Equal(28000, user02.balanceIdrTesting);
        Assert.Equal(0, user02.holdBalanceIdrTesting);


        Assert.Equal(0, user02.availableBalanceBtcTesting);
        Assert.Equal(2, user02.balanceBtcTesting);
        Assert.Equal(2, user02.holdBalanceBtcTesting);

        var totalOrderHistories = orderHistories.Count();
        Assert.Equal(6, totalOrderHistories);

    }


    [Fact]
    public void ScenarioFiveBuySell()
    {

        // var factory = new FactoryTest();
        // factory.IsResetIdDevMode = false;
        var market = new Market()
        {
            Orders = new List<Order>()
        };

        var user01 = new UserProfile();
        user01.username = "test01";
        user01.PopulateCurrency();

        var user02 = new UserProfile();
        user02.username = "test02";
        user02.PopulateCurrency();
        user02.AddBalanceTesting("btc", 5);

        var orderHistories = new List<OrderHistory>();

        var orderInputBuy = new OrderInput()
        {
            // Transactions = new List<Transaction>(),
            //OrderTransactions = new List<OrderTransaction>(),
            Amount = 10,
            RequestRate = 10000,
            IsBuy = true,
            UserProfile = user01,
            Id = 1
        };
        var orderBuy = OrderFactory.Generate(orderInputBuy);

        // transaction 1
        var outputBuy = orderBuy.ExecuteTest(market.Orders);
        market.Orders.Add(orderBuy);
        {
            var total = outputBuy.OrderHistories.Count();
            Assert.Equal(1, total);
            orderHistories.AddRange(outputBuy.OrderHistories);
        }

        var r = outputBuy != null;
        Assert.True(r);

        Assert.False(orderBuy.IsFillComplete);

        Assert.Equal(10, orderBuy.LeftAmount);

        Assert.Equal(-100000, user01.availableBalanceIdrTesting);
        Assert.Equal(0, user01.balanceIdrTesting);
        Assert.Equal(100000, user01.holdBalanceIdrTesting);

        Assert.Equal(0, user01.availableBalanceBtcTesting);
        Assert.Equal(0, user01.balanceBtcTesting);
        Assert.Equal(0, user01.holdBalanceBtcTesting);

        var orderInputSell = new OrderInput()
        {
            // Transactions = new List<Transaction>(),
            //OrderTransactions = new List<OrderTransaction>(),
            Amount = 3,
            RequestRate = 5000,
            IsBuy = false,
            UserProfile = user02,
            Id = 2
        };
        var orderSell = OrderFactory.Generate(orderInputSell);

        Assert.Equal(5, user02.availableBalanceBtcTesting);

        // transaction 2
        var outputSell = orderSell.ExecuteTest(market.Orders);
        market.Orders.Add(orderSell);
        {
            var total = outputSell.OrderHistories.Count();
            Assert.Equal(2, total);
            orderHistories.AddRange(outputSell.OrderHistories);
        }

        var r2 = outputSell != null;
        Assert.True(r2);

        LogHelperBusiness.Log(market.Orders);
        LogHelperBusiness.Log(market.Orders.First());
        LogHelperBusiness.Log(orderSell);

        Assert.True(orderSell.IsFillComplete);

        Assert.Equal(0, orderSell.LeftAmount);


        Assert.Equal(-30000, user01.balanceIdrTesting);
        Assert.Equal(70000, user01.holdBalanceIdrTesting);
        Assert.Equal(-100000, user01.availableBalanceIdrTesting);

        Assert.Equal(3, user01.availableBalanceBtcTesting);
        Assert.Equal(3, user01.balanceBtcTesting);
        Assert.Equal(0, user01.holdBalanceBtcTesting);

        Assert.Equal(30000, user02.availableBalanceIdrTesting);
        Assert.Equal(30000, user02.balanceIdrTesting);
        Assert.Equal(0, user02.holdBalanceIdrTesting);

        Assert.Equal(2, user02.availableBalanceBtcTesting);
        Assert.Equal(2, user02.balanceBtcTesting);
        Assert.Equal(0, user02.holdBalanceBtcTesting);


    }


    [Fact]
    public void ScenarioSixBuySell()
    {

        // var factory = new FactoryTest();
        // factory.IsResetIdDevMode = false;
        var market = new Market()
        {
            Orders = new List<Order>()
        };

        var user01 = new UserProfile();
        user01.username = "test01";
        user01.PopulateCurrency();

        var user02 = new UserProfile();
        user02.username = "test02";
        user02.PopulateCurrency();
        user02.AddBalanceTesting("btc", 5);

        var orderHistories = new List<OrderHistory>();

        var orderInputBuy = new OrderInput()
        {
            // Transactions = new List<Transaction>(),
            //OrderTransactions = new List<OrderTransaction>(),
            Amount = 10,
            RequestRate = 10000,
            IsBuy = true,
            UserProfile = user01,
            Id = 1
        };
        var orderBuy = OrderFactory.Generate(orderInputBuy);

        // transaction 1
        var outputBuy = orderBuy.ExecuteTest(market.Orders);
        market.Orders.Add(orderBuy);
        {
            var total = outputBuy.OrderHistories.Count();
            Assert.Equal(1, total);
            orderHistories.AddRange(outputBuy.OrderHistories);
        }

        var r = outputBuy != null;
        Assert.True(r);

        Assert.False(orderBuy.IsFillComplete);

        Assert.Equal(10, orderBuy.LeftAmount);

        Assert.Equal(-100000, user01.availableBalanceIdrTesting);
        Assert.Equal(0, user01.balanceIdrTesting);
        Assert.Equal(100000, user01.holdBalanceIdrTesting);

        Assert.Equal(0, user01.availableBalanceBtcTesting);
        Assert.Equal(0, user01.balanceBtcTesting);
        Assert.Equal(0, user01.holdBalanceBtcTesting);

        var orderInputSell = new OrderInput()
        {
            // Transactions = new List<Transaction>(),
            //OrderTransactions = new List<OrderTransaction>(),
            Amount = 3,
            RequestRate = 5000,
            IsBuy = false,
            UserProfile = user02,
            Id = 2
        };
        var orderSell = OrderFactory.Generate(orderInputSell);

        Assert.Equal(5, user02.availableBalanceBtcTesting);

        // transaction 2
        var outputSell = orderSell.ExecuteTest(market.Orders);
        market.Orders.Add(orderSell);
        {
            var total = outputSell.OrderHistories.Count();
            Assert.Equal(2, total);
            orderHistories.AddRange(outputSell.OrderHistories);
        }

        var r2 = outputSell != null;
        Assert.True(r2);

        LogHelperBusiness.Log(market.Orders);
        LogHelperBusiness.Log(market.Orders.First());
        LogHelperBusiness.Log(orderSell);

        Assert.True(orderSell.IsFillComplete);

        Assert.Equal(0, orderSell.LeftAmount);


        Assert.Equal(-30000, user01.balanceIdrTesting);
        Assert.Equal(70000, user01.holdBalanceIdrTesting);
        Assert.Equal(-100000, user01.availableBalanceIdrTesting);

        Assert.Equal(3, user01.availableBalanceBtcTesting);
        Assert.Equal(3, user01.balanceBtcTesting);
        Assert.Equal(0, user01.holdBalanceBtcTesting);

        Assert.Equal(30000, user02.availableBalanceIdrTesting);
        Assert.Equal(30000, user02.balanceIdrTesting);
        Assert.Equal(0, user02.holdBalanceIdrTesting);

        Assert.Equal(2, user02.availableBalanceBtcTesting);
        Assert.Equal(2, user02.balanceBtcTesting);
        Assert.Equal(0, user02.holdBalanceBtcTesting);


    }
}