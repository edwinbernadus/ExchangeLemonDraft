using System.Threading.Tasks;

namespace BlueLight.Main
{
    public interface IOrderService
    {
        Task CreateNew();
        Task Init(Order inputOrder, string currencyPair);
        Task DealOrder(Order oppositeOrder);
    }
}