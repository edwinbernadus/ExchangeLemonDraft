using Microsoft.AspNetCore.Mvc;

namespace ExchangeLemonCore.Controllers
{
    public class DebugGraphController : Controller
    {
        // http://localhost:5000/debugGraph
        public ActionResult Index()
        {
            // return PartialView();
            return View();
        }
    }
}
