using System;
using System.Threading.Tasks;
using BlueLight.Main;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleSignalPlayground
{

    class Test
    {
        public async Task Execute()
        {
            var service = DependencySendBtcHelper.GenerateService();
            var s = service.GetService<IBtcServiceSendMoney>();
            var s2 = await s.SendMoney("muMtCU1BG9Bnf3XAFiLFLGAdKh1T7PqUNL", 800);
        }
        
    }
}

