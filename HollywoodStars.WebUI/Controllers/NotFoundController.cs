using Microsoft.AspNetCore.Mvc;

namespace HollywoodStars.WebUI.Controllers
{
    public class NotFoundController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
