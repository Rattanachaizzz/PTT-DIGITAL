using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebSupportTeam.Data;
using WebSupportTeam.Models;

namespace WebSupportTeam.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Home()
        {
            ViewBag.StationCount = _db.Station_masters.Count();
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}