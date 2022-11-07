using System;

namespace BlueLight.Main
{
    public class AmountCalculator
    {
        public static decimal Calc(decimal amount, decimal requestRate)
        {
            var output = amount * requestRate;
            //return output;
            decimal after1 = Math.Round(output, 8,
            MidpointRounding.ToEven); // Rounds "up"
            return after1;

        }
        //public static double Calc(double amount, double requestRate)
        //{
        //    var output = amount * requestRate;
        //    return output;
        //    //double after1 = Math.Round(output, 8,
        //    //MidpointRounding.AwayFromZero); // Rounds "up"
        //    //return after1;
        //}


        public static decimal CalcRoundSum(decimal amount, decimal requestRate)
        {
            var output = amount * requestRate;
            // return output;
            decimal after1 = Math.Round(output, 8,
            MidpointRounding.ToEven); // Rounds "up"
            return after1;
        }



        public static decimal CalcRound(decimal total,int precision = 8)
        {
            var output = total;
            // return output;
            decimal after1 = Math.Round(output, precision,
            //MidpointRounding.AwayFromZero); // Rounds "up"
            MidpointRounding.ToEven); // Rounds "up"
            return after1;
        }

        //public static double CalcFloat(float amount, float requestRate)
        //{
        //    var output = amount * requestRate;
        //    return (float)output;
        //}

        //public static decimal CalcDecimal(decimal amount, decimal requestRate)
        //{
        //    var output = amount * requestRate;
        //    return output;
        //}
    }
}