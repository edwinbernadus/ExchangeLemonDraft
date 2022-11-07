using BlueLight.Main;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeLemonCore.Controllers
{
    public class DevRabbitController : Controller
    {
        // http://localhost:50727/devRabbit/init
        //public int Init()
        //{
        //    RpcBackEnd.InitBackend();
        //    return -1;
        //}

        // http://localhost:50727/devRabbit/testSend/20
        public string TestSend(int id)
        {
            var item  = id;
            var rpcClient = new RpcClient();
            string response = rpcClient.Call(item.ToString());
            //var c = new RpcClientTest();
            //var p = c.Execute(id);
            var p = response;
            return p;
        }


        
        // http://localhost:50727/devRabbit/testSendTwo/20
        public string TestSendTwo(int id)
        {
            var item  = id;
            var rpcClient = new RpcClient();
            string response = rpcClient.CallTransaction(item.ToString());
            //var c = new RpcClientTest();
            //var p = c.Execute(id);
            var p = response;
            return p;
        }
    }
}
