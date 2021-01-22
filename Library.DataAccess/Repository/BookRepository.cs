using Library.DataAccess.Data;
using Library.DataAccess.Repository.IRepository;
using Library.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DataAccess.Repository
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        private readonly ApplicationDbContext _db;

        public BookRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Book book)
        {

            var objFromDb = _db.Book.FirstOrDefault(s => s.Bookid == book.Bookid);
            if (objFromDb != null)
            {
                objFromDb.BookName = book.BookName;
                objFromDb.Author = book.Author;
                objFromDb.Stock = book.Stock;
                objFromDb.AvailableStock = book.AvailableStock;
                _db.SaveChanges();
            }
        }
    }
}
