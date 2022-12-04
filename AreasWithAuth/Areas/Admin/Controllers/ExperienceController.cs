using AreasWithAuth.Data;
using AreasWithAuth.Models;
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
            return Json(new { data = _db.Experiences.OrderBy(e=>e.DateModified).ToList() });
        }
        [Authorize(Roles = "Admin")]
        public IActionResult AddExperience(Experience experience)
        {
            try
            {
                Experience addedExperience = new Experience();
                addedExperience.Position = experience.Position;
                addedExperience.PositionDescription = experience.PositionDescription;
                addedExperience.Location = experience.Location;
                addedExperience.CompanyName = experience.CompanyName;
                addedExperience.CompanyDescription = experience.CompanyDescription;
                addedExperience.StartingDate = experience.StartingDate;
                if (experience.EndingDate != null)
                {
                    addedExperience.EndingDate = experience.EndingDate;
                }
                addedExperience.Name = experience.Name;
                _db.Add(addedExperience);
                _db.SaveChanges();
                return Json(new { data = addedExperience });
            }
            catch (Exception err)
            {
                return Json(err);
            }
        }
    }
}
