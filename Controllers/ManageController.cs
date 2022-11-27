using ASP.NET_Lab_4.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ASP.NET_Lab_4.Controllers
{
    [Authorize(Roles ="Admin")]
    public class ManageController : Controller
    {
        private Lab_4_DB db;

        public ManageController(Lab_4_DB _db)
        {
            db = _db;
        }

        public IActionResult GetRoles()
        {

            ViewBag.Roles = new SelectList(db.Roles.ToList(), "Id", "RoleName");
            return View();
        }
        public IActionResult GetUsers(int roleid)
        {
            var role = db.Roles.Include(a => a.Users).FirstOrDefault(a => a.Id == roleid);
            //get users in role
            ViewBag.usersinrole = new SelectList(role.Users.ToList(), "Id", "Name");
            //get users not in role
            var allusers = db.Users.ToList();
            ViewBag.usersnotinrole = new SelectList(allusers.Except(role.Users.ToList()), "Id", "Name");
            ViewBag.RoleId = roleid;
            ViewBag.Roles = new SelectList(db.Roles.ToList(), "Id", "RoleName", roleid);
            return View();
        }

        public IActionResult ChangeUserRoles(int roleid, List<int> UserToRemove, List<int> UserToAdd)
        {
            var role = db.Roles.Include(a => a.Users).FirstOrDefault(a => a.Id == roleid);
            foreach(var user in UserToRemove)
            {
                role.Users.Remove(db.Users.FirstOrDefault(a => a.Id == user));
            }

            foreach (var user in UserToAdd)
            {
                role.Users.Add(db.Users.FirstOrDefault(a => a.Id == user));
            }

            db.SaveChanges();
            return RedirectToAction("GetRoles");
        }
    }
}
