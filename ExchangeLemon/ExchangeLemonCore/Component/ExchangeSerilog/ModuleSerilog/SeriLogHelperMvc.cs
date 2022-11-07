using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BlueLight.Main;
using ExchangeLemonCore;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ExchangeSerilog
{
    public class SeriLogHelperMvc : ILogHelperMvc
    {

        // string moduleName = "LogHelperMvc";
        SerilogHelper serilogHelper;

        public SeriLogHelperMvc(SerilogHelper serilogHelper)
        {
            this.serilogHelper = serilogHelper;
        }

        public async Task SaveLog(object output, HttpRequest request,
            [CallerMemberName] string callerName = "")
        {
            await Task.Delay(0);
            // var t1 = JsonConvert.SerializeObject(output);
            // serilogHelper.SendMessage(moduleName, t1);
        }

        public async Task SaveError(object output, HttpRequest request,
            Exception ex, [CallerMemberName] string callerName = "")
        {
            await Task.Delay(0);

            var content = JsonConvert.SerializeObject(output);

            var logMvc = new LogMvc()
            {
                CallerName = callerName,
                Content = content,
            };
            LogMvcHelper.Populate(request, ex, logMvc);
            

            // if (ParamRepo.IsSaveLogEnable)
            // {
            //     // context.LogMvcs.Add(logMvc);
            //     await serilogHelper.SaveChangesAsync();
            // }

            // var t1 = JsonConvert.SerializeObject(output);
            // serilogHelper.SendMessage(moduleName, t1);
            // var t2 = JsonConvert.SerializeObject(logMvc);
            // serilogHelper.SendMessage(moduleName, t2);

        }


    }
}