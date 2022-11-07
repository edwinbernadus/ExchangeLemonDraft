using System;
using System.Threading;

namespace BlueLight.Main
{
    public class SemaphoreDetail : IDisposable
    {
        public SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);
        public int TotalFlag { get; set; }

        public void Dispose()
        {
            semaphoreSlim.Dispose();
        }
    }
}