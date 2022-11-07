//using ExchangeLemonCore.Data;
using Microsoft.EntityFrameworkCore;

namespace BlueLight.Main
{
    public class GraphDbContext : DbContext 
    {
        public GraphDbContext(DbContextOptions<GraphDbContext> options) : base(options)
        {

        }


        //public DbSet<GraphPlainData> GraphPlainDatas { get; set; }
        public DbSet<GraphPlainDataExt> GraphPlainDataExts { get; set; }
        
    }

}
