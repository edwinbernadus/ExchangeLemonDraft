using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using BlueLight.Main;
using Newtonsoft.Json;
using System.IO;
using ExchangeLemonCore.Controllers;
using ExchangeLemonCore;
//using PCLStorage;

namespace DebugWorkplace
{
    public class ReplayFileService : IReplayFileService
    {
        public IFileService fileService { get; private set; }
        public ReplayFileService (IFileService fileService)
        {
            this.fileService = fileService;
        }
        string fileName = "sample.json";

        public async Task<List<LogItemEventSource>> LoadItems()
        {
            var content = await LoadFromFileAsync();
            var output = JsonConvert.DeserializeObject<List<LogItemEventSource>>(content);
            return output;
        }

        public async Task<string> LoadFromFileAsync()
        {
            var content = await fileService.Load(fileName);
            return content;
        }

        public async Task SaveToFileAsync(List<LogItemEventSource> items)
        {
            var jsonContent = JsonConvert.SerializeObject(items);
            await fileService.Save(fileName, jsonContent);
        }
    }
}