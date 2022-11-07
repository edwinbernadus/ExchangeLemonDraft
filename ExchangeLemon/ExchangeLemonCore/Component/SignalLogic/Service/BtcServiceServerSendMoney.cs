using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace BlueLight.Main
{
    public class BtcServiceServerSendMoney : IBtcServiceSendMoney
    {
        public BtcServiceServerSendMoney(IHubContext<WatcherHub> watcherHubContext)
        {
            WatcherHubContext = watcherHubContext;
        }

        public IHubContext<WatcherHub> WatcherHubContext { get; }

        
        public async Task<string> SendMoney(string address, long amountInSatoshi)
        {
            WatcherHub.ResetTcs();
            await WatcherHubContext.Clients.All.SendAsync("sendTransfer", address, amountInSatoshi);
            
            //var task = WatcherHub.tcs.Task;


            //int timeout = 1000 * 5;
            var task = WatcherHub.tcs.Task;
            await task;
            var r = task.Result;
            WatcherHub.ResetTcs();

            return r;
            //if (await Task.WhenAny(task, Task.Delay(timeout)) == task)
            //{
            //    // task completed within timeout

            //    await task;
            //    var r = task.Result;
            //    WatcherHub.ResetTcs();




            //    return r;
            //}
            //else
            //{
            //    return "time-out";
            //    // timeout logic
            //}



        }
    }
}



    //    public async Task<string> SendMoney(string sourcePrivateKey,
    // string targetAddress,
    // long amountSatoshi,
    // long amountFeeSatoshi)
    //    {
    //        var input = (decimal)amountSatoshi / (decimal)100000000;
    //        var result = await BitcoinLib.Services.CoinService.SendToAddress(targetAddress, input, "", "", subtractFeeFromAmount: false);

//        return resullt;
//    }