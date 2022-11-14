using ASP.NET_Lab_4.Data;
using ASP.NET_Lab_4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;


namespace ASP.NET_Lab_4.Controllers
{

    public class StudentController : Controller
    {
        Lab_4_DB dB;

        public StudentController(Lab_4_DB _dB)
        {
            dB = _dB;
        }

        [Authorize(Roles = ("Admin,Instructor,Student"))]
        public IActionResult Index()
        {
            // Eager Loading

            return View(dB.Students.Include(a => a.Department).ToList());
        }

        // Create EndPoint
        [Authorize(Roles = ("Admin,Instructor"))]

        [HttpGet]
        public IActionResult Create()
        {

            ViewBag.dept = new SelectList(dB.Departments.ToList(), "DeptId", "DeptName");
            return View();
        }
        [HttpPost]
        public IActionResult Create(Student std)
        {
            // Validation 3nd el server 2bl ma y3ml submit le el Database
            if (ModelState.IsValid)
            {
                dB.Students.Add(std);
                dB.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.dept = new SelectList(dB.Departments.ToList(), "DeptId", "DeptName");
            return View(std);
        }

        // Update EndPoint 
        [Authorize(Roles = ("Admin,Instructor"))]

        [HttpGet]
        public IActionResult Update(int? id)
        {

            var oldstd = dB.Students.FirstOrDefault(a => a.Id == id);
            ViewBag.dept = new SelectList(dB.Departments.ToList(), "DeptId", "DeptName");
            return View(oldstd);
        }

        [HttpPost]
        public IActionResult Update(Student std)
        {
            var oldstd = dB.Students.FirstOrDefault(a => a.Id == std.Id);
            if (ModelState.IsValid)
            {
                oldstd.Name = std.Name;
                oldstd.Age = std.Age;
                oldstd.Email = std.Email;
                oldstd.Username = std.Username;
                oldstd.DeptId = std.DeptId;

                dB.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {

                return View();
            }


        }


        // Delete EndPoint
        [Authorize(Roles = ("Admin,Instructor"))]

        public IActionResult Delete(int? id)
        {
            var DeletedStd = dB.Students.FirstOrDefault(a => a.Id == id.Value);
            dB.Students.Remove(DeletedStd);
            dB.SaveChanges();
            return RedirectToAction("Index");

        }

        // Details EndPoint
        [Authorize(Roles = ("Admin,Instructor,Student"))]

        public IActionResult Details(int? Id)
        {
            var std = dB.Students.Include(a => a.Department).FirstOrDefault(a => a.Id == Id);
            return View(std);
        }


        // CheckUserName EndPoint 

        [HttpPost]
        public IActionResult CheckUserName(string Username, int Id)
        {
            return Json(IsUnique(Username, Id));

        }


        private bool IsUnique(string Username, int Id)
        {
            if (Id == 0)
            {
                return !dB.Students.Any(a => a.Username == Username);
            }
            else
            {
                return !dB.Students.Any(a => a.Username == Username && a.Id != Id);

            }
        }














    }
}
