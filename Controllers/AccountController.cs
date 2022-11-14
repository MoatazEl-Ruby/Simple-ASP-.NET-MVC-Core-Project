using ASP.NET_Lab_4.Data;
using ASP.NET_Lab_4.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ASP.NET_Lab_4.Controllers
{
    public class AccountController : Controller
    {
        Lab_4_DB dB;

        public AccountController(Lab_4_DB _dB)
        {
            dB = _dB;
        }

        public IActionResult Index()
        {
            return View();
        }

        public  IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(!ModelState.IsValid)
                return View(model);
            var user = dB.Users.Include(a=> a.Roles).FirstOrDefault(a => a.UserName == model.UserName && a.Password == model.Password);
            if (user != null)
            {
                Claim c1 = new Claim(ClaimTypes.Name, user.UserName);
                Claim c2 = new Claim("Age", user.Age.ToString());
                ClaimsIdentity ci = new ClaimsIdentity("cookie");
                ci.AddClaim(c1);
                ci.AddClaim(c2);
                foreach (var item in user.Roles)
                {
                    ci.AddClaim(new Claim (ClaimTypes.Role, item.RoleName));
                }
                ClaimsPrincipal cp = new ClaimsPrincipal(ci);

                await HttpContext.SignInAsync(cp);
                return RedirectToAction("Index" , "Home");
            }
            else
            {
                ModelState.AddModelError("", "Username or Password Doesn't Exist");
                return View(model);
            }
        }


        public async Task<IActionResult> Logout()
        {
           await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }


        public  IActionResult accessdenied()
        {
            return View();
        }
    }
}
