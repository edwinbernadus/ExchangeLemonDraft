using System.Collections.Generic;
using System.Linq;

namespace BlueLight.Main
{
    public class OrderItemMainHelper
    {

        static decimal startLimit = -0.00000005m;
        static decimal endLimit = 0.00000005m;
        public static bool IsValidGreyZoneNumber(decimal number1)
        {
            
            if (number1 >= startLimit && number1 <= endLimit)
            {
                return true;
            }

            return false;
        }

        public static decimal FilterGreyNumber(decimal number1)
        {
            if (number1 >= startLimit && number1 <= endLimit)
            {
                return number1;
            }
            else
            {
                var items = GenerateItems();
                if (items.ContainsKey(number1))
                {
                    var output = items[number1];
                    return output;
                }
                else
                {
                    return number1;
                }
            }

        }


        public static Dictionary<decimal, decimal> GenerateItems()
        {
            var  itemsOne = GenerateItemsPositive();
            var itemsTwo = GenerateItemsNegative();

            var GroupNames = new Dictionary<decimal, decimal>();
            GroupNames = GroupNames.Concat(itemsOne)
                       .ToDictionary(x => x.Key, x => x.Value);
            GroupNames = GroupNames.Concat(itemsTwo)
                       .ToDictionary(x => x.Key, x => x.Value);
            return GroupNames;
        }

        public static Dictionary<decimal,decimal> GenerateItemsPositive()
        {
            var output = new Dictionary<decimal, decimal>();
            const decimal diff = 0.00000001m;
            const decimal seperator = 0.00000005m;
            var startFrom = 0.00000025m;
            output[startFrom] = startFrom;

            var next = startFrom;
            var indicator = startFrom;

            var counter = 49 - 25;
            
            while (counter > 0)
            {
                if (counter % 5 == 0)
                {
                    indicator = indicator - seperator;
                }
                next = next - diff;
                output[next] = indicator;

             
                counter--;
            }

            return output;
            
        }

        public static Dictionary<decimal, decimal> GenerateItemsNegative()
        {
            var output = new Dictionary<decimal, decimal>();
            const decimal diff = 0.00000001m;
            const decimal seperator = 0.00000005m;
            var startFrom = -0.00000025m;
            output[startFrom] = startFrom;

            var next = startFrom;
            var indicator = startFrom;

            var counter = 49 - 25;

            while (counter > 0)
            {
                if (counter % 5 == 0)
                {
                    indicator = indicator + seperator;
                }
                next = next + diff;
                output[next] = indicator;


                counter--;
            }

            return output;

        }
      
    }
}