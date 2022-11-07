using Microsoft.Extensions.DependencyInjection;
using BackEndClassLibrary;

namespace BlueLight.Main.Tests
{
    public class ServiceHelper
    {
        public static ServiceProvider Generate()
        {
            var serviceCollection = DependencyHelper.GenerateServiceCollectionForTesting();
            ServiceProvider service = serviceCollection.BuildServiceProvider();

            return service;
        }
    }
}