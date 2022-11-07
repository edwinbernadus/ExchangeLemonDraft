using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ExchangeLemonCore.Controllers
{
    public class Console2
    {
        public static void WriteLine(string input)
        {
            Console.WriteLine(input);
            Debug.WriteLine(input);
        }

        public static async Task WriteLineAsync(string input)
        {
            await Task.Delay(0);
            Console.WriteLine(input);
            Debug.WriteLine(input);
        }
    }
}