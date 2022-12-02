using AreasWithAuth.Data;
using AreasWithAuth.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AreasWithAuth.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;
        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }


        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AppUser appUser)
        {
            AppUser usr = _db.AppUsers.Include(u => u.AppUserRole).FirstOrDefault(us=>us.UserName == appUser.UserName && us.Password == appUser.Password);
            if(usr !=null)
            {
                List<Claim> userClaims = new List<Claim>();
                userClaims.Add(new Claim(ClaimTypes.Name,usr.UserName));
                userClaims.Add(new Claim(ClaimTypes.GivenName,usr.Name));
                userClaims.Add(new Claim(ClaimTypes.NameIdentifier,usr.Id.ToString()));
                userClaims.Add(new Claim(ClaimTypes.Role,usr.AppUserRole.RoleName));
                var claimsIdentity = new ClaimsIdentity(userClaims,CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,new ClaimsPrincipal(claimsIdentity));
                return Json(usr);
            }
            return View(appUser);   
        }

    }
}
