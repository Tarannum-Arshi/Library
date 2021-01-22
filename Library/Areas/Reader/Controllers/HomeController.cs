using Library.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Library.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace Library.Areas.Reader.Controllers
{
    [Area("Reader")]
    public class HomeController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
           _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Index(string name, string password)
        {


            if (ModelState.IsValid)
            {
                var admin = _unitOfWork.Admin.GetFirstOrDefault(a => a.Name == name && a.Password == password);
                if (admin != null)
                {
                    var identity = new ClaimsIdentity(new[] {
                     new Claim("id", admin.UserId.ToString()),
                     new Claim("email", admin.Email.ToString()),
                     new Claim("names", admin.Name),
                     new Claim("phone", admin.Phone.ToString()),
                     new Claim(ClaimTypes.Role, admin.Category)
                    }, CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);

                    var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Index", "User", new { area = "Reader" });
                }
                else
                {
                    ViewData["Message"] = "Incorrect username or password";
                    return RedirectToAction(nameof(Index));

                }
            }
            return View();
        }
        [HttpPost]
        public IActionResult Register(Admin admin)
        {
            admin.Category = "u";

            if (ModelState.IsValid)
            {
                _unitOfWork.Admin.Add(admin);

                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(admin);
        }
    }

}
