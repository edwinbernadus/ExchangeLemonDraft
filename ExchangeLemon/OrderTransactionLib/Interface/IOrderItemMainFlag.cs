using System.Threading.Tasks;

namespace BlueLight.Main
{
    public interface IOrderItemMainFlag
    {
        Task SemaphoreStart();
        Task SemaphoreEnd();
    }
}