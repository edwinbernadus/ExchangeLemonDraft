using System;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BittrexBalancesSchema;
using BlueLight.Main;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace BlueLight.Main
{
    public class DbLogHelperMvc : ILogHelperMvc
    {

        private LoggingContext context;

        public DbLogHelperMvc(LoggingContext context)
        {
            this.context = context;
        }

        public async Task SaveLog(object output, HttpRequest request,
            [CallerMemberName] string callerName = "")
        {
            await Task.Delay(0);
            // //var context = new ApplicationContext ();

            // var content = JsonConvert.SerializeObject(output);

            // var logMvc = new LogMvc()
            // {
            //     CallerName = callerName,
            //     Content = content,
            // };
            // logMvc.Populate(request);

            // if (ParamRepo.IsSaveLogEnable)
            // {
            //     context.LogMvcs.Add(logMvc);
            //     await context.SaveChangesAsync();
            // }

        }

        public async Task SaveError(object output, HttpRequest request,
            Exception ex, [CallerMemberName] string callerName = "")
        {
            await Task.Delay(0);


            // var content = JsonConvert.SerializeObject(output);

            // var logMvc = new LogMvc()
            // {
            //     CallerName = callerName,
            //     Content = content,
            // };
            // logMvc.Populate(request, ex);

            // if (ParamRepo.IsSaveLogEnable)
            // {
            //     context.LogMvcs.Add(logMvc);
            //     await context.SaveChangesAsync();
            // }

        }


    }


}