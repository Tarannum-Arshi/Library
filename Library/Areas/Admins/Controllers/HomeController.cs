using Library.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Library.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

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
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Book.Get(id);
            _unitOfWork.Book.Remove( id);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));

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

        
        #endregion

    }
}
