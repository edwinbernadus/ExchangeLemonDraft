//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using BackEndClassLibrary;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;
//using Xunit;

//namespace BlueLight.Main.Tests
//{
//    public class UnitTestGraph
//    {


//        [Fact]
//        public void TestOne()
//        {
//            var serviceProvider = DependencyHelper.GenerateServiceProviderForTesting();
//            var repoGraph = serviceProvider.GetService<RepoGraph>();

//            var items = new List<GraphPlainDataExt>();

//            {
//                var dateInput = new DateTime(2012, 12, 10, 22, 20, 0);
//                items.Add(new GraphPlainDataExt()
//                {
//                    DateTimeInput = dateInput,
//                    Value = 10
//                });
//            }
//            {
//                var dateInput = new DateTime(2012, 12, 10, 22, 19, 0);
//                items.Add(new GraphPlainDataExt()
//                {
//                    DateTimeInput = dateInput,
//                    Value = -1
//                });
//            }
//            {
//                var dateInput = new DateTime(2012, 12, 10, 22, 18, 0);
//                items.Add(new GraphPlainDataExt()
//                {
//                    DateTimeInput = dateInput,
//                    Value = 8
//                });
//            }
//            {
//                var dateInput = new DateTime(2012, 12, 10, 22, 17, 0);
//                items.Add(new GraphPlainDataExt()
//                {
//                    DateTimeInput = dateInput,
//                    Value = -1
//                });
//            }
            
//            var prevTotal = items.Sum(x => x.Value);
//            Assert.Equal(16, prevTotal);
            
//            repoGraph.PopulateDataFromPrevious(items);

//            var total = items.Sum(x => x.Value);
//            Assert.Equal(36, total);


//        }
        
        
//        [Fact]
//        public async Task TestTwo()
//        {
//            var serviceProvider = DependencyHelper.GenerateServiceProviderForTesting();

//            var currencyPair = "btc_usd";
//            var context = serviceProvider.GetService<ApplicationContext>();
//            context.Transactions.Add(new Transaction()
//            {
//                CurrencyPair  = currencyPair,
//                TransactionDate = new DateTime(2012, 12, 10, 22, 18, 33),
//                TransactionRate =  10
//            });
            
//            context.Transactions.Add(new Transaction()
//            {
//                CurrencyPair  = currencyPair,
//                TransactionDate= new DateTime(2012, 12, 10, 22, 16, 33),
//                TransactionRate=  8
//            });

//            await context.SaveChangesAsync();
            
//            var repoGraph = serviceProvider.GetService<RepoGraph>();

//            var items = new List<GraphPlainDataExt>();

//            {
//                var dateInput = new DateTime(2012, 12, 10, 22, 20, 0);
//                items.Add(new GraphPlainDataExt()
//                {
//                    DateTimeInput = dateInput,
//                    Value = 10
//                });
//            }
//            {
//                var dateInput = new DateTime(2012, 12, 10, 22, 19, 0);
//                items.Add(new GraphPlainDataExt()
//                {
//                    DateTimeInput = dateInput,
//                    Value = -1
//                });
//            }
//            {
//                var dateInput = new DateTime(2012, 12, 10, 22, 18, 0);
//                items.Add(new GraphPlainDataExt()
//                {
//                    DateTimeInput = dateInput,
//                    Value = 8
//                });
//            }
//            {
//                var dateInput = new DateTime(2012, 12, 10, 22, 17, 0);
//                items.Add(new GraphPlainDataExt()
//                {
//                    DateTimeInput = dateInput,
//                    Value = -1
//                });
//            }
            
//            var prevTotal = items.Sum(x => x.Value);
//            Assert.Equal(16, prevTotal);
            
//            await repoGraph.EnsureGetDataFromPersistant(items);

//            var total = items.Sum(x => x.Value);
//            Assert.Equal(36, total);


//            var context2 = serviceProvider.GetService<GraphDbContext>();
//            var total2 = await context2.GraphPlainDataExts.CountAsync();
//            Assert.Equal(2, total2);

//        }


//        [Fact]
//        public void TestThree()
//        {
//            var serviceProvider = DependencyHelper.GenerateServiceProviderForTesting();
//            var repoGraph = serviceProvider.GetService<RepoGraph>();

//            var items = new List<GraphPlainDataExt>();

//            {
//                var dateInput = new DateTime(2012, 12, 10, 22, 20, 0);
//                items.Add(new GraphPlainDataExt()
//                {
//                    DateTimeInput = dateInput,
//                    Value = 10
//                });
//            }
//            {
//                var dateInput = new DateTime(2012, 12, 10, 22, 19, 0);
//                items.Add(new GraphPlainDataExt()
//                {
//                    DateTimeInput = dateInput,
//                    Value = -1
//                });
//            }
//            {
//                var dateInput = new DateTime(2012, 12, 10, 22, 18, 0);
//                items.Add(new GraphPlainDataExt()
//                {
//                    DateTimeInput = dateInput,
//                    Value = 8
//                });
//            }
//            {
//                var dateInput = new DateTime(2012, 12, 10, 22, 17, 0);
//                items.Add(new GraphPlainDataExt()
//                {
//                    DateTimeInput = dateInput,
//                    Value = -1
//                });
//            }

//            {
//                var dateInput = new DateTime(2012, 12, 10, 22, 16, 0);
//                items.Add(new GraphPlainDataExt()
//                {
//                    DateTimeInput = dateInput,
//                    Value = -1
//                });
//            }

//            {
//                var dateInput = new DateTime(2012, 12, 10, 22, 15, 0);
//                items.Add(new GraphPlainDataExt()
//                {
//                    DateTimeInput = dateInput,
//                    Value = -1
//                });
//            }


//            {
//                var dateInput = new DateTime(2012, 12, 10, 22, 14, 0);
//                items.Add(new GraphPlainDataExt()
//                {
//                    DateTimeInput = dateInput,
//                    Value = -1
//                });
//            }

//            var prevTotal = items.Sum(x => x.Value);
//            Assert.Equal(13, prevTotal);

//            repoGraph.PopulateDataFromPrevious(items);

//            var total = items.Where(x => x.Value == -1).Count();
//            Assert.Equal(0, total);


//        }
//    }
//}