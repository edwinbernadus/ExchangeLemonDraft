using System;
using System.Threading.Tasks;
using BlueLight.Main;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleSignalPlayground
{
    class Program
    {


        static void Main(string[] args)
        {
            Console.WriteLine("start monitor btc transfer-ver5");
            NewMethod();
            Console.ReadLine();
        }

        private static Task NewMethod()
        {
            return Caller();
        }

        static async Task Caller()
        {
            try
            {
                await Execute();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static async Task Execute()
        {
            var factory = new Logic();
            await factory.Execute();

            //var t = new Test();
            //await t.Execute();
        }
    }
}

