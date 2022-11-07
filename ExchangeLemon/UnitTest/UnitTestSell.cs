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
    public class UnitTestSell
    {
        //[Fact]
        //public void Test1()
        //{
        //    var c = new Class1();
        //    var z = c.GetOne();
        //    Assert.Equal(4, z);
        //}


        [Fact]
        public void SellTwoFull()
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
                Amount = 3,
                RequestRate = 3000,
                IsBuy = false,
                UserProfile = user
            };
            var order = OrderFactory.Generate(orderInput);
            var output = order.ExecuteTest(market.Orders);
            var r = output.CreatedOrder == null;
            Assert.True(r);

            

            var transactions = order.Transactions.ToList();
            {
                var transcation = transactions[0];
                // var buyer = transcation.Detail.GetBuyer();
                var seller = transcation.GetBuyOrderFirst();
                Assert.Equal(0, seller.LeftAmount);
            }
            {
                var transcation = transactions[1];
                // var buyer = transcation.Detail.GetBuyer();
                var seller = transcation.GetBuyOrderFirst();
                Assert.Equal(0, seller.LeftAmount);
            }

            // var totalTransaction = order.Transactions.Count;
            var totalTransaction = order.Transactions.Count;
            Assert.Equal(2, totalTransaction);


            Assert.True(factory.order11.IsFillComplete);
            Assert.True(factory.order12.IsFillComplete);
            Assert.True(order.IsFillComplete);
        }


        [Fact]
        public void SellOneFull()
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
                Amount = 2,
                RequestRate = 4000,
                IsBuy = false,
                UserProfile = user
            };
            var order = OrderFactory.Generate(orderInput);
            var output = order.ExecuteTest(market.Orders);



            var r = output.CreatedOrder == null;
            Assert.True(r);

            // var totalTransaction = order.Transactions.Count;
            var totalTransaction = order.Transactions.Count;
            Assert.Equal(1, totalTransaction);


            Assert.True(factory.order11.IsFillComplete);
            Assert.True(order.IsFillComplete);
        }


        [Fact]
        public void SellOneAndHalfTwice()
        {
            var factory = new FactoryTest();
            factory.IsResetIdDevMode = false;

            var user = new UserProfile();
            user.PopulateCurrency();

            var market = factory.Generate();
            {
                var orderInput = new OrderInput()
                {
                    // Transactions = new List<Transaction>(),
                    //OrderTransactions = new List<OrderTransaction>(),
                    Amount = 2.5m,
                    RequestRate = 2000,
                    IsBuy = false,
                    UserProfile = user

                };
                var order = OrderFactory.Generate(orderInput);
                var output = order.ExecuteTest(market.Orders);
                var r = output.CreatedOrder == null;
                Assert.True(r);

                var transactions = order.Transactions.ToList();
                {
                    var transcation = transactions[0];
                    // var buyer = transcation.Detail.GetBuyer();
                    var seller = transcation.GetBuyOrderFirst();
                    Assert.Equal(11, seller.Id);
                    Assert.Equal(0, seller.LeftAmount);
                }
                {
                    var transcation = transactions[1];
                    // var buyer = transcation.Detail.GetBuyer();
                    var seller = transcation.GetBuyOrderFirst();
                    Assert.Equal(12, seller.Id);
                    Assert.Equal(0.5m, seller.LeftAmount);
                }

                // var totalTransaction = order.Transactions.Count;
                var totalTransaction = order.Transactions.Count;
                Assert.Equal(2, totalTransaction);


                Assert.True(factory.order11.IsFillComplete);

                Assert.False(factory.order12.IsFillComplete);

                Assert.True(order.IsFillComplete);

                Assert.Equal(0, factory.order11.LeftAmount);
                Assert.Equal(0.5m, factory.order12.LeftAmount);

            }
            {
                var orderInput = new OrderInput()
                {
                    // Transactions = new List<Transaction>(),
                    //OrderTransactions = new List<OrderTransaction>(),
                    Amount = 2.5m,
                    RequestRate = 2000,
                    IsBuy = false,
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
                    var seller = transcation.GetBuyOrderFirst();
                    Assert.Equal(12, seller.Id);
                    Assert.Equal(0, seller.LeftAmount);
                }
                {
                    var transcation = transactions[1];
                    // var buyer = transcation.Detail.GetBuyer();
                    var seller = transcation.GetBuyOrderFirst();
                    Assert.Equal(13, seller.Id);
                    Assert.Equal(0, seller.LeftAmount);
                }





                Assert.True(factory.order11.IsFillComplete);
                Assert.True(factory.order12.IsFillComplete);
                Assert.True(factory.order13.IsFillComplete);

                Assert.True(order.IsFillComplete);

                Assert.Equal(0, factory.order11.LeftAmount);
                Assert.Equal(0, factory.order12.LeftAmount);
                Assert.Equal(0, factory.order13.LeftAmount);

            }
        }



        [Fact]
        public void SellOneAndHalf()
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
                Amount = 2.5m,
                RequestRate = 2000,
                IsBuy = false,
                UserProfile = user
            };
            var order = OrderFactory.Generate(orderInput);
            var output = order.ExecuteTest(market.Orders);
            // var output = order.Sell(market.Orders);
            var r = output.CreatedOrder == null;
            Assert.True(r);


            var transactions = order.Transactions.ToList();
            {
                var transcation = transactions[0];
                // var buyer = transcation.Detail.GetBuyer();
                var seller = transcation.GetBuyOrderFirst();
                Assert.Equal(11, seller.Id);
                Assert.Equal(0, seller.LeftAmount);

            }
            {
                var transcation = transactions[1];
                // var buyer = transcation.Detail.GetBuyer();
                var seller = transcation.GetBuyOrderFirst();
                Assert.Equal(12, seller.Id);
                Assert.Equal(0.5m, seller.LeftAmount);
            }

            // var totalTransaction = order.Transactions.Count;
            var totalTransaction = order.Transactions.Count;
            Assert.Equal(2, totalTransaction);


            Assert.True(factory.order11.IsFillComplete);

            Assert.False(factory.order12.IsFillComplete);

            Assert.True(order.IsFillComplete);

            Assert.Equal(0, factory.order11.LeftAmount);
            Assert.Equal(0.5m, factory.order12.LeftAmount);





        }

        [Fact]
        public void SellHalf()
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
                Amount = 1,
                RequestRate = 2000,
                // IsBuy = true,
                IsBuy = false,
                UserProfile = user
            };
            var order = OrderFactory.Generate(orderInput);
            var output = order.ExecuteTest(market.Orders);
            var r = output.CreatedOrder == null;
            Assert.True(r);





            Assert.False(factory.order11.IsFillComplete);

            Assert.False(factory.order12.IsFillComplete);

            Assert.Equal(1, factory.order11.LeftAmount);
            Assert.Equal(1, factory.order12.LeftAmount);




            Assert.True(order.IsFillComplete);




        }


        [Fact]
        public void SellHalfSecondItem()
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
                Amount = 0.5m,
                RequestRate = 3000,
                IsBuy = false,
                UserProfile = user
            };
            var order = OrderFactory.Generate(orderInput);
            var output = order.ExecuteTest(market.Orders);
            var r = output.CreatedOrder == null;
            Assert.True(r);





            Assert.False(factory.order11.IsFillComplete);

            Order order12 = factory.order12;
            Assert.False(order12.IsFillComplete);


            Assert.Equal(1.5m, factory.order11.LeftAmount);
            Assert.Equal(1, factory.order12.LeftAmount);





            Assert.True(order.IsFillComplete);




        }

        [Fact]
        public void SellHigher()
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
                Amount = 3,
                RequestRate = 5000,
                IsBuy = false,
                UserProfile = user
            };
            var order = OrderFactory.Generate(orderInput);
            var output = order.ExecuteTest(market.Orders);
            var r = output.CreatedOrder == null;
            Assert.False(r);


            // 



            Assert.False(factory.order13.IsFillComplete);
            Assert.False(factory.order12.IsFillComplete);
            Assert.False(order.IsFillComplete);


            Assert.Equal(3, order.LeftAmount);

            Assert.Equal(2, factory.order13.LeftAmount);
            Assert.Equal(1, factory.order12.LeftAmount);



        }

        [Fact]
        public void SellHigherTwice()
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
                    ////OrderTransactions = new List<OrderTransaction>(),
                    Amount = 3,
                    RequestRate = 5000,
                    IsBuy = false,
                    UserProfile = user
                };
                var order = OrderFactory.Generate(orderInput);
                var output = order.ExecuteTest(market.Orders);
                var r = output.CreatedOrder == null;
                Assert.False(r);


                // 



                Assert.False(factory.order13.IsFillComplete);
                Assert.False(factory.order12.IsFillComplete);
                Assert.False(order.IsFillComplete);


                Assert.Equal(3, order.LeftAmount);

                Assert.Equal(2, factory.order13.LeftAmount);
                Assert.Equal(1, factory.order12.LeftAmount);

            }


            {
                var orderInput = new OrderInput()
                {
                    // Transactions = new List<Transaction>(),
                    //OrderTransactions = new List<OrderTransaction>(),
                    Amount = 3,
                    RequestRate = 5000,
                    IsBuy = false,
                    UserProfile = user
                };
                var order = OrderFactory.Generate(orderInput);
                var output = order.ExecuteTest(market.Orders);
                var r = output.CreatedOrder == null;
                Assert.False(r);


                // 



                Assert.False(factory.order13.IsFillComplete);
                Assert.False(factory.order12.IsFillComplete);
                Assert.False(factory.order11.IsFillComplete);
                Assert.False(order.IsFillComplete);


                Assert.Equal(3, order.LeftAmount);

                Assert.Equal(2, factory.order13.LeftAmount);
                Assert.Equal(1, factory.order12.LeftAmount);
                Assert.Equal(2, factory.order11.LeftAmount);

            }

        }
    }
}