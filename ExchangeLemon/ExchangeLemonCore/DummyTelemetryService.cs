using System;
using BlueLight.Main;

namespace ExchangeLemonCore
{
    public class DummyTelemetryService : ICustomTelemetryService
    {
        public void Submit(Exception ex)
        {
            
        }
    }
}