using System.Collections.Generic;
using System.Threading.Tasks;
using BlueLight.Main;

namespace ExchangeLemonCore
{
    public interface IReplayFileService
    {
        IFileService fileService { get; }

        Task<string> LoadFromFileAsync();
        Task<List<LogItemEventSource>> LoadItems();
        Task SaveToFileAsync(List<LogItemEventSource> items);
    }
}