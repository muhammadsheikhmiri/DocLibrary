using Microsoft.AspNetCore.Mvc;

namespace ProjectChaptereight.BlogWeb.Controllers
{
    public class ErrorHandlerController: Microsoft.AspNetCore.Mvc.Controller
    {
        [Route("/ErrorHandler/{code}")]
        public IActionResult Index(int code)
        {
            switch (code)
            {
                case 404:
                    return View("NotFound");
                case 500:
                    return View("ServerError");
            }
            return View("NotFound");
        }
    }

}
