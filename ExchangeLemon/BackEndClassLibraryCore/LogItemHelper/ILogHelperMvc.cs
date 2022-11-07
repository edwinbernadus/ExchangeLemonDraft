using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
namespace BlueLight.Main
{
    public interface ILogHelperMvc
    {
        Task SaveLog(object output, HttpRequest request, string callerName = "");

        Task SaveError(object output, HttpRequest request, Exception ex, string callerName = "");
    }
}