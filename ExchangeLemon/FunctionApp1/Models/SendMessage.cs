using System;
using System.Net.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using System.Threading.Tasks;

namespace FunctionApp1
{

    public class SendMessage
    {
        public int blockChainId { get; set; }
        public string transactionId { get; set; }
        public int confirmations { get; set; }
    }
}
