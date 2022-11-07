using System;
using System.Threading.Tasks;
using BlueLight.Main;
//using DebugWorkplace;

namespace ConsoleDev
{
    class LogicScenarioOne
    {

        public LogicScenarioOne(IServiceProvider inputServiceProvider)
        {
            serviceProvider = inputServiceProvider;
        }

        public IServiceProvider serviceProvider { get; }

        public async Task Register()
        {
            await Task.Delay(0);
            //{
            //    var l = new LogicArchive(serviceProvider);


            //    var c = BtcCloudService.Generate();
            //    var lists = await c.GetListHook();

            //    await l.Logic9DeleteAllHooks();

            //}
            //{
            //    var l = new LogicSubmitRegister(serviceProvider);
            //    var m = await l.GetList();
            //    await l.ExecuteRegister();

            //}

        }

        internal async Task GenerateAddress()
        {
            await Task.Delay(0);
            var logic = new BtcService();
            var s1 = logic.GenerateAddress();
        }
    }
}
