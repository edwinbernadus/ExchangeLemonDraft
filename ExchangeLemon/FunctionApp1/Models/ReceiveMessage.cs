using System;
using System.Net.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using System.Threading.Tasks;

namespace FunctionApp1
{

    public class ReceiveMessage
    {
        public string address { get; set; }

        public int blockChainId { get; set; }

    }
}
