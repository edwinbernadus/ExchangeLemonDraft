using Microsoft.AspNetCore.Mvc;
//using ExchangeLemonCore.Data;
//using BackEndStandard;
//using ExchangeLemonCore.Data;

namespace ExchangeLemonCore.Controllers
{
    public class DebugViewController : Controller
    {

        // http://localhost:5000/debugView/demoAmp
        public ActionResult DemoAmp()
        {
            return View();
        }



        // http://localhost:5000/debugView/GraphTwo
        public ActionResult GraphTwo()
        {
            return View();
        }

        // http://localhost:50727/debugView/GraphTwoPartial
        public ActionResult GraphTwoPartial()
        {
            return PartialView();
        }

        // http://localhost:50727/debugView/GraphThreeContent
        public ActionResult GraphThreeContent()
        {
            return Content("woot");
        }


        // http://localhost:50727/debugView/GraphThree
        public ActionResult GraphThree()
        {
            return View();
        }

    }
}