using System.Web.Mvc;

namespace Ttifa.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Admin()
        {
            return View();
        }

        public ActionResult Editor()
        {
            return View();
        }

        public ActionResult Upload()
        {
            return View();
        }
    }
}