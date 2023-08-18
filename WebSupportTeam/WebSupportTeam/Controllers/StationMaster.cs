using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;
using WebSupportTeam.Data;
using WebSupportTeam.Models;

namespace WebSupportTeam.Controllers
{
    public class StationMasterController : Controller
    {
        private readonly ApplicationDbContext _db;
        public StationMasterController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index(string filter, string key)
        {
            try
            {
                ViewBag.list = new List<string>() { "Pbl", "Bu", "Name", "Brand", "Time Update" };
                List<station_masters> station_master = null;
                switch (filter)
                {
                    case "Pbl":
                        station_master = _db.station_master.Where(x => x.pbl_station.Contains(key) || key == null).ToList();
                        break;
                    case "Bu":
                        station_master = _db.station_master.Where(x => x.bu_station.Contains(key) || key == null).ToList();
                        break;
                    case "Name":
                        station_master = _db.station_master.Where(x => x.name_station.Contains(key) || key == null).ToList();
                        break;
                    case "Brand":
                        station_master = _db.station_master.Where(x => x.brand_station.Contains(key) || key == null).ToList();
                        break;
                    case "Time Update":
                        station_master = _db.station_master.Where(x => x.create_date.ToString().Contains(key) || key == null).ToList();
                        break;
                    default:
                        //station_master = _db.station_master.Where(x => x.pbl_station.Contains(key) || key == null).ToList();
                        //station_master = _db.station_master.Where(x => x.pbl_station.Contains(key) || key == null).Take(10).OrderBy(x => x.id_station).ToList();
                        station_master = _db.station_master.Where(x => x.id_station <= 10).OrderBy(x => x.id_station).ToList();
                        break;
                }
                return View(station_master);
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Read()
        {
            return View();
        }
        public IActionResult Edit()
        {
            return View();
        }
        public IActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(station_masters data)
        {
            return View();
        }


        public IActionResult Search()
        {
            var data = ViewBag.Data;
            var filter = ViewBag.filter;
            return View();
        }
    }
}
