
using LogLibrary;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace PulseLogic
{

    public class PulseService {

#if DEBUG
        static int totalDelaySeconds = 1;
#else
        static int totalDelaySeconds = 5;
#endif


        SignalFactory factory = new SignalFactory();
        public string hostName;

        HubConnection _hubConnection
        {
            get
            {
                return factory._hubConnection;
            }
        }
        public async Task InitAsync(string hostName)
        {
            
            string url = $"{hostName}/signal/pulse";
            Console3.WriteLine($"signal pulse url: {url}");
            this.hostName = url;


            factory.Generate(url);

            await factory.Connect();
        }

        public  async void ExecuteLooping(string moduleName)
        {
        
            var isContinue = true;

            while (isContinue)
            {
                await Send(moduleName);
                
                await Task.Delay(1000 * totalDelaySeconds);
            }

        }

        public async Task Send(string moduleName)
        {
            
            try
            {
                await _hubConnection.SendAsync("registerPulse", moduleName);
                //Console3.WriteLine($"register pulse: {moduleName} - {DateTime.Now.ToLongTimeString()}");
            }
            catch (Exception)
            {
            }
        }

       
    }



}
