using Microsoft.AspNetCore.Mvc;

namespace WebSupportTeam.Controllers
{
    public class ApplicationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
