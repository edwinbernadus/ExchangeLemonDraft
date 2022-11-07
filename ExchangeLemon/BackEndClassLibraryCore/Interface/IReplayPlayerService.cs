using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlueLight.Main;

namespace ExchangeLemonCore
{
    public interface IReplayPlayerService
    {
        
        Task ClearDb();
        Task<long> ExecuteFromTable();
        Task<List<LogItemEventSource>> GetReplayItemsAsync();
        Task Invoker(LogItemEventSource logItem);
        Task<Tuple<bool, List<UserProfileDetail>>> ValidationAvailableBalance();
        Task<Tuple<bool, List<UserProfileDetail>>> ValidationHoldBalance();
    }
}