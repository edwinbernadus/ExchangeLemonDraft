using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BlueLight.Main
{

    public class OrderItemMainFlag : IOrderItemMainFlag
    {
        string pair = "btc_usd";
        private readonly SignalDashboard _signalDashboard;
        public OrderItemMainFlag(SignalDashboard _signalDashboard)
        {
            this._signalDashboard = _signalDashboard;
        }


        static Dictionary<string, SemaphoreDetail> semaphoreDictionary =
        new Dictionary<string, SemaphoreDetail>();

        SemaphoreSlim GetSemaphore(string pair)
        {
            EnsureHasData(pair);
            var output = semaphoreDictionary[pair].semaphoreSlim;
            return output;
        }

        void EnsureHasData(string pair)
        {
            if (semaphoreDictionary.ContainsKey(pair) == false)
            {
                var input = new SemaphoreDetail()
                {
                    semaphoreSlim = new SemaphoreSlim(1, 1),
                    TotalFlag = 0
                };
                semaphoreDictionary[pair] = input;
            }
        }

        int AddFlag(string pair)
        {
            EnsureHasData(pair);
            semaphoreDictionary[pair].TotalFlag++;
            return semaphoreDictionary[pair].TotalFlag;

        }

        int RemoveFlag(string pair)
        {
            EnsureHasData(pair);
            semaphoreDictionary[pair].TotalFlag--;
            return semaphoreDictionary[pair].TotalFlag;
        }

        public async Task SemaphoreEnd()
        {
            //private async Task SemaphoreEnd()
            {
                if (FeatureRepo.UseSemaphore)
                {
                    var semaphoreSlim = GetSemaphore(this.pair);
                    semaphoreSlim.Release();
                    var TotalFlag = RemoveFlag(pair);
                    var message = $"SemaphoreEnd-Total:{TotalFlag}";

                    await _signalDashboard.Submit(message);
                }

                //await Task.Delay(0);
                //return 
                //throw new NotImplementedException();
            }
        }

        public async Task SemaphoreStart()
        {
            //private async Task SemaphoreStart()
            {
                if (FeatureRepo.UseSemaphore)
                {
                    var semaphoreSlim = GetSemaphore(this.pair);
                    await semaphoreSlim.WaitAsync();
                    var TotalFlag = AddFlag(pair);
                    var message = $"SemaphoreStart-Total:{TotalFlag}";
                    await _signalDashboard.Submit(message);
                }

                //return Task.Delay(0);
                //throw new NotImplementedException();
            }
        }
    }
}