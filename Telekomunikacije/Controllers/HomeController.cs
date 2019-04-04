using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telekomunikacije.Models;

namespace Telekomunikacije.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext  _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            var users = _context.Users.ToList();
            return View(users);
        }

        public ActionResult About()
        {
            var course = new Course
            {
                Name = "Akustika",
                ESPB = 8,
                Password = "1234",
                Year = 4,
                Id = 1,

                ProffesorIds = new List<int> { 1,2,3,4,5}
                


            };

            return View(course);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}