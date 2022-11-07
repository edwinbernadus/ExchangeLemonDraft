namespace BlueLight.Main
{
    public class PairHelper
    {
        public static string GetFirstPair(string currencyPair)
        {
            var currencyCode = currencyPair.Split('_')[0];
            return currencyCode;
        }

        public static string GetSecondPair(string currencyPair)
        {

            //var currencyCode = currencyPair.Split('_')[1];

            var items = currencyPair.Split('_');
            if (items.Length > 1)
            {
                var currencyCode = currencyPair.Split('_')[1];
                return currencyCode;
            }
            else
            {
                return "xxx";
            }

        }
    }

}