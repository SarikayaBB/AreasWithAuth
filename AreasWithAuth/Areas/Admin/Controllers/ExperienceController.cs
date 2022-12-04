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
            return Json(new { data = _db.Experiences.OrderBy(e => e.DateModified).ToList() });
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


        [Authorize(Roles = "Admin")]
        public IActionResult DeleteExperience(Experience experience)
        {
            try
            {
                Experience deletedExperience = _db.Experiences.Find(experience.Id);
                if (deletedExperience != null)
                {
                    _db.Remove(deletedExperience);
                    _db.SaveChanges();
                }
            }
            catch (Exception err)
            {
                return Json(err);
            }
            return Json(experience.ToString());
        }
    
        
        [Authorize(Roles = "Admin")]
        public IActionResult FindById(Experience experience)
        {
            Experience foundExperience = _db.Experiences.FirstOrDefault(e => e.Id == experience.Id);
            if (foundExperience != null)
            {
                return Json(foundExperience);
            }
            else
            {
                return Json(null);
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult EditExperience(Experience experience)
        {
            Experience foundExperience = _db.Experiences.FirstOrDefault(ex=>ex.Id== experience.Id);
            foundExperience.StartingDate = experience.StartingDate;
            foundExperience.EndingDate= experience.EndingDate;
            foundExperience.CompanyName = experience.CompanyName;
            foundExperience.CompanyDescription = experience.CompanyDescription;
            foundExperience.Location = experience.Location;
            foundExperience.Name = experience.Name;
            foundExperience.Position= experience.Position;
            foundExperience.PositionDescription= experience.PositionDescription;
            foundExperience.DateModified = DateTime.Now;
            _db.Update(foundExperience);
            _db.SaveChanges();
            return Json(foundExperience);
        }
    }
}
