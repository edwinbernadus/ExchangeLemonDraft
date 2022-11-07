using System;
using System.Collections.Generic;

namespace BotWalletWatcher
{
    public class MainParam
    {
        public static bool IsQueueDevMode = false;
        public static int DefaultLastBlockChainPosition = 1441538;
        
        

        public static string GetQueue(string queueName)
        {
            var queueNames = new Dictionary<string, bool>();
            queueNames.Add("receive", false);
            queueNames.Add("dev-receive", false);
            queueNames.Add("send", false);
            queueNames.Add("dev-send", false);
            var output = queueNames.ContainsKey(queueName);
            if (output)
            {
                return queueName;
            }
            else
            {
                throw new NotImplementedException();
            }
            
        }
        

        // public static string connString = "Data Source=localhost,32768; Database=CoffeeDb; user id =sa ; password= PasswordSuper; MultipleActiveResultSets=False;";
        //public static string connString = "Data Source=192.168.1.8; Database=WaterBearBlockDev; user id =edwin ; password= PasswordSuper; MultipleActiveResultSets=False;";
        // public static string connString = "Data Source=.; Initial Catalog=WaterBearLogDev; Integrated Security=True; MultipleActiveResultSets=False;";

    }
}