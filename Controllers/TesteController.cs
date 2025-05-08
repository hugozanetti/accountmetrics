using Microsoft.AspNetCore.Mvc;

namespace accountmetrics.Controllers;

    public class TesteController : Controller
    {
        // GET: TesteController
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TestaSoma(int? param1, int? param2)
        {
            var result = param1.Value + param2.Value;
            ViewData["resultado"] = result.ToString();

            return View();
        }

    }

