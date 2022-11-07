using System.Threading.Tasks;

namespace BlueLight.Main
{
    public class FakeContextSaveService : IContextSaveService
    {
 
        public async Task ExecuteAsync()
        {
            await Task.Delay(0);
        }

  
    }
}