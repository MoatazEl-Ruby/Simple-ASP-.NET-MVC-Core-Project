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
            var students = dB.Users.Where(a => a.Roles.Contains(dB.Roles.FirstOrDefault(x => x.RoleName == "Student")));
            return View(students);
        }

        [Authorize(Roles = ("Admin"))]

        public IActionResult showInstructors()
        {
            var instructors = dB.Users.Where(a => a.Roles.Contains(dB.Roles.FirstOrDefault(x => x.RoleName == "Instructor")));
            return View(instructors);
        }  
        
        
        

        // Create EndPoint
        [Authorize(Roles = ("Admin,Instructor"))]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                dB.Users.Add(user);
                user.AddRoleToUser(dB.Roles.FirstOrDefault(a => a.Id == 3));
                dB.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }



        [Authorize(Roles = ("Admin"))]
        [HttpGet]
        public IActionResult CreateInstructor()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateInstructor(User user)
        {
            if (ModelState.IsValid)
            {
                dB.Users.Add(user);
                user.AddRoleToUser(dB.Roles.FirstOrDefault(a => a.Id == 2));
                dB.SaveChanges();
                return RedirectToAction("showInstructors");
            }
            return View(user);
        }







        // Update EndPoint 
        [Authorize(Roles = ("Admin"))]
        [HttpGet]
        public IActionResult Update(int? id)
        {

            var oldstd = dB.Users.FirstOrDefault(a => a.Id == id);
            return View(oldstd);
        }

        [HttpPost]
        public IActionResult Update(User user)
        {
            var oldstd = dB.Users.FirstOrDefault(a => a.Id == user.Id);
            if (ModelState.IsValid)
            {
                oldstd.Name = user.Name;
                oldstd.Age = user.Age;
                oldstd.Email = user.Email;
                oldstd.UserName = user.UserName;
                dB.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(user);
            }
        }


        [Authorize(Roles = ("Admin"))]
        [HttpGet]
        public IActionResult UpdateInstructor(int? id)
        {

            var oldstd = dB.Users.FirstOrDefault(a => a.Id == id);
            return View(oldstd);
        }

        [HttpPost]
        public IActionResult UpdateInstructor(User user)
        {
            var oldstd = dB.Users.FirstOrDefault(a => a.Id == user.Id);
            if (ModelState.IsValid)
            {
                oldstd.Name = user.Name;
                oldstd.Age = user.Age;
                oldstd.Email = user.Email;
                oldstd.UserName = user.UserName;
                dB.SaveChanges();
                return RedirectToAction("showInstructors");
            }
            else
            {
                return View(user);
            }
        }





        // Delete EndPoint
        [Authorize(Roles = ("Admin"))]
        public IActionResult Delete(int? id)
        {
            var DeletedStd = dB.Users.FirstOrDefault(a => a.Id == id.Value);
            dB.Users.Remove(DeletedStd);
            dB.SaveChanges();
            return RedirectToAction("Index");
        }


        [Authorize(Roles = ("Admin"))]
        public IActionResult DeleteInstructor(int? id)
        {
            var DeletedStd = dB.Users.FirstOrDefault(a => a.Id == id.Value);
            dB.Users.Remove(DeletedStd);
            dB.SaveChanges();
            return RedirectToAction("showInstructors");
        }



        // Details EndPoint
        [Authorize(Roles = ("Admin,Instructor,Student"))]
        public IActionResult Details(int? Id)
        {
            var std = dB.Users.FirstOrDefault(a => a.Id == Id);
            return View(std);
        }


        [Authorize(Roles = ("Admin"))]
        public IActionResult DetailsInstructor(int? Id)
        {
            var std = dB.Users.FirstOrDefault(a => a.Id == Id);
            return View(std);
        }



        //CheckUserName EndPoint

        [HttpPost]
         public IActionResult CheckUserName(string UserName, int? Id)
        {
            return Json(IsUnique(UserName, Id));

        }


        private bool IsUnique(string UserName, int? Id)
        {
            if (Id == 0)
            {
                return !dB.Users.Any(a => a.UserName == UserName);
            }
            else
            {
                return !dB.Users.Any(a => a.UserName == UserName && a.Id != Id);

            }
        }
    }
}
