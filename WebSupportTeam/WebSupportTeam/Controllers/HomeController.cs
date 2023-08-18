using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using WebSupportTeam.Data;
using Microsoft.EntityFrameworkCore;

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
            ViewBag.StationCount = _db.station_master.Count();
            ViewBag.fileCount = Directory.GetFiles(BaseDirectory() + "documents", "*", SearchOption.AllDirectories).Length;
            @ViewBag.cardCount = _db.card.Count();
            @ViewBag.appCount = 99;
            ViewBag.StationCount = _db.station_master.Count();
            ViewBag.shiftSchedule1 = _db.data_config.Where(x => x.key == "shiftSchedule1").ToList()[0].value;
            ViewBag.shiftSchedule2 = _db.data_config.Where(x => x.key == "shiftSchedule2").ToList()[0].value;
            ViewBag.fileCount = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\wwwroot\\documents").Length;
            return View();
        }

        public string BaseDirectory() 
        {
            var documentPath = AppContext.BaseDirectory; //C:\Users\Aof\Desktop\WebSupportTeam\WebSupportTeam\bin\Debug\net7.0\
            return documentPath;
        }


        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult StationMaster()
        {
            return View();
        }

        public IActionResult NoteCard()
        {
            return View();
        }

        public IActionResult Document()
        {
            return View();
        }

        public IActionResult Application()
        {
            return View();
        }
        public IActionResult Help()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images"); //"E:\\FILE\\WebSupportTeam\\WebSupportTeam\\wwwroot\\uploads"

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName; //"e49b3c28-ed0d-4b4e-ac2c-8bd98d647280_1.png"
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create)) //"E:\\FILE\\WebSupportTeam\\WebSupportTeam\\wwwroot\\uploads\\e49b3c28-ed0d-4b4e-ac2c-8bd98d647280_1.png"
                {
                    await file.CopyToAsync(stream);
                }

                var data = _db.data_config.FirstOrDefault(x => x.key == "shiftSchedule1");
                data.value = uniqueFileName; 
                _db.SaveChanges();

                return RedirectToAction("Home", new { message = "File uploaded successfully!" });
            }

            return RedirectToAction("Home", new { message = "No file uploaded." });
        }
    }
}