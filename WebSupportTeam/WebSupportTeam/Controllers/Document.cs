using Microsoft.AspNetCore.Mvc;
using WebSupportTeam.Data;

namespace WebSupportTeam.Controllers
{
    public class DocumentController : Controller
    {

        private readonly ApplicationDbContext db;
        public DocumentController(ApplicationDbContext db)
        {
            db = db;
        }
        public class DocumentModal
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public string User { get; set; }
        }
        private List<DocumentModal> _documentList = new List<DocumentModal>();
        public IActionResult Index()
        {
            var list = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\wwwroot\\documents"); //"D:\\githup\\WebSupportTeam\\WebSupportTeam\\wwwroot\\documents\\Lavender fleet fraud diagram2.pdf"
            for (int i = 1; i <= list.Count(); i++)
            {
                DocumentModal D = new DocumentModal();
                D.Id = i;
                D.Name = list[i - 1].Split('\\').Last().ToString();
                D.Type = D.Name.Split('.').Last();
                D.User = "Admin";
                _documentList.Add(D);
            }
            return View(_documentList);
        }

        public IActionResult add() 
        {
            return RedirectToAction("Index");
           /* return RedirectToAction("Index", "Document");*/
        }


        [HttpPost]
        [Route("/Document/delete")]
        public IActionResult delete([FromBody] string _name)
        {
            if (_name != null)
            {
                string _path = Directory.GetCurrentDirectory() + "\\wwwroot\\documents\\" + _name; /*"D:\\githup\\WebSupportTeam\\WebSupportTeam\\wwwroot\\documentsLavender fleet fraud diagram2.pdf"*/
                FileInfo file = new FileInfo(_path);
                file.Delete();
            }

           /* return RedirectToAction("Index");*/
            return Redirect("https://localhost:7292/Document/Index#");
            /*return RedirectToAction("Index", "Document");*/
        }
    }
}
