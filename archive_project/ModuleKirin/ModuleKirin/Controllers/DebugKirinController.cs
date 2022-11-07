//using System.Web.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlueLight.KirinLogic
{
    public class DebugKirinController : Controller {
        //[Route ("debug/version")]
        public string Version () {
            var localVersion = 2;
            var globalVersion = BlueLight.Main.VersionItem.ParamVersion;
            var output = $"kirin - {localVersion} - {globalVersion}";
            return output;

        }

   
    }

}