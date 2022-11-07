using System.Collections.Generic;
using System.Linq;
using BlueLight.Main;
using Xunit;
public class UnitTestHoldBuy
{

    [Fact]
    public void AddHoldBuyEmptyMarketAndTestRunningBalance()
    {
        // var factory = new FactoryTest();
        // factory.IsResetIdDevMode = false;
        var market = new Market()
        {
            Orders = new List<Order>()
        };

        var user = new UserProfile();
        user.PopulateCurrency();

        var orderInput = new OrderInput()
        {
            // Transactions = new List<Transaction>(),
            //OrderTransactions = new List<OrderTransaction>(),
            Amount = 5,
            RequestRate = 9000,
            IsBuy = true,
            UserProfile = user
        };
        var order = OrderFactory.Generate(orderInput);
        var output = order.ExecuteTest(market.Orders);
        var r = output.CreatedOrder != null;
        Assert.True(r);


        // 



        // Assert.Equal(false, factory.order1.IsComplete);
        // Assert.Equal(false, factory.order2.IsComplete);
        Assert.False(order.IsFillComplete);


        Assert.Equal(5, order.LeftAmount);

        // Assert.Equal(3, factory.order1.LeftAmount);
        // Assert.Equal(2, factory.order2.LeftAmount);



        Assert.Equal(-45000, user.availableBalanceIdrTesting);
        Assert.Equal(0, user.balanceIdrTesting);
        Assert.Equal(45000, user.holdBalanceIdrTesting);

        Assert.Equal(0, user.availableBalanceBtcTesting);
        Assert.Equal(0, user.balanceBtcTesting);
        Assert.Equal(0, user.holdBalanceBtcTesting);

        var detail = user.GetUserProfileDetail("idr");
        var transaction = detail.HoldTransactions.First();
        Assert.Equal(45000, transaction.RunningHoldBalance);

    }

    [Fact]
    public void AddHoldBuy()
    {
        var factory = new FactoryTest();
        factory.IsResetIdDevMode = false;
        var market = factory.Generate();

        var user = new UserProfile();
        user.PopulateCurrency();

        var orderInput = new OrderInput()
        {
            // Transactions = new List<Transaction>(),
            //OrderTransactions = new List<OrderTransaction>(),
            Amount = 5,
            RequestRate = 9000,
            IsBuy = true,
            UserProfile = user
        };
        var order = OrderFactory.Generate(orderInput);
        var output = order.ExecuteTest(market.Orders);
        var r = output.CreatedOrder != null;
        Assert.True(r);


        // 



        Assert.False(factory.order1.IsFillComplete);
        Assert.False(factory.order2.IsFillComplete);
        Assert.False(order.IsFillComplete);


        Assert.Equal(5, order.LeftAmount);

        Assert.Equal(3, factory.order1.LeftAmount);
        Assert.Equal(2, factory.order2.LeftAmount);



        Assert.Equal(-45000, user.availableBalanceIdrTesting);
        Assert.Equal(0, user.balanceIdrTesting);
        Assert.Equal(45000, user.holdBalanceIdrTesting);

        Assert.Equal(0, user.availableBalanceBtcTesting);
        Assert.Equal(0, user.balanceBtcTesting);
        Assert.Equal(0, user.holdBalanceBtcTesting);
    }

    [Fact]
    public void RemoveHoldTransactionCompleteBuy()
    {
        var factory = new FactoryTest();
        factory.IsResetIdDevMode = false;
        var market = factory.Generate();

        var user = new UserProfile();
        user.PopulateCurrency();
        user.username = "buyer";

        var orderInput = new OrderInput()
        {
            Id = 1,
            //Transactions = new List<Transaction>(),
            //OrderTransactions = new List<OrderTransaction>(),
            Amount = 5,
            RequestRate = 9000,
            IsBuy = true,
            UserProfile = user
        };
        var order = OrderFactory.Generate(orderInput);
        var output = order.ExecuteTest(market.Orders);
        var r = output.CreatedOrder != null;
        Assert.True(r);


        // 



        Assert.False(factory.order1.IsFillComplete);
        Assert.False(factory.order2.IsFillComplete);
        Assert.False(order.IsFillComplete);


        Assert.Equal(5, order.LeftAmount);

        Assert.Equal(3, factory.order1.LeftAmount);
        Assert.Equal(2, factory.order2.LeftAmount);



        Assert.Equal(-45000, user.availableBalanceIdrTesting);
        Assert.Equal(0, user.balanceIdrTesting);
        Assert.Equal(45000, user.holdBalanceIdrTesting);

        Assert.Equal(0, user.availableBalanceBtcTesting);
        Assert.Equal(0, user.balanceBtcTesting);
        Assert.Equal(0, user.holdBalanceBtcTesting);


        ICollection<Order> Orders = new List<Order>();
        // market.Orders.Add(order);
        Orders.Add(order);

        var user1 = new UserProfile();
        user1.PopulateCurrency();
        user1.username = "seller";

        var orderInput1 = new OrderInput()
        {
            Id = 2,
            //Transactions = new List<Transaction>(),
            //OrderTransactions = new List<OrderTransaction>(),
            Amount = 5,
            RequestRate = 9000,
            IsBuy = false,
            UserProfile = user1
        };
        var order1 = OrderFactory.Generate(orderInput1);
        Assert.False(order.IsFillComplete);
        Assert.False(order1.IsFillComplete);

        order1.ExecuteTest(Orders);

        Assert.True(order.IsFillComplete);
        Assert.True(order1.IsFillComplete);



        Assert.Equal(-45000, user.availableBalanceIdrTesting);
        Assert.Equal(-45000, user.balanceIdrTesting);
        Assert.Equal(0, user.holdBalanceIdrTesting);

        Assert.Equal(5, user.availableBalanceBtcTesting);
        Assert.Equal(5, user.balanceBtcTesting);
        Assert.Equal(0, user.holdBalanceBtcTesting);


        Assert.Equal(45000, user1.availableBalanceIdrTesting);
        Assert.Equal(45000, user1.balanceIdrTesting);
        Assert.Equal(0, user1.holdBalanceIdrTesting);

        Assert.Equal(-5, user1.availableBalanceBtcTesting);
        Assert.Equal(-5, user1.balanceBtcTesting);
        Assert.Equal(0, user1.holdBalanceBtcTesting);

        // Assert.Equal(-5, user.availableBalanceBtc);
        // Assert.Equal(-5, user.balanceBtc);
        // Assert.Equal(0, user.holdBalanceBtc);


        // Assert.Equal(5, user1.availableBalanceBtc);
        // Assert.Equal(5, user1.balanceIdr);
        // Assert.Equal(0, user1.holdBalanceBtc);
    }


    [Fact]
    public void RemoveHoldTransactionCompleteHalfBuy()
    {
        var factory = new FactoryTest();
        factory.IsResetIdDevMode = false;
        var market = factory.Generate();

        var buyer = new UserProfile();
        buyer.PopulateCurrency();
        buyer.username = "buyer";

        var orderInput = new OrderInput()
        {
            Id = 1,
            //Transactions = new List<Transaction>(),
            //OrderTransactions = new List<OrderTransaction>(),
            Amount = 5,
            RequestRate = 9000,
            IsBuy = true,
            UserProfile = buyer
        };
        var order = OrderFactory.Generate(orderInput);
        var output = order.ExecuteTest(market.Orders);
        var r = output.CreatedOrder != null;
        Assert.True(r);


        // 



        Assert.False(factory.order1.IsFillComplete);
        Assert.False(factory.order2.IsFillComplete);
        Assert.False(order.IsFillComplete);


        Assert.Equal(5, order.LeftAmount);

        Assert.Equal(3, factory.order1.LeftAmount);
        Assert.Equal(2, factory.order2.LeftAmount);



        Assert.Equal(-45000, buyer.availableBalanceIdrTesting);
        Assert.Equal(0, buyer.balanceIdrTesting);
        Assert.Equal(45000, buyer.holdBalanceIdrTesting);

        Assert.Equal(0, buyer.availableBalanceBtcTesting);
        Assert.Equal(0, buyer.balanceBtcTesting);
        Assert.Equal(0, buyer.holdBalanceBtcTesting);


        ICollection<Order> Orders = new List<Order>();
        // market.Orders.Add(order);
        Orders.Add(order);

        var seller = new UserProfile();
        seller.PopulateCurrency();
        seller.username = "seller";

        var orderInput1 = new OrderInput()
        {
            Id = 2,
            //Transactions = new List<Transaction>(),
            //OrderTransactions = new List<OrderTransaction>(),
            Amount = 3,
            RequestRate = 9000,
            IsBuy = false,
            UserProfile = seller
        };
        var order1 = OrderFactory.Generate(orderInput1);
        Assert.False(order.IsFillComplete);
        Assert.False(order1.IsFillComplete);

        order1.ExecuteTest(Orders);

        Assert.False(order.IsFillComplete);
        Assert.True(order1.IsFillComplete);



        Assert.Equal(-45000, buyer.availableBalanceIdrTesting);
        Assert.Equal(-27000, buyer.balanceIdrTesting);
        Assert.Equal(18000, buyer.holdBalanceIdrTesting);

        Assert.Equal(3, buyer.availableBalanceBtcTesting);
        Assert.Equal(3, buyer.balanceBtcTesting);
        Assert.Equal(0, buyer.holdBalanceBtcTesting);


        Assert.Equal(27000, seller.availableBalanceIdrTesting);
        Assert.Equal(27000, seller.balanceIdrTesting);
        Assert.Equal(0, seller.holdBalanceIdrTesting);

        Assert.Equal(-3, seller.availableBalanceBtcTesting);
        Assert.Equal(-3, seller.balanceBtcTesting);
        Assert.Equal(0, seller.holdBalanceBtcTesting);


    }

    [Fact]
    public void CancelHoldTransactionBuy()
    {
        var factory = new FactoryTest();
        factory.IsResetIdDevMode = false;
        var market = factory.Generate();

        var user = new UserProfile();
        user.PopulateCurrency();

        var orderInput = new OrderInput()
        {
            // Transactions = new List<Transaction>(),
            //OrderTransactions = new List<OrderTransaction>(),
            Amount = 5,
            RequestRate = 9000,
            IsBuy = true,
            UserProfile = user
        };
        var order = OrderFactory.Generate(orderInput);
        var output = order.ExecuteTest(market.Orders);
        var r = output.CreatedOrder != null;
        Assert.True(r);


        // 



        Assert.False(factory.order1.IsFillComplete);
        Assert.False(factory.order2.IsFillComplete);
        Assert.False(order.IsFillComplete);


        Assert.Equal(5, order.LeftAmount);

        Assert.Equal(3, factory.order1.LeftAmount);
        Assert.Equal(2, factory.order2.LeftAmount);



        Assert.Equal(-45000, user.availableBalanceIdrTesting);
        Assert.Equal(0, user.balanceIdrTesting);
        Assert.Equal(45000, user.holdBalanceIdrTesting);

        Assert.Equal(0, user.availableBalanceBtcTesting);
        Assert.Equal(0, user.balanceBtcTesting);
        Assert.Equal(0, user.holdBalanceBtcTesting);

        // order.CancelBuy();
        order.Cancel();


        Assert.Equal(0, user.availableBalanceIdrTesting);
        Assert.Equal(0, user.balanceIdrTesting);
        Assert.Equal(0, user.holdBalanceIdrTesting);

        Assert.Equal(0, user.availableBalanceBtcTesting);
        Assert.Equal(0, user.balanceBtcTesting);
        Assert.Equal(0, user.holdBalanceBtcTesting);

    }


    [Fact]
    public void CancelHoldTransactionHalfBuy()
    {
        var factory = new FactoryTest();
        factory.IsResetIdDevMode = false;
        var market = factory.Generate();

        var buyer = new UserProfile();
        buyer.PopulateCurrency();
        buyer.username = "buyer";

        var orderInput = new OrderInput()
        {
            Id = 1,
            //Transactions = new List<Transaction>(),
            //OrderTransactions = new List<OrderTransaction>(),
            Amount = 5,
            RequestRate = 9000,
            IsBuy = true,
            UserProfile = buyer
        };
        var order = OrderFactory.Generate(orderInput);
        var output = order.ExecuteTest(market.Orders);
        var r = output.CreatedOrder != null;
        Assert.True(r);


        // 



        Assert.False(factory.order1.IsFillComplete);
        Assert.False(factory.order2.IsFillComplete);
        Assert.False(order.IsFillComplete);


        Assert.Equal(5, order.LeftAmount);

        Assert.Equal(3, factory.order1.LeftAmount);
        Assert.Equal(2, factory.order2.LeftAmount);



        Assert.Equal(-45000, buyer.availableBalanceIdrTesting);
        Assert.Equal(0, buyer.balanceIdrTesting);
        Assert.Equal(45000, buyer.holdBalanceIdrTesting);

        Assert.Equal(0, buyer.availableBalanceBtcTesting);
        Assert.Equal(0, buyer.balanceBtcTesting);
        Assert.Equal(0, buyer.holdBalanceBtcTesting);


        ICollection<Order> Orders = new List<Order>();
        // market.Orders.Add(order);
        Orders.Add(order);

        var seller = new UserProfile();
        seller.PopulateCurrency();
        seller.username = "seller";

        var orderInput1 = new OrderInput()
        {
            Id = 2,
            //Transactions = new List<Transaction>(),
            //OrderTransactions = new List<OrderTransaction>(),
            Amount = 3,
            RequestRate = 9000,
            IsBuy = false,
            UserProfile = seller
        };
        var order1 = OrderFactory.Generate(orderInput1);
        Assert.False(order.IsFillComplete);
        Assert.False(order1.IsFillComplete);

        order1.ExecuteTest(Orders);

        Assert.False(order.IsFillComplete);
        Assert.True(order1.IsFillComplete);



        Assert.Equal(-45000, buyer.availableBalanceIdrTesting);
        Assert.Equal(-27000, buyer.balanceIdrTesting);
        Assert.Equal(18000, buyer.holdBalanceIdrTesting);

        Assert.Equal(3, buyer.availableBalanceBtcTesting);
        Assert.Equal(3, buyer.balanceBtcTesting);
        Assert.Equal(0, buyer.holdBalanceBtcTesting);


        Assert.Equal(27000, seller.availableBalanceIdrTesting);
        Assert.Equal(27000, seller.balanceIdrTesting);
        Assert.Equal(0, seller.holdBalanceIdrTesting);

        Assert.Equal(-3, seller.availableBalanceBtcTesting);
        Assert.Equal(-3, seller.balanceBtcTesting);
        Assert.Equal(0, seller.holdBalanceBtcTesting);






        // order.CancelBuy();
        order.Cancel();


        Assert.Equal(-27000, buyer.availableBalanceIdrTesting);
        Assert.Equal(-27000, buyer.balanceIdrTesting);
        Assert.Equal(0, buyer.holdBalanceIdrTesting);

        Assert.Equal(3, buyer.availableBalanceBtcTesting);
        Assert.Equal(3, buyer.balanceBtcTesting);
        Assert.Equal(0, buyer.holdBalanceBtcTesting);
    }


    [Fact]
    public void CancelHoldHalfBuyAfterSellFull()
    {
        var factory = new FactoryTest();
        factory.IsResetIdDevMode = false;
        var market = factory.GenerateSampleBuyer();

        var user = new UserProfile();
        user.PopulateCurrency();
        user.username = "seller";


        // buy
        // id 11 - 2 - 4000
        // id 12 - 2 - 3000

        var orderInput = new OrderInput()
        {
            // Transactions = new List<Transaction>(),
            //OrderTransactions = new List<OrderTransaction>(),
            Amount = 3,
            RequestRate = 3000,
            IsBuy = false,
            UserProfile = user
        };
        var order = OrderFactory.Generate(orderInput);
        var output = order.ExecuteTest(market.Orders);
        var r = output.CreatedOrder != null;
        Assert.False(r);


        var transactions = order.Transactions.ToList();
        {
            var transcation = transactions[0];
            // var buyer = transcation.Detail.GetBuyer();
            var buyer = transcation.GetBuyOrderFirst();
            Assert.Equal(11, buyer.Id);
            Assert.Equal(0, buyer.LeftAmount);

        }
        {
            var transcation = transactions[1];
            // var buyer = transcation.Detail.GetBuyer();
            var buyer = transcation.GetBuyOrderFirst();
            Assert.Equal(12, buyer.Id);
            Assert.Equal(1, buyer.LeftAmount);
        }

        var totalTransaction = order.Transactions.Count;
        //var totalTransaction = order.OrderTransactions.Count;
        Assert.Equal(2, totalTransaction);


        Assert.True(factory.order11.IsFillComplete);

        Assert.False(factory.order12.IsFillComplete);

        Assert.True(order.IsFillComplete);

        Assert.Equal(0, factory.order11.LeftAmount);
        Assert.Equal(1, factory.order12.LeftAmount);

        var order12 = factory.order12;
        var buyer2 = order12.UserProfile;
        var username = buyer2.username;


        var total = 14000 - (4000 * 2) - 3000;

        Assert.Equal(0, buyer2.availableBalanceIdrTesting);
        Assert.Equal(total, buyer2.balanceIdrTesting);
        Assert.Equal(total, buyer2.holdBalanceIdrTesting);

        Assert.Equal(3, buyer2.availableBalanceBtcTesting);
        Assert.Equal(3, buyer2.balanceBtcTesting);
        Assert.Equal(0, buyer2.holdBalanceBtcTesting);

        order12.Cancel();


        Assert.Equal(total, buyer2.availableBalanceIdrTesting);
        Assert.Equal(total, buyer2.balanceIdrTesting);
        Assert.Equal(0, buyer2.holdBalanceIdrTesting);

        Assert.Equal(3, buyer2.availableBalanceBtcTesting);
        Assert.Equal(3, buyer2.balanceBtcTesting);
        Assert.Equal(0, buyer2.holdBalanceBtcTesting);

    }

}
