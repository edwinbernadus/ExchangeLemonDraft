using System.Threading.Tasks;
using BlueLight.Main;
using ExchangeLemonCore;
using ExchangeLemonCore.Controllers;

namespace BackEndClassLibrary
{
    internal class DevReplayValidationService : IReplayValidationService
    {
        public void FindDiff(ReplayValidationItem beforeItems, ReplayValidationItem afterItems)
        {

        }
        public async Task<ReplayValidationItem> CaptureItems()
        {
            await Task.Delay(0);
            var item = new ReplayValidationItem();
            return item;
        }

        public Task Execute(LogItem logItem = null, long counter = -1)
        {
            throw new System.NotImplementedException();
        }

        public bool IsValidLogic(ReplayValidationItem before, ReplayValidationItem after)
        {
            return true;
        }
    }
}