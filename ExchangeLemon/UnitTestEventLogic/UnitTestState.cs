//using BackEndClassLibrary;
//using FunctionBitcoinExt;
//using Microsoft.Extensions.DependencyInjection;
//using System;

//namespace BlueLight.Main.Tests
//{
//    public class UnitTestState
//    {
//        public IServiceProvider serviceProvider { get; set; }
//        public ApplicationContext context { get; set; }
//        public IBtcCloudServiceRegisterNotification BtcCloudServiceRegisterNotification { get; internal set; }

     

//        public static UnitTestState GenerateTwo (ServiceCollection serviceCollection)
//        {
//            var serviceProvider = serviceCollection.BuildServiceProvider();
//            var context = serviceProvider.GetService<ApplicationContext>();
//            var output = new UnitTestState()
//            {
//                context = context,
//                serviceProvider = serviceProvider
//            };
//            return output;
//        }
//    }
//}