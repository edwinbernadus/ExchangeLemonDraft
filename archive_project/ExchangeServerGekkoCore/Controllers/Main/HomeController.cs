using Microsoft.AspNetCore.Mvc;

namespace BlueLight.Main
{
    public class HomeController : Controller {

        public string Index () {
            return "Hello World";
        }
    }
}