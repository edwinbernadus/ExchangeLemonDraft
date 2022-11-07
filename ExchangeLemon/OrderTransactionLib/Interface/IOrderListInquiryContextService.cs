using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlueLight.Main
{
    public interface IOrderListInquiryContextService
    {
        Task<List<Order>> GetItemsBuy(int take, string currentPair);
        Task<List<Order>> GetItemsKirin(string currentPair);
        Task<List<Order>> GetItemsSell(int take, string currentPair);
        Task ClearCache(string pair);
    }
}