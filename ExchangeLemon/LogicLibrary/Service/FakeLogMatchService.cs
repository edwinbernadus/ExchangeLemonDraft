using System;
using System.Threading.Tasks;

namespace BlueLight.Main
{
    public class FakeLogMatchService : ILogMatchService
    {
        public Task CaptureAsync(string v, TimeSpan t)
        {
            return Task.Delay(0);
        }

        public Task SettingAsync(string mode)
        {
            return Task.Delay(0);
        }

        public Task StartAsync()
        {
            return Task.Delay(0);
        }
    }
}