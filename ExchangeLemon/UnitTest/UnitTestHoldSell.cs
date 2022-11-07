using System.Collections.Generic;
using System.Linq;
using BlueLight.Main;
using Xunit;
public class UnitTestHoldSell
{

    [Fact]
    public void AddHoldSellEmptyMarket()
    {

        var market = new Market()
        {
            Orders = new List<Order>()
        };

        var user = new UserProfile();
        user.PopulateCurrency();
        user.username = "seller";

        var orderInput = new OrderInput()
        {
            // Transactions = new List<Transaction>(),
            // OrderTransactions = new List<OrderTransaction>(),
            Amount = 5,
            RequestRate = 10000,
            IsBuy = false,
            UserProfile = user
        };
        var order = OrderFactory.Generate(orderInput);
        var output = order.ExecuteTest(market.Orders);
        var r = output.CreatedOrder != null;
        Assert.True(r);

        Assert.False(order.IsFillComplete);


        Assert.Equal(5, order.LeftAmount);

        Assert.Equal(0, user.availableBalanceIdrTesting);
        Assert.Equal(0, user.balanceIdrTesting);
        Assert.Equal(0, user.holdBalanceIdrTesting);

        Assert.Equal(-5, user.availableBalanceBtcTesting);
        Assert.Equal(0, user.balanceBtcTesting);
        Assert.Equal(5, user.holdBalanceBtcTesting);
    }

    [Fact]
    public void AddHoldSell()
    {
        var factory = new FactoryTest();
        factory.IsResetIdDevMode = false;
        var market = factory.Generate();

        var user = new UserProfile();
        user.PopulateCurrency();
        user.username = "seller";

        var orderInput = new OrderInput()
        {
            // Transactions = new List<Transaction>(),
            // OrderTransactions = new List<OrderTransaction>(),
            Amount = 5,
            RequestRate = 10000,
            IsBuy = false,
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



        Assert.Equal(0, user.availableBalanceIdrTesting);
        Assert.Equal(0, user.balanceIdrTesting);
        Assert.Equal(0, user.holdBalanceIdrTesting);

        Assert.Equal(-5, user.availableBalanceBtcTesting);
        Assert.Equal(0, user.balanceBtcTesting);
        Assert.Equal(5, user.holdBalanceBtcTesting);
    }

    [Fact]
    public void RemoveHoldTransactionCompleteSell()
    {
        var factory = new FactoryTest();
        factory.IsResetIdDevMode = false;
        var market = factory.Generate();

        var seller = new UserProfile();
        seller.PopulateCurrency();
        seller.username = "seller";

        var orderInput = new OrderInput()
        {
            Id = 1,
            //Transactions = new List<Transaction>(),
            // OrderTransactions = new List<OrderTransaction>(),
            Amount = 5,
            RequestRate = 10000,
            IsBuy = false,
            UserProfile = seller
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



        Assert.Equal(0, seller.availableBalanceIdrTesting);
        Assert.Equal(0, seller.balanceIdrTesting);
        Assert.Equal(0, seller.holdBalanceIdrTesting);

        Assert.Equal(-5, seller.availableBalanceBtcTesting);
        Assert.Equal(0, seller.balanceBtcTesting);
        Assert.Equal(5, seller.holdBalanceBtcTesting);


        ICollection<Order> Orders = new List<Order>();
        // market.Orders.Add(order);
        Orders.Add(order);

        var buyer = new UserProfile();
        buyer.PopulateCurrency();
        buyer.username = "buyer";

        var orderInput1 = new OrderInput()
        {
            Id = 2,
            //Transactions = new List<Transaction>(),
            // OrderTransactions = new List<OrderTransaction>(),
            Amount = 5,
            RequestRate = 10000,
            IsBuy = true,
            UserProfile = buyer
        };
        var order1 = OrderFactory.Generate(orderInput1);

        Assert.False(order.IsFillComplete);
        Assert.False(order1.IsFillComplete);




        order1.ExecuteTest(Orders);

        Assert.True(order.IsFillComplete);
        Assert.True(order1.IsFillComplete);



        Assert.Equal(50000, seller.availableBalanceIdrTesting);
        Assert.Equal(50000, seller.balanceIdrTesting);
        Assert.Equal(0, seller.holdBalanceIdrTesting);

        Assert.Equal(-5, seller.availableBalanceBtcTesting);
        Assert.Equal(-5, seller.balanceBtcTesting);
        Assert.Equal(0, seller.holdBalanceBtcTesting);


        Assert.Equal(-50000, buyer.availableBalanceIdrTesting);
        Assert.Equal(-50000, buyer.balanceIdrTesting);
        Assert.Equal(0, buyer.holdBalanceIdrTesting);

        Assert.Equal(5, buyer.availableBalanceBtcTesting);
        Assert.Equal(5, buyer.balanceBtcTesting);
        Assert.Equal(0, buyer.holdBalanceBtcTesting);


    }


    [Fact]
    public void RemoveHoldTransactionCompleteHalfSell()
    {
        var factory = new FactoryTest();
        factory.IsResetIdDevMode = false;
        // var market = factory.Generate();

        ICollection<Order> Orders = new List<Order>();
        // market.Orders.Add(order);


        var seller = new UserProfile();
        seller.PopulateCurrency();
        seller.username = "seller";

        seller.AddBalanceTesting("btc", 5);

        var orderInput = new OrderInput()
        {
            Id = 1,
            //Transactions = new List<Transaction>(),
            // OrderTransactions = new List<OrderTransaction>(),
            Amount = 5,
            RequestRate = 10000,
            IsBuy = false,
            UserProfile = seller
        };
        var order = OrderFactory.Generate(orderInput);
        Orders.Add(order);
        var output = order.ExecuteTest(Orders);
        var r = output.CreatedOrder != null;
        Assert.True(r);


        // 



        // Assert.Equal(false, factory.order1.IsComplete);
        // Assert.Equal(false, factory.order2.IsComplete);
        Assert.False(order.IsFillComplete);


        Assert.Equal(5, order.LeftAmount);

        // Assert.Equal(3, factory.order1.LeftAmount);
        // Assert.Equal(2, factory.order2.LeftAmount);



        Assert.Equal(0, seller.availableBalanceIdrTesting);
        Assert.Equal(0, seller.balanceIdrTesting);
        Assert.Equal(0, seller.holdBalanceIdrTesting);

        Assert.Equal(0, seller.availableBalanceBtcTesting);
        Assert.Equal(5, seller.balanceBtcTesting);
        Assert.Equal(5, seller.holdBalanceBtcTesting);


        // ICollection<Order> Orders = new List<Order>();
        // market.Orders.Add(order);
        // Orders.Add(order);

        var buyer = new UserProfile();
        buyer.PopulateCurrency();
        buyer.username = "buyer";

        var orderInput1 = new OrderInput()
        {
            Id = 2,
            // Transactions = new List<Transaction>(),
            // OrderTransactions = new List<OrderTransaction>(),
            Amount = 3,
            RequestRate = 10000,
            IsBuy = true,
            UserProfile = buyer,
            //Transactions = new List<Transaction>(),
        };
        var order1 = OrderFactory.Generate(orderInput1);
        Orders.Add(order1);

        Assert.False(order.IsFillComplete);
        Assert.False(order1.IsFillComplete);

        order1.ExecuteTest(Orders);

        Assert.False(order.IsFillComplete);
        Assert.True(order1.IsFillComplete);



        Assert.Equal(30000, seller.availableBalanceIdrTesting);
        Assert.Equal(30000, seller.balanceIdrTesting);
        Assert.Equal(0, seller.holdBalanceIdrTesting);

        Assert.Equal(0, seller.availableBalanceBtcTesting);
        Assert.Equal(2, seller.balanceBtcTesting);
        Assert.Equal(2, seller.holdBalanceBtcTesting);


        Assert.Equal(-30000, buyer.availableBalanceIdrTesting);
        Assert.Equal(-30000, buyer.balanceIdrTesting);
        Assert.Equal(0, buyer.holdBalanceIdrTesting);

        Assert.Equal(3, buyer.availableBalanceBtcTesting);
        Assert.Equal(3, buyer.balanceBtcTesting);
        Assert.Equal(0, buyer.holdBalanceBtcTesting);


    }

    [Fact]
    public void CancelHoldTransactionSell()
    {
        var factory = new FactoryTest();
        factory.IsResetIdDevMode = false;
        var market = factory.Generate();

        var seller = new UserProfile();
        seller.username = "seller";
        seller.PopulateCurrency();
        seller.AddBalanceTesting("btc", 5);

        var orderInput = new OrderInput()
        {
            //Transactions = new List<Transaction>(),
            // OrderTransactions = new List<OrderTransaction>(),
            Amount = 5,
            RequestRate = 10000,
            IsBuy = false,
            UserProfile = seller
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



        Assert.Equal(0, seller.availableBalanceIdrTesting);
        Assert.Equal(0, seller.balanceIdrTesting);
        Assert.Equal(0, seller.holdBalanceIdrTesting);

        Assert.Equal(0, seller.availableBalanceBtcTesting);
        Assert.Equal(5, seller.balanceBtcTesting);
        Assert.Equal(5, seller.holdBalanceBtcTesting);

        // order.CancelSell();
        order.Cancel();


        Assert.Equal(0, seller.availableBalanceIdrTesting);
        Assert.Equal(0, seller.balanceIdrTesting);
        Assert.Equal(0, seller.holdBalanceIdrTesting);

        Assert.Equal(5, seller.availableBalanceBtcTesting);
        Assert.Equal(5, seller.balanceBtcTesting);
        Assert.Equal(0, seller.holdBalanceBtcTesting);

    }



    [Fact]
    public void CancelHoldTransactionHalfSell()
    {
        var factory = new FactoryTest();
        factory.IsResetIdDevMode = false;
        var market = factory.Generate();

        var seller = new UserProfile();
        seller.PopulateCurrency();
        seller.username = "seller";

        seller.AddBalanceTesting("btc", 5);

        var orderInput = new OrderInput()
        {
            Id = 1,
            //Transactions = new List<Transaction>(),
            // OrderTransactions = new List<OrderTransaction>(),
            Amount = 5,
            RequestRate = 10000,
            IsBuy = false,
            UserProfile = seller
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



        Assert.Equal(0, seller.availableBalanceIdrTesting);
        Assert.Equal(0, seller.balanceIdrTesting);
        Assert.Equal(0, seller.holdBalanceIdrTesting);

        Assert.Equal(0, seller.availableBalanceBtcTesting);
        Assert.Equal(5, seller.balanceBtcTesting);
        Assert.Equal(5, seller.holdBalanceBtcTesting);


        ICollection<Order> Orders = new List<Order>();
        // market.Orders.Add(order);
        Orders.Add(order);

        var buyer = new UserProfile();
        buyer.PopulateCurrency();
        buyer.username = "buyer";

        var orderInput1 = new OrderInput()
        {
            Id = 2,
            //Transactions = new List<Transaction>(),
            //// OrderTransactions = new List<OrderTransaction>(),
            Amount = 3,
            RequestRate = 10000,
            IsBuy = true,
            UserProfile = buyer
        };
        var order1 = OrderFactory.Generate(orderInput1);

        Assert.False(order.IsFillComplete);
        Assert.False(order1.IsFillComplete);

        order1.ExecuteTest(Orders);

        Assert.False(order.IsFillComplete);
        Assert.True(order1.IsFillComplete);



        Assert.Equal(30000, seller.availableBalanceIdrTesting);
        Assert.Equal(30000, seller.balanceIdrTesting);
        Assert.Equal(0, seller.holdBalanceIdrTesting);

        Assert.Equal(0, seller.availableBalanceBtcTesting);
        Assert.Equal(2, seller.balanceBtcTesting);
        Assert.Equal(2, seller.holdBalanceBtcTesting);


        Assert.Equal(-30000, buyer.availableBalanceIdrTesting);
        Assert.Equal(-30000, buyer.balanceIdrTesting);
        Assert.Equal(0, buyer.holdBalanceIdrTesting);

        Assert.Equal(3, buyer.availableBalanceBtcTesting);
        Assert.Equal(3, buyer.balanceBtcTesting);
        Assert.Equal(0, buyer.holdBalanceBtcTesting);






        // order.CancelSell();
        order.Cancel();


        Assert.Equal(30000, seller.availableBalanceIdrTesting);
        Assert.Equal(30000, seller.balanceIdrTesting);
        Assert.Equal(0, seller.holdBalanceIdrTesting);

        Assert.Equal(2, seller.availableBalanceBtcTesting);
        Assert.Equal(2, seller.balanceBtcTesting);
        Assert.Equal(0, seller.holdBalanceBtcTesting);
    }


    [Fact]
    public void CancelHoldHalfSellAfterBuyFull()
    {
        var factory = new FactoryTest();
        factory.IsResetIdDevMode = false;
        var market = factory.GenerateSampleSales();

        var buyer = new UserProfile();
        buyer.PopulateCurrency();
        buyer.username = "buyer";


        // 1 - 3 , 11500
        // 2 - 2 , 11000

        var orderInput = new OrderInput()
        {
            //Transactions = new List<Transaction>(),
            // OrderTransactions = new List<OrderTransaction>(),
            Amount = 4,
            RequestRate = 11500,
            IsBuy = true,
            UserProfile = buyer
        };
        var order = OrderFactory.Generate(orderInput);
        var output = order.ExecuteTest(market.Orders);
        var r = output.CreatedOrder != null;
        Assert.False(r);


        var transactions = order.Transactions.ToList();
        {
            var transcation = transactions[0];
            // var buyer = transcation.Detail.GetBuyer();
            var seller = transcation.GetSellOrderFirst();
            Assert.Equal(2, seller.Id);
            Assert.Equal(0, seller.LeftAmount);

        }
        {
            var transcation = transactions[1];
            // var buyer = transcation.Detail.GetBuyer();
            var seller = transcation.GetSellOrderFirst();
            Assert.Equal(1, seller.Id);
            Assert.Equal(1, seller.LeftAmount);
        }

        // var totalTransaction = order.Transactions.Count;
        var totalTransaction = order.Transactions.Count;
        Assert.Equal(2, totalTransaction);


        Assert.False(factory.order1.IsFillComplete);

        Assert.True(factory.order2.IsFillComplete);

        Assert.True(order.IsFillComplete);

        Assert.Equal(1, factory.order1.LeftAmount);
        Assert.Equal(0, factory.order2.LeftAmount);

        var order1 = factory.order1;

        var seller2 = order1.UserProfile;
        var username = seller2.username;


        var balanceIdr = (11500 * 2 + 11000 * 2);


        Assert.Equal(balanceIdr, seller2.availableBalanceIdrTesting);
        Assert.Equal(balanceIdr, seller2.balanceIdrTesting);
        Assert.Equal(0, seller2.holdBalanceIdrTesting);

        Assert.Equal(0, seller2.availableBalanceBtcTesting);
        Assert.Equal(1, seller2.balanceBtcTesting);
        Assert.Equal(1, seller2.holdBalanceBtcTesting);

        // order.CancelBuy();
        // order.CancelSell();
        order1.Cancel();


        Assert.Equal(balanceIdr, seller2.availableBalanceIdrTesting);
        Assert.Equal(balanceIdr, seller2.balanceIdrTesting);
        Assert.Equal(0, seller2.holdBalanceIdrTesting);

        Assert.Equal(1, seller2.availableBalanceBtcTesting);
        Assert.Equal(1, seller2.balanceBtcTesting);
        Assert.Equal(0, seller2.holdBalanceBtcTesting);
    }

}
