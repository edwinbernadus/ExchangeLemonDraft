using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlueLight.Main;
using ExchangeLemonCore;
using ExchangeLemonCore.Controllers;

namespace BackEndClassLibrary
{
    internal class DevReplayPlayerService : IReplayPlayerService
    {
        public Task ValidationCancelOrderAll()
        {
            return Task.Delay(0);
        }

        public Task ClearDb()
        {
            return Task.Delay(0);
        }

        public async Task<long> ExecuteFromTable()
        {
            await Task.Delay(0);
            return 0;
        }

      

        public Task Invoker(LogItemEventSource logItem)
        {
            return Task.Delay(0);
        }

        public async Task<Tuple<bool, List<UserProfileDetail>>> ValidationAvailableBalance()
        {
            await Task.Delay(0);
            return new Tuple<bool, List<UserProfileDetail>>(false,null);
        }

        public async Task<Tuple<bool, List<UserProfileDetail>>> ValidationHoldBalance()
        {
            await Task.Delay(0);
            return new Tuple<bool, List<UserProfileDetail>>(false,null);
        }

        public async Task<List<LogItemEventSource>> GetReplayItemsAsync()
        {
            await Task.Delay(0);
            return new List<LogItemEventSource>();
        }

        //public async Task<List<LogItem>> GetReplayItemsAsync()
        //{
        //    await Task.Delay(0);
        //    return new List<LogItem>();
        //}
    }
}