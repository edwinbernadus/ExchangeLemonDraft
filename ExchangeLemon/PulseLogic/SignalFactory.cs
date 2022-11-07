
using LogLibrary;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace PulseLogic
{
    public class SignalFactory
    {
        public string Url { get; private set; }

        public HubConnection _hubConnection;

        public bool IsConnect { get; private set; }

        public HubConnection Generate(string url)
        {
            this.Url = url;
            _hubConnection = new HubConnectionBuilder()
            .WithUrl(url)
           .AddMessagePackProtocol()
           .Build();


            _hubConnection.Closed += _hubConnection_Closed;
            return _hubConnection;
        }


        public async Task Connect()
        {
            await LoopingReconnecting();
        }

        private async Task LoopingReconnecting()
        {
            while (IsConnect == false)
            {
                try
                {
                    Console3.WriteLine($"{Url} - Try connecting");
                    await _hubConnection.StartAsync();
                    IsConnect = true;
                    Console3.WriteLine($"{Url} - Connected");
                }
                catch (Exception)
                {
                    Console3.WriteLine($"{Url} - Failed try connecting");
                    Console3.WriteLine($"{Url} - Waiting for 5 seconds");
#if DEBUG
                    const int MillisecondsDelay = 1000;
#else
        const int MillisecondsDelay = 5000;
#endif

                    await Task.Delay(MillisecondsDelay);
                }
            }
        }

        private async Task _hubConnection_Closed(Exception arg)
        {
            IsConnect = false;
            await LoopingReconnecting();
            //while (IsConnect == false)
            //{
            //    Console3.WriteLine($"Connection lost, waiting for 5 seconds");
            //    await Task.Delay(5000);
            //    Console3.WriteLine($"Try connecting");
            //    try
            //    {
            //        await _hubConnection.StartAsync();
            //        IsConnect = true;
            //        Console3.WriteLine($"Connected");
            //    }
            //    catch (Exception ex)
            //    {

            //        Console3.WriteLine($"Failed try connecting");
            //    }

            //}
        }
    }



}
