using AreasWithAuth.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AreasWithAuth.Web.Areas.Admin.Controllers
{

   [Area("Admin")] 
    public class ExperienceController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ExperienceController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult GetAll()
        {
            return Json(new { data = _db.Experiences.ToList() });
        }
    }
}
