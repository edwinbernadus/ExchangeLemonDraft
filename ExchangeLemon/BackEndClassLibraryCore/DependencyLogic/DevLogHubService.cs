using System.Threading.Tasks;
using BlueLight.Main;
public class DevLogHubService : ILogHubService
{
    public Task SendMessage(string content)
    {
        return Task.Delay(0);
    }
}