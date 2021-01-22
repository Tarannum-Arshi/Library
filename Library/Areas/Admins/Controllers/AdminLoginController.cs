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

namespace Library.Areas.Admins.Controllers
{
    [Area("Admins")]
    public class AdminLoginController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;

        public AdminLoginController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string name, string password)
        {

            
            if (ModelState.IsValid)
            {
                var admin = _unitOfWork.Admin.GetFirstOrDefault(a => a.Name == name && a.Password == password);
                if (admin!= null)
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

                    return RedirectToAction("Index", "Home", new { area = "Admins" });
                }
                else
                {
                    ViewData["Message"] = "Incorrect username or password";
                    return RedirectToAction(nameof(Index));

                }
            }
            return View();
        }

    }
}
