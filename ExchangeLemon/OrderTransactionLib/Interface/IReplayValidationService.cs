using System.Threading.Tasks;
using BlueLight.Main;

namespace ExchangeLemonCore.Controllers
{
    public interface IReplayValidationService
    {
        Task<ReplayValidationItem> CaptureItems();
        Task Execute(LogItem logItem = null, long counter = -1);
        bool IsValidLogic(ReplayValidationItem before, ReplayValidationItem after);
        void FindDiff(ReplayValidationItem beforeItems, ReplayValidationItem afterItems);
    }
}