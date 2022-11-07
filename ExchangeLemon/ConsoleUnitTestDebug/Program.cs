using System;
using System.Threading.Tasks;
using BlueLight.Main.Tests;

namespace ConsoleUnitTestDebug
{
    class Program
    {
        static void Main(string[] args)
        {

            // c.Execute();
            NewMethod();

            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }

        private static System.Threading.Tasks.Task NewMethod()
        {
            // var c = new UnitTestSellThenBuyStepOne();
            // var output = c.ExecuteScenarioOne();

            // var c = new SellThenBuyStep1Scenario2();
            // var output = c.Execute();

            // var c = new SellThenBuyStep1Scenario3();
            // var output = c.Execute();

            // var c = new SellThenBuyStep1Scenario4();
            // var output = c.Execute();


            //var c = new UnitTestIncomingBtc();
            //var output = c.TestTwo();


            var c = new UnitTestOutgoingBtc();
            var output = c.TestConfirmZero();
            

            //var output = new Task(() =>
            //{

            //});
            return output;
        }
    }
}
