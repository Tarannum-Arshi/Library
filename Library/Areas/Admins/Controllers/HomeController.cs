using Library.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Library.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Library.Utility;

namespace Library.Areas.Admins.Controllers
{
    [Area("Admins")]
    [Authorize(Roles ="a" )]
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
        public IActionResult Upsert(int? id )
        {
            Book book = new Book();
            
            if(id == null)
            {
                return View(book);
            }

            book = _unitOfWork.Book.Get(id.GetValueOrDefault());
            if(book == null)
            {
                return NotFound();
            }
            return View(book);

        }
        public IActionResult Approve()
        {

            return View();

        }
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Book.Get(id);
            _unitOfWork.Book.Remove( id);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Delete));

        }
        public IActionResult ApproveRequest(int id)
        {
            Issue issue = new Issue();
            DateTime dt = new DateTime();
            dt = DateTime.Now;
            issue.IssueId = id;
            issue.IssueDate = dt;
            issue.ReturnDate = dt.AddDays(15);
            issue.Status = "a";
            if (ModelState.IsValid)
            {
                _unitOfWork.Issue.Update(issue);

                _unitOfWork.Save();
                issue = _unitOfWork.Issue.Get(id);
                var bid = issue.Bookid;
                Book book = new Book();
                book = _unitOfWork.Book.Get(bid);
                book.AvailableStock = book.AvailableStock - 1;

                _unitOfWork.Book.Update(book);

                _unitOfWork.Save();

            }
            return View(issue);
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home", new { area = "Reader"});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(int id, string BookName, string Author, int Stock, int AvailableStock)
        {
            Book book = new Book();
            book.Bookid = id;
            book.BookName = BookName;
            book.Author = Author;
            book.Stock = Stock;
            book.AvailableStock = AvailableStock;

            if (ModelState.IsValid)
            {
                if(id == 0)
                {
                    _unitOfWork.Book.Add(book);
                }
                else
                {
                    _unitOfWork.Book.Update(book);
                }

                _unitOfWork.Save();
              return RedirectToAction(nameof(Index));
           }
           return View(book);
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
           var allObj = _unitOfWork.SPCall.List<Cover>(SD.Proc_Admin_Get,null);
           return Json(new { data = allObj});
        }

        #endregion

    }
}
