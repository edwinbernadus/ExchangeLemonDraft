using BlueLight.Main;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace ConsoleDev
{
    internal class LogicGenerateGraphData
    {
        private IServiceProvider serviceProvider;

        public LogicGenerateGraphData(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        internal async Task Execute()
        {
            var hours = RepoGraphHelper.GeneratePeriod(DateTime.Now, 24);
            var repoGraph = serviceProvider.GetService<RepoGraphExt>();

            var ctx = serviceProvider.GetService<GraphDbContext>();
            //var connectionString = ctx.Database.GetDbConnection().ConnectionString;
            var items2 = await repoGraph.GetItemsDraft();
            var items3 = await repoGraph.GetItemsDraft();

            //var items = await repoGraph.GetItems();
            //throw new NotImplementedException();
        }
    }
}