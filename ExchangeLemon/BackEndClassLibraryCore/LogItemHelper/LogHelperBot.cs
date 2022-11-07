using System.Threading.Tasks;

namespace BlueLight.Main
{

    public class LogHelperBot
    {
        private LoggingContext context;

        public LogHelperBot(LoggingContext context)
        {
            this.context = context;
        }
        public async Task Save(int id, string userName)
        {
            await Task.Delay(0);
            // var content = id.ToString();
            // var classType = "number-id";

            // var log = new LogBot()
            // {
            //     Content = content,
            //     UserName = userName,
            //     ClassType = classType
            // };

            // if (ParamRepo.IsSaveLogEnable)
            // {
            //     context.LogBots.Add(log);
            //     await context.SaveChangesAsync();
            // }
        }

    }
}