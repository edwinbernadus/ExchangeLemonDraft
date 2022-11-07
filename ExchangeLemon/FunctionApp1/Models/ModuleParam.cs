using System;
using System.Net.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using System.Threading.Tasks;

namespace FunctionApp1
{


    public class ModuleParam
    {
        public static string GetHostName()
        {

#if DEBUG
            string hostName = "http://win8.southeastasia.cloudapp.azure.com";
            // string hostName = "http://localhost:53252";

#else
        string hostName = "http://win8.southeastasia.cloudapp.azure.com";
#endif
            return hostName;
        }
    }
}
