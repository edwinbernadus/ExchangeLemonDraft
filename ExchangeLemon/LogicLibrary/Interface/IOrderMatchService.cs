using System.Linq;
using System.Threading.Tasks;

namespace BlueLight.Main
{
    public interface IOrderMatchService
    {
        Task<Order> SellMatchMaker(Order order);
        Task<Order> BuyMatchMaker(Order order);
    }
}
