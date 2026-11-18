using Microsoft.AspNetCore.Mvc;

namespace ProjectChaptereight.BlogWeb.Areas.Admin.Controllers
{

    public class HomeController : AdminControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
