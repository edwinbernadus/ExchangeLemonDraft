using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BlueLight.Main
{
    public class ContextSaveService : IContextSaveService
    {
        private ApplicationContext _context;

        public ContextSaveService(ApplicationContext context)
        {
            _context = context;
        }

     

 
        public async Task ExecuteAsync()
        {
            _context.EnsureAutoHistory();
            await _context.SaveChangesAsync();
        }

    
    }
}