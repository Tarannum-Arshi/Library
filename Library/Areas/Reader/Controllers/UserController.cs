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
            string claimvalue = User.FindFirst("id").Value;
            int value = Convert.ToInt32(claimvalue);
            Issue issue = new Issue();
            issue = _unitOfWork.Issue.GetFirstOrDefault(i => i.UserId == value && i.Status == "a");
            return Json(new { data = issue });
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
                return View(book);
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
            Issue issue = new Issue();
            Book book = new Book();
            book = _unitOfWork.Book.Get(id);
            book.AvailableStock = book.AvailableStock + 1;
            if (ModelState.IsValid)
            {
                _unitOfWork.Issue.Remove(id);

                _unitOfWork.Save();

                _unitOfWork.Book.Update(book);

                _unitOfWork.Save();

                return RedirectToAction(nameof(Index));
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
        public IActionResult G
        #endregion
    }
}
