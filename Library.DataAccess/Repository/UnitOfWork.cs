using Library.DataAccess.Data;
using Library.DataAccess.Repository.IRepository;
using Library.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.DataAccess.Repository
{
    public class UnitOfWork :IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Book = new BookRepository(_db);
            Admin = new AdminRepository(_db);
            Issue = new IssueRepositroy(_db);
            SPCall = new SPCall(_db);
        }


        public IAdminRepository Admin { get; private set; }
        public IBookRepository Book { get; private set;  }
        public IIssueRepository Issue { get; private set; }

        public ISPCall SPCall { get;  private set; }


        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
