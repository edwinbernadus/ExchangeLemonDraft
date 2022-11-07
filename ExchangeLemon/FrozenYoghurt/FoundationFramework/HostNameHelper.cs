using System.IO;
using System.Linq;

namespace BlueLight.Main
{

    public static class HostNameHelper
    {
        static string hostname;

        static bool IsValid(string input0)
        {
            var input = input0;
            input = input ?? "";
            input = input.Trim();
            var output = string.IsNullOrEmpty(input) == false;
            return output;
        }




        public static string GetHostName()
        {
            if (hostname == null)
            {
                var fileName = "hostname.config";
                var f2 = File.ReadAllLines(fileName);
                var f3 = f2.ToList()
                    .Where(x => IsValid(x))
                    .First();
                hostname = f3.Trim();
            }

            return hostname;

        }
    }
}
