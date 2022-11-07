using System.Threading.Tasks;
using BlueLight.Main;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ExchangeLemonCore.Controllers
{
    public class DevEntityController : Controller
    {
        private readonly ApplicationContext _applicationContext;
        private readonly IConfiguration configuration;

        // private readonly LoggingContext _loggingContext;

        private readonly LoggingExtContext _loggingExtContext;

        public DevEntityController(ApplicationContext applicationContext,
            IConfiguration configuration,
            // LoggingContext loggingContext, 
            LoggingExtContext loggingExtContext)
        {
            _loggingExtContext = loggingExtContext;
            // _loggingContext = loggingContext;
            _applicationContext = applicationContext;
            this.configuration = configuration;
        }


        // http://localhost:5000/devEntity
        public async Task<string> Index()
        {
            var item1 = await this._applicationContext.Orders.CountAsync();
            // var item2 = await this._loggingContext..CountAsync();
            var item3 = await this._loggingExtContext.LogItems.CountAsync();

            var output = $"{item1} - {item3}";
            return output;
        }


        // http://localhost:5000/devEntity/test2
        public async Task<string> Test2()
        {
            var item1 = await this._applicationContext.Orders.CountAsync();
            var output = $"{item1}";
            return output;
        }


        // http://localhost:5000/devEntity/test1
        public async Task<string> Test1()
        {
            var item3 = await this._loggingExtContext.LogItems.CountAsync();

            var output = $"{item3}";
            return output;
        }


        // http://localhost:5000/devEntity/GetConnString
        public async Task<string> GetConnString()
        {
            var Configuration = this.configuration;
            var connString = Configuration.GetConnectionString("DefaultConnection");
            var logConnString = Configuration.GetConnectionString("LoggingConnection");

            var sourceConnString = Configuration.GetConnectionString("SourceConnection");
            
            var output = $"{connString} - {logConnString} - {sourceConnString}";
            return output;
        }
    }
}