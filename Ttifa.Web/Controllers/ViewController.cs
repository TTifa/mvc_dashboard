using System.Web.Mvc;
using Ttifa.Generate;

namespace Ttifa.Web.Controllers
{
    public class ViewController : Controller
    {
        // GET: View
        public ActionResult Index(string module, string viewName)
        {
            var name = string.IsNullOrEmpty(module?.Trim()) ? viewName : $"{module}\\{viewName}";

            return View($"/Views/{name}.cshtml");
        }

        public ApiResult Generate(string module, string view)
        {
            var genrateTask = new GenerateTask(null);
            genrateTask.Generate(module, view);
            //genrateTask.GenerateAll();

            return new ApiResult();
        }
    }
}