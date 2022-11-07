using System.Threading.Tasks;

namespace BlueLight.Main
{
    public interface ILogHubService
    {
        Task SendMessage(string content);
    }
}