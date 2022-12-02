using AreasWithAuth.Data;
using AreasWithAuth.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AreasWithAuth.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        public HomeController(ApplicationDbContext db)
        {
            _db = db;
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