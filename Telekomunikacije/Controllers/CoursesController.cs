using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telekomunikacije.Models;

namespace Telekomunikacije.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private ApplicationDbContext _context;

        public CoursesController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpPost]
        public ActionResult Create(Course course)
        {
            
            if (!ModelState.IsValid)
            {

                return View("New", course);
            }
            _context.Courses.Add(course);
            _context.SaveChanges();
            return RedirectToAction("Index", "Courses");
        }

        public ActionResult Enrolment(int id)
        {
            var course = _context.Courses.Single(c => c.Id == id);

            return View(course);
        }

        [HttpPost]
        public ActionResult Delete(int Id)
        {
            var course = _context.Courses.Single(x => x.Id == Id);
            _context.Courses.Remove(course);
            _context.SaveChanges();
            return RedirectToAction("Index", "Courses");
        }
        


        [HttpPost]
        public ActionResult EnrolStudent(Course course)
        {
            var courseDb = _context.Courses.Single(c => c.Id == course.Id);

            if(courseDb.Password!=course.Password)
                ModelState.AddModelError("Password", "Pogresna Lozinka,Molimo Vas Da Unesete Tacnu");

            if (!ModelState.IsValid )
            {
                
                return View("Enrolment",courseDb);
            }
           

            return RedirectToAction("CourseStudent","Courses");
        }
        public ActionResult CourseStudent()
        {
            return View();
        }

        public ActionResult New()
        {



            return View();
        }

        public ActionResult Index()
        {

            var courses = _context.Courses.ToList();



            return View(courses);
        }

    }
}