using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Runtime.InteropServices.JavaScript;
using WebSupportTeam.Data;
using WebSupportTeam.Models;

namespace WebSupportTeam.Controllers
{
    public class NoteCardController : Controller
    {
        private readonly ApplicationDbContext _db;
        public NoteCardController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var _data = _db.card.OrderBy(x=>x.card_id).ToList();
            return View(_data);
        }

        [HttpPost]
        [Route("/NoteCard/GetCard")]
        public IActionResult GetCard([FromBody] int _id)
        {
            var dataJson = Json(_db.card.Where(x => x.card_id == _id).ToList());
            return dataJson;

        }

        [HttpPost]
        [Route("/NoteCard/addCard")]
        public IActionResult addCard([FromBody] string _data)
        {
            try
            {
                var data = JsonConvert.DeserializeObject<cards>(_data);
                data.time_update = DateTime.Now.ToUniversalTime();
                data.user = "ADMIN";
                _db.Add(data);
                _db.SaveChanges();
                return Ok("complete");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpPost]
        [Route("/NoteCard/editCard")]
        public IActionResult editCard([FromBody] string _data)
        {
            try
            {
                var data = JsonConvert.DeserializeObject<cards>(_data);
                var card = _db.card.SingleOrDefault(c => c.card_id == data.card_id);
                card.card_id = data.card_id;
                card.title = data.title;
                card.detail = data.detail;
                card.time_update = DateTime.Now.ToUniversalTime();
                card.user = "ADMIN";
                _db.SaveChanges();
                return Ok("complete");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpPost]
        [Route("/NoteCard/deleteCard")]
        public IActionResult deleteCard([FromBody] string _id)
        {
            try
            {
                var card = _db.card.SingleOrDefault(c => c.card_id == int.Parse(_id));
                _db.card.Remove(card);
                _db.SaveChanges();
                return Ok("complete");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
