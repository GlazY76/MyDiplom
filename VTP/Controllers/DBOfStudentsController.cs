using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VTP.Data;
using VTP.Models;
using VTP.Models.ViewModels;

namespace VTP.Controllers
{
    [Authorize]
    public class DBOfStudentsController : Controller
    {
        ApplicationDbContext db;

        public DBOfStudentsController(ApplicationDbContext _db)
        {
            db = _db;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult EditStudent(int id)
        {
            var student = db.Students.FirstOrDefault(s => s.Id == id);
            return View(student);
        }

        [HttpGet]
        public ActionResult SaveEditStudent([FromBody]Student student)
        {
            if (student != null)
            {
                db.Students.Update(student);
                db.SaveChanges();
            }
            return Json(Url.Action("Index", "DBOfStudents"));
        }

        [HttpGet]
        public ActionResult AddStudents()
        {
            return View();
        }

        public ActionResult GetAll()
        {
            var students = db.Students.ToList();
            return PartialView("PartialView/_GetAll", students);
        }

        public ActionResult AddStudentsPartial()
        {
            return PartialView("PartialView/_AddStudentsPartial");
        }

        [HttpPost]
        public ActionResult AddStudentsPost([FromBody]IEnumerable<Student> students)
        {
            if (students.Count() > 0)
            {
                foreach (var student in students)
                {
                    db.Students.Add(student);
                }
                db.SaveChanges();
            }
            return Json(Url.Action("Index", "DBOfStudents"));
        }

        [HttpDelete]
        public void DeleteStudent([FromBody]int id)
        {
            var student = db.Students.FirstOrDefault(s => s.Id == id);
            if (student != null)
            {
                db.Students.Remove(student);
                db.SaveChanges();
            }
        }

        public ActionResult ShowStudent(int id)
        {
            var student = db.Students.FirstOrDefault(s => s.Id == id);
            if (student != null) {
                return View(student);
            }
            return View("Index");
        }
    }
}