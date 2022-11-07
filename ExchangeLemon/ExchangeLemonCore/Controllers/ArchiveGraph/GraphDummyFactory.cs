//using System;
//using System.Collections.Generic;
//using System.Linq;
//using BlueLight.Main;

//namespace ExchangeLemonCore.Controllers
//{
//    public class GraphDummyFactory
//    {

//        public static Random random = new Random();
//        public static int lastRate = -1;

//        public static List<GraphPlainData> GenerateDummySetTwo()
//        {

//            var output = new List<GraphPlainData>();
//            var numbers = Enumerable.Range(0, 25).ToList();

//            var lastRate = 7500;
//            foreach (var i in numbers)
//            {
//                var date = DateTime.Now.AddMinutes(-1 * i);
//                var nextNumber = random.Next(-25, 25);
//                lastRate = lastRate + nextNumber;

//                var item = new GraphPlainData()
//                {
//                    Value = lastRate,
//                    DateTime = date,
//                    Sequence = i
//                };

//                output.Add(item);
//            }
//            return output;
//        }

//        public static List<GraphPlainData> GenerateDummySetOne()
//        {
//            var items = new List<GraphPlainData>();
//            items.Add(new GraphPlainData()
//            {
//                DateTime = DateTime.Now,
//                Value = 5,
//                Sequence = 1
//            });
//            items.Add(new GraphPlainData()
//            {
//                DateTime = DateTime.Now.AddDays(-1),
//                Value = 7,
//                Sequence = 2

//            });
//            items.Add(new GraphPlainData()
//            {
//                DateTime = DateTime.Now.AddDays(-2),
//                Value = 2,
//                Sequence = 3

//            });
//            return items;
//        }
//    }
//}