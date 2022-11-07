using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ExchangeLemonCore.Controllers
{
    public class DevStringController : Controller
    {
        private IConfiguration configuration;

        public DevStringController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // http://locahost:5000/devString/GetContent/12345
        public string GetContent(string id)
        {

            var logConnString = "";
            {
                var input = configuration.GetConnectionString("LoggingConnection");
                var items = input.Split(';');
                logConnString = items[0] + ";" + items[1];
            }


            var connString = "";
            {
                var input = configuration.GetConnectionString("DefaultConnection");
                var items = input.Split(';');
                connString = items[0] + ";" + items[1];
            }

            
            var output = $"{connString}\r\r{logConnString}";

            if (id == "12345")
            {
                return output;
            }
            else
            {
                return "nothing";
            }

        }
    }
}