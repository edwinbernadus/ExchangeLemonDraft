using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BlueLight.Main
{
    public interface IContextSaveService
    {
        Task ExecuteAsync();
        
    }
}