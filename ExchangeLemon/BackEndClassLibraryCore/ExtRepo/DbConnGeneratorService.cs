//using System.Data.Entity;
//using System.Web.Http;
//using System.Web.Mvc;
//using System.Data.Entity;
using System.Data;
using System.Data.SqlClient;

using Microsoft.Extensions.Configuration;
//using System.Web.Http;
//using System.Web.Mvc;



namespace BlueLight.Main
{
    public class DbConnGeneratorService
    {
        private IConfiguration configuration;

        public DbConnGeneratorService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IDbConnection Generate()
        {


            var connectionString = configuration.GetConnectionString("DefaultConnection");
            return new SqlConnection(connectionString);

        }
    }
}
