using System;
using System.Threading.Tasks;

namespace BlueLight.Main
{
    public interface ILogMatchService
    {
        Task StartAsync();
        Task CaptureAsync(string content, TimeSpan t);
        Task SettingAsync(string mode);
    }
}
