using System.Collections.Generic;
using System.Linq;

namespace BlueLight.Main
{



    public class FactoryTest
    {

        public UserProfile user;
        public Order order1;
        public Order order2;
        public Order order3;


        public bool IsResetIdDevMode { get; set; } = true;

        public Order order11;
        public Order order12;
        public Order order13;



        public bool UseCustomUser { get; set; }

        

        public Market Generate()
        {
            if (UseCustomUser == false)
            {
                user = new UserProfile();
                user.PopulateCurrency();
                user.username = "woot";
                
            }




            var market = new Market()
            {
                Orders = new List<Order>()
            };

            // sell
            // id 1 - 3 - 11500
            // id 2 - 2 - 11000
            // id 3 - 3 - 10000
            {
                var orderInput1 = new OrderInput()
                {
                    Id = 1,
                    Amount = 3,
                    //LeftAmount = 3,
                    IsBuy = false,
                    RequestRate = 11500,
                    // Transactions = new List<Transaction>(),
                    // OrderTransactions = new List<OrderTransaction>(),
                    UserProfile = user,
                    //Transactions = new List<Transaction>(),

                };
                this.order1 = OrderFactory.Generate(orderInput1);

                market.Orders.Add(order1);
                //order1.ExecuteTest(market.Orders);

                var orders1 = market.Orders;
                var orders2 = orders1.AsQueryable();


                var orderLogic = FakeServiceFactory.GenerateTransactionService(orders2);
                orderLogic.ExecuteSyncTest(order1, skipBalanceNegativeValidation:true);
                // order1.Execute(orders2, GlobalTestParam.TestCurrency);
            }


            {
                var orderInput2 = new OrderInput()
                {
                    Id = 2,
                    Amount = 2,
                    //LeftAmount = 2,
                    IsBuy = false,
                    RequestRate = 11000,
                    // Transactions = new List<Transaction>(),
                    // OrderTransactions = new List<OrderTransaction>(),
                    UserProfile = user,
                    //Transactions = new List<Transaction>(),
                };
                this.order2 = OrderFactory.Generate(orderInput2);

                market.Orders.Add(order2);
                order2.ExecuteTest(market.Orders);
            }


            {
                var orderInput3 = new OrderInput()
                {
                    Id = 3,
                    Amount = 3,
                    //LeftAmount = 3,
                    IsBuy = false,
                    RequestRate = 10000,
                    // Transactions = new List<Transaction>(),
                    // OrderTransactions = new List<OrderTransaction>(),
                    UserProfile = user,
                    //Transactions = new List<Transaction>(),
                };
                //Transactions = new List<Transaction>(),

                this.order3 = OrderFactory.Generate(orderInput3);
                market.Orders.Add(order3);
                order3.ExecuteTest(market.Orders);
            }



            // buy
            // id 11 - 2 - 4000
            // id 12 - 1 - 3000
            // id 13 - 2 - 2000
            {
                var orderInput11 = new OrderInput()
                {
                    Id = 11,
                    Amount = 2,
                    //LeftAmount = 2,
                    IsBuy = true,
                    RequestRate = 4000,
                    // Transactions = new List<Transaction>(),
                    // OrderTransactions = new List<OrderTransaction>(),
                    UserProfile = user,
                    //Transactions = new List<Transaction>(),
                };
                this.order11 = OrderFactory.Generate(orderInput11);
                market.Orders.Add(order11);
                order11.ExecuteTest(market.Orders);
            }


            {
                var orderInput12 = new OrderInput()
                {
                    Id = 12,
                    Amount = 1,
                    //LeftAmount = 1,
                    IsBuy = true,
                    RequestRate = 3000,
                    // Transactions = new List<Transaction>(),
                    // OrderTransactions = new List<OrderTransaction>(),
                    UserProfile = user,
                    //Transactions = new List<Transaction>(),
                };
                this.order12 = OrderFactory.Generate(orderInput12);

                market.Orders.Add(order12);
                order12.ExecuteTest(market.Orders);
            }


            {
                var orderInput13 = new OrderInput()
                {
                    Id = 13,
                    Amount = 2,
                    //LeftAmount = 2,
                    IsBuy = true,
                    RequestRate = 2000,
                    // Transactions = new List<Transaction>(),
                    // OrderTransactions = new List<OrderTransaction>(),
                    UserProfile = user,
                    //Transactions = new List<Transaction>(),
                };
                this.order13 = OrderFactory.Generate(orderInput13);
                market.Orders.Add(order13);
                order13.ExecuteTest(market.Orders);
            }


            if (this.IsResetIdDevMode)
            {
                ResetId();
            }

            return market;
        }
        public Market GenerateSampleBuyer()
        {
            user = new UserProfile();
            user.PopulateCurrency();
            user.username = "buyer";
            user.AddBalanceTesting("idr", 14000);

            // buy
            // id 11 - 2 - 4000
            // id 12 - 2 - 3000

            var market = new Market()
            {
                Orders = new List<Order>()
            };

            {

                var orderInput11 = new OrderInput()
                {
                    Id = 11,
                    Amount = 2,
                    IsBuy = true,
                    RequestRate = 4000,
                    // Transactions = new List<Transaction>(),
                    // OrderTransactions = new List<OrderTransaction>(),
                    UserProfile = user,
                    //Transactions = new List<Transaction>(),
                };
                this.order11 = OrderFactory.Generate(orderInput11);
                market.Orders.Add(order11);
                order11.ExecuteTest(market.Orders);
            }


            {
                var orderInput12 = new OrderInput()
                {
                    Id = 12,
                    Amount = 2,
                    IsBuy = true,
                    RequestRate = 3000,
                    // Transactions = new List<Transaction>(),
                    // OrderTransactions = new List<OrderTransaction>(),
                    UserProfile = user,
                    //Transactions = new List<Transaction>(),
                };
                this.order12 = OrderFactory.Generate(orderInput12);
                market.Orders.Add(order12);
                order12.ExecuteTest(market.Orders);
            }





            if (this.IsResetIdDevMode)
            {
                ResetId();
            }

            return market;
        }

        public Market GenerateSampleSales()
        {
            user = new UserProfile();
            user.PopulateCurrency();
            user.username = "seller";
            user.AddBalanceTesting("btc", 5);

            // 1 - 3 , 11500
            // 2 - 2 , 11000

            var market = new Market()
            {
                Orders = new List<Order>()
            };

            {
                var orderInput1 = new OrderInput()
                {
                    Id = 1,
                    Amount = 3,
                    IsBuy = false,
                    RequestRate = 11500,
                    // Transactions = new List<Transaction>(),
                    // OrderTransactions = new List<OrderTransaction>(),
                    UserProfile = user,
                    //Transactions = new List<Transaction>(),

                };
                this.order1 = OrderFactory.Generate(orderInput1);
                market.Orders.Add(order1);
                order1.ExecuteTest(market.Orders);
            }


            {
                var orderInput2 = new OrderInput()
                {
                    Id = 2,
                    Amount = 2,
                    IsBuy = false,
                    RequestRate = 11000,
                    // Transactions = new List<Transaction>(),
                    // OrderTransactions = new List<OrderTransaction>(),
                    UserProfile = user,
                    //Transactions = new List<Transaction>(),
                };
                this.order2 = OrderFactory.Generate(orderInput2);
                market.Orders.Add(order2);
                order2.ExecuteTest(market.Orders);
            }





            if (this.IsResetIdDevMode)
            {
                ResetId();
            }

            return market;
        }

        private void ResetId()
        {
            order1.Id = 0;
            order2.Id = 0;
            order3.Id = 0;


            order11.Id = 0;
            order12.Id = 0;
            order13.Id = 0;
        }
    }
}
