using System;
using System.Collections.Generic;
using System.Diagnostics;
//using LibraryOne;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
//using Serilog;
using Xunit;
using System.Linq;
using BlueLight.Main;

namespace UnitTest
{

    public class UnitTestBuy
    {




        [Fact]
        public void BuyTwoFull()
        {
            var factory = new FactoryTest();
            factory.IsResetIdDevMode = false;
            var market = factory.Generate();


            var user = new UserProfile();
            user.PopulateCurrency();

            var orderInput = new OrderInput()
            {
                // Transactions = new List<Transaction>(),
                //// OrderTransactions = new List<OrderTransaction>(),
                Amount = 5,
                RequestRate = 11500,
                IsBuy = true,
                UserProfile = user
            };
            var order = OrderFactory.Generate(orderInput);

            var output = order.ExecuteTest(market.Orders);
            var r = output.CreatedOrder != null;
            Assert.False(r);


            //var transactions = order.Transactions.ToList();

            var transactions = order.Transactions.ToList();
            {
                var transcation = transactions[0];
                // var buyer = transcation.Detail.GetBuyer();

                var seller = transcation.GetSellOrderFirst();
                Assert.Equal(0, seller.LeftAmount);
            }
            {
                var transcation = transactions[1];
                // var buyer = transcation.Detail.GetBuyer();
                var seller = transcation.GetSellOrderFirst();
                Assert.Equal(0, seller.LeftAmount);
            }

            // var totalTransaction = order.Transactions.Count;
            var totalTransaction = order.Transactions.Count;
            Assert.Equal(2, totalTransaction);


            Assert.True(factory.order3.IsFillComplete);
            Assert.True(factory.order2.IsFillComplete);
            Assert.True(order.IsFillComplete);
        }


        [Fact]
        public void BuyOneFull()
        {
            var factory = new FactoryTest();
            factory.IsResetIdDevMode = false;
            var market = factory.Generate();

            var user = new UserProfile();
            user.PopulateCurrency();

            var orderInput = new OrderInput()
            {
                // Transactions = new List<Transaction>(),
                //// OrderTransactions = new List<OrderTransaction>(),
                Amount = 3,
                RequestRate = 11500,
                IsBuy = true,
                UserProfile = user
            };
            var order = OrderFactory.Generate(orderInput);
            var output = order.ExecuteTest(market.Orders);



            var r = output.CreatedOrder != null;
            Assert.False(r);

            // var totalTransaction = order.Transactions.Count;
            var totalTransaction = order.Transactions.Count;
            Assert.Equal(1, totalTransaction);


            // Assert.Equal(true, factory.order1.IsComplete);
            Assert.True(factory.order3.IsFillComplete);
            Assert.True(order.IsFillComplete);
        }


        [Fact]
        public void BuyOneAndHalfTwiceAndRunningBalance()
        {
            var factory = new FactoryTest();
            factory.IsResetIdDevMode = false;
            var market = factory.Generate();

            var user = new UserProfile();
            user.PopulateCurrency();

            {
                var orderInput = new OrderInput()
                {
                    // Transactions = new List<Transaction>(),
                    //// OrderTransactions = new List<OrderTransaction>(),
                    Amount = 4,
                    RequestRate = 11500,
                    IsBuy = true,
                    UserProfile = user
                };
                var order = OrderFactory.Generate(orderInput);
                var output = order.ExecuteTest(market.Orders);
                var r = output.CreatedOrder == null;
                Assert.True(r);

                //var transactions = order.Transactions.ToList();
                var transactions = order.Transactions.ToList();
                {
                    var transcation = transactions[0];
                    // var buyer = transcation.Detail.GetBuyer();
                    var seller = transcation.GetSellOrderFirst();
                    Assert.Equal(3, seller.Id);
                    Assert.Equal(0, seller.LeftAmount);
                }
                {
                    var transcation = transactions[1];
                    // var buyer = transcation.Detail.GetBuyer();
                    var seller = transcation.GetSellOrderFirst();
                    Assert.Equal(2, seller.Id);
                    Assert.Equal(1, seller.LeftAmount);
                }

                // var totalTransaction = order.Transactions.Count;
                var totalTransaction = order.Transactions.Count;
                Assert.Equal(2, totalTransaction);


                Assert.True(factory.order3.IsFillComplete);

                Assert.False(factory.order2.IsFillComplete);

                Assert.True(order.IsFillComplete);

                Assert.Equal(0, factory.order3.LeftAmount);
                Assert.Equal(1, factory.order2.LeftAmount);

            }
            {
                var orderInput = new OrderInput()
                {
                    // Transactions = new List<Transaction>(),
                    //// OrderTransactions = new List<OrderTransaction>(),
                    Amount = 4,
                    RequestRate = 11500,
                    IsBuy = true,
                    UserProfile = user
                };
                var order = OrderFactory.Generate(orderInput);

                var output = order.ExecuteTest(market.Orders);
                var r = output.CreatedOrder == null;
                Assert.True(r);


                // var totalTransaction = order.Transactions.Count;
                var totalTransaction = order.Transactions.Count;
                Assert.Equal(2, totalTransaction);

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
                    Assert.Equal(0, seller.LeftAmount);
                }





                Assert.True(factory.order1.IsFillComplete);
                Assert.True(factory.order2.IsFillComplete);
                Assert.True(factory.order3.IsFillComplete);

                Assert.True(order.IsFillComplete);

                Assert.Equal(0, factory.order1.LeftAmount);
                Assert.Equal(0, factory.order2.LeftAmount);
                Assert.Equal(0, factory.order3.LeftAmount);


              
            }

            var detail = user.GetUserProfileDetail("btc");
            var accountTransactions = detail.AccountTransactions.ToList();

        

            Assert.Equal(3, accountTransactions[0].RunningBalance);
            Assert.Equal(4, accountTransactions[1].RunningBalance);
            Assert.Equal(5, accountTransactions[2].RunningBalance);
            Assert.Equal(8, accountTransactions[3].RunningBalance);
            
        }



        [Fact]
        public void BuyOneAndHalf()
        {
            var factory = new FactoryTest();
            factory.IsResetIdDevMode = false;
            var market = factory.Generate();

            var user = new UserProfile();
            user.PopulateCurrency();

            var orderInput = new OrderInput()
            {
                // Transactions = new List<Transaction>(),
                // OrderTransactions = new List<OrderTransaction>(),
                Amount = 4,
                RequestRate = 11500,
                IsBuy = true,
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
                var seller = transcation.GetSellOrderFirst();
                Assert.Equal(3, seller.Id);
                Assert.Equal(0, seller.LeftAmount);

            }
            {
                var transcation = transactions[1];
                // var buyer = transcation.Detail.GetBuyer();
                var seller = transcation.GetSellOrderFirst();
                Assert.Equal(2, seller.Id);
                Assert.Equal(1, seller.LeftAmount);
            }

            // var totalTransaction = order.Transactions.Count;
            var totalTransaction = order.Transactions.Count;
            Assert.Equal(2, totalTransaction);


            Assert.True(factory.order3.IsFillComplete);

            Assert.False(factory.order2.IsFillComplete);

            Assert.True(condition: order.IsFillComplete);

            Assert.Equal(0, factory.order3.LeftAmount);
            Assert.Equal(1, factory.order2.LeftAmount);





        }

        [Fact]
        public void BuyHalf()
        {
            var factory = new FactoryTest();
            var market = factory.Generate();


            var user = new UserProfile();
            user.PopulateCurrency();



            var orderInput = new OrderInput()
            {
                // Transactions = new List<Transaction>(),
                // OrderTransactions = new List<OrderTransaction>(),
                Amount = 1,
                //LeftAmount = 1,
                RequestRate = 11500,
                IsBuy = true,
                UserProfile = user
            };
            var order = OrderFactory.Generate(orderInput);
            var output = order.ExecuteTest(market.Orders);
            var r = output.CreatedOrder != null;
            Assert.False(r);





            Assert.False(factory.order3.IsFillComplete);

            Assert.False(factory.order2.IsFillComplete);

            Assert.Equal(2, factory.order3.LeftAmount);
            Assert.Equal(2, factory.order2.LeftAmount);




            Assert.True(order.IsFillComplete);




        }


        [Fact]
        public void BuyHalfSecondItem()
        {
            var factory = new FactoryTest();
            factory.IsResetIdDevMode = false;
            var market = factory.Generate();

            var user = new UserProfile();
            user.PopulateCurrency();

            var orderInput = new OrderInput()
            {
                // Transactions = new List<Transaction>(),
                // OrderTransactions = new List<OrderTransaction>(),
                Amount = 1,
                //LeftAmount = 1,
                RequestRate = 11000,
                IsBuy = true,
                UserProfile = user
            };
            var order = OrderFactory.Generate(orderInput);
            var output = order.ExecuteTest(market.Orders);
            var r = output.CreatedOrder != null;
            Assert.False(r);





            Assert.False(factory.order3.IsFillComplete);

            Assert.False(factory.order2.IsFillComplete);


            Assert.Equal(2, factory.order3.LeftAmount);
            Assert.Equal(2, factory.order2.LeftAmount);





            Assert.True(order.IsFillComplete);




        }

        [Fact]
        public void BuyLower()
        {
            var factory = new FactoryTest();
            factory.IsResetIdDevMode = false;
            var market = factory.Generate();

            var user = new UserProfile();
            user.PopulateCurrency();

            var orderInput = new OrderInput()
            {
                // Transactions = new List<Transaction>(),
                // OrderTransactions = new List<OrderTransaction>(),
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



        }

        [Fact]
        public void BuyLowerTwice()
        {
            var factory = new FactoryTest();
            factory.IsResetIdDevMode = false;
            var market = factory.Generate();

            var user = new UserProfile();
            user.PopulateCurrency();

            {
                var orderInput = new OrderInput()
                {
                    // Transactions = new List<Transaction>(),
                    // OrderTransactions = new List<OrderTransaction>(),
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

            }


            {
                var orderInput = new OrderInput()
                {
                    // Transactions = new List<Transaction>(),
                    // OrderTransactions = new List<OrderTransaction>(),
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
                Assert.False(factory.order3.IsFillComplete);
                Assert.False(order.IsFillComplete);


                Assert.Equal(5, order.LeftAmount);

                Assert.Equal(3, factory.order1.LeftAmount);
                Assert.Equal(2, factory.order2.LeftAmount);
                Assert.Equal(3, factory.order3.LeftAmount);

            }

        }



    }
}