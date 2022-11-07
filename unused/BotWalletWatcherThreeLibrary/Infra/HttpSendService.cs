using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BotWalletWatcherLibrary;
using Microsoft.EntityFrameworkCore;

namespace BotWalletWatcher
{
    public class HttpSendService
    {
        public async Task<string> Execute(string url) 
        {
            var httpClient = new HttpClient();
            var result = await httpClient.GetStringAsync(url);
            return result;
        }

        internal async Task ExecutePost(string url, string content)
        {
            var httpClient = new HttpClient();
            var s = new StringContent(content);
            await httpClient.PostAsync(url,s);
            
        }
    }
}
