using Newtonsoft.Json;
using Serilog;
//using Serilog.Sinks.MSSqlServer;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace BlueLight.Main
{
    public class LogHelperObject
    {
        private readonly LoggingContext context;

        public LogHelperObject(LoggingContext dBContext)
        {
            context = dBContext;
        }

        public async Task SaveObjectIncludeUser(object item,
            string userName, string moduleName, [CallerMemberName] string callerName = "")
        {
            await Task.Delay(0);
            // //var context = new DBContext();
            // var input = item;
            // var content = JsonConvert.SerializeObject(input);
            // var classType = input.GetType().ToString();

            // var log = new LogItem()
            // {
            //     Content = content,
            //     UserName = userName,
            //     ClassName = classType,
            //     ModuleName = moduleName,
            //     CallerName = callerName
            // };

            // if (ParamRepo.IsSaveLogEnable)
            // {
            //     context.LogItems.Add(log);
            //     await context.SaveChangesAsync();
            // }

            // var c2 = $"{content} - {userName} - {classType}";
            // Log.Warning(c2);
            // // LogHelperConsole.SaveLogWarning (c2);
        }

        public async Task SaveObject(object item, [CallerMemberName] string callerName = "")
        {
            await Task.Delay(0);

            // var content = JsonConvert.SerializeObject(item);

            // string moduleName = "SaveObject";
            // var log = new LogItem()
            // {
            //     Content = content,
            //     UserName = "-",
            //     ClassName = "-",
            //     ModuleName = moduleName,
            //     CallerName = callerName
            // };

            // if (ParamRepo.IsSaveLogEnable)
            // {
            //     context.LogItems.Add(log);
            //     await context.SaveChangesAsync();
            // }

            // var c2 = $"{item} - {moduleName}";
            // Log.Warning(c2);
        }

    }
}