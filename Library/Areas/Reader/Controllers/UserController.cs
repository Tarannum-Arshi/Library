using Library.DataAccess.Repository.IRepository;
using Library.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Threading;
using Library.Utility;
using Microsoft.Data.SqlClient;
using Dapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
namespace Library.Areas.Reader.Controllers
{
    [Area("Reader")]
    [Authorize(Roles = "u")]
    public class UserController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Return()
        {
            return View();
        }
        public IActionResult Edit()
        {
            string claimvalue = User.FindFirst("id").Value;
            int id = Convert.ToInt32(claimvalue);
            Admin admin = new Admin();
            admin = _unitOfWork.Admin.Get(id);
            if (admin == null)
            {
                return NotFound();
            }
            return View(admin);
        }
        public IActionResult Confirm(int? id)
        {
            Book book = new Book();
            if (id == null)
            {
                return View(book);
            }

            book = _unitOfWork.Book.Get(id.GetValueOrDefault());
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }
        public IActionResult Issue(int? id )
        {
            Book book = new Book();
             if (id == null)
             {
               return View(book);
             }

            book = _unitOfWork.Book.Get(id.GetValueOrDefault());
             if (book == null)
             {
               return NotFound();
             }
           int avs = book.AvailableStock;
            if (avs==0)
            {
               return NotFound();
            }
               return View(book);
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home", new { area = "Reader" });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, string Name, int Phone, string Email, string Password)
        {
            Admin admin = new Admin();
            admin.UserId = id;
            admin.Name = Name;
            admin.Phone = Phone;
            admin.Email = Email;
            admin.Password = Password;
            admin.Category = "u";

            if (ModelState.IsValid)
            {
                _unitOfWork.Admin.Update(admin);

                _unitOfWork.Save();
                return RedirectToAction("Index", "User", new { area = "Reader" });
            }
            return View(admin);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Issue(int id)
        {
            string claimvalue = User.FindFirst("id").Value;
            int value = Convert.ToInt32(claimvalue);
            Issue issue = new Issue();
            DateTime dt = new DateTime();
            dt = DateTime.Now;
            issue.UserId = value;
            issue.Bookid = id;
            issue.IssueDate = dt;
            issue.ReturnDate = dt.AddDays(15);
            issue.Status = "p";

            if (ModelState.IsValid)
            {
                _unitOfWork.Issue.Add(issue);

                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(issue);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Confirm(int id)
        {
            Book book = new Book();
            book = _unitOfWork.Book.Get(id);
            book.AvailableStock = book.AvailableStock + 1;
            var issue = _unitOfWork.Issue.GetFirstOrDefault(i => i.Bookid == id);
            var iid = issue.IssueId;
            if (ModelState.IsValid)
            {
                _unitOfWork.Issue.Remove(iid);

                _unitOfWork.Save();

                _unitOfWork.Book.Update(book);

                _unitOfWork.Save();

                return RedirectToAction(nameof(Return));
            }
            return View(issue);
        }

        #region API CALLS
        
        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.Book.GetAll();
            return Json(new { data = allObj });
        }

        [HttpGet]
        public IActionResult GetAlls()
        {
            string claimvalue = User.FindFirst("id").Value;
            var userid = new DynamicParameters();
            userid.Add("UserId", claimvalue);
            var issue = _unitOfWork.SPCall.List<Return>(SD.Proc_Issue_Get, userid);
            return Json(new { data = issue });
        }
        #endregion
    }
}
