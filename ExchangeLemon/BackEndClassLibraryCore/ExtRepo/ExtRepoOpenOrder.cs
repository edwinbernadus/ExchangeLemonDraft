//using System.Data.Entity;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
//using System.Web.Http;
//using System.Web.Mvc;
using Dapper;
using System.Linq;

namespace BlueLight.Main
{
    public class ExtRepoOpenOrder
    {
        // private IConfiguration configuration;

        private readonly DbConnGeneratorService _dbConnGeneratorService;

        public ExtRepoOpenOrder(DbConnGeneratorService dbConnGeneratorService)
        {
            _dbConnGeneratorService = dbConnGeneratorService;
        }


        public async Task<List<Order>> GetAllOpenOrders(long userId,
            string currencyPair)
        {

            await Task.Delay(0);
            string sQuery = "select * FROM Orders"
                    + " where CurrencyPair = @CurrencyPair and"
                    + " IsOpenOrder = @IsOpenOrder and"
                    + " UserProfileid  = @UserId";
                    //+ " Orderby CreatedDate desc";
            List<Order> output = null;
            using (var Connection = _dbConnGeneratorService.Generate())
            {
                var t = Connection.Query<Order>
                    (sQuery, new
                    {
                        CurrencyPair = currencyPair,
                        IsOpenOrder = 1,
                        UserId = userId
                    });
                var t2 = t.ToList();
                var t3 = t2.OrderByDescending(x => x.CreatedDate).ToList();
                output = t3;
            }
            
            return output;


        }
    }
}