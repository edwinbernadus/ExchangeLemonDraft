using System;

namespace BlueLight.Main
{
    public interface ICustomTelemetryService
    {
        void Submit(Exception ex);
    }
}