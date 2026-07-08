using Microsoft.AspNetCore.Mvc;

namespace PORTIMAGES.Web.Controllers.Website
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
