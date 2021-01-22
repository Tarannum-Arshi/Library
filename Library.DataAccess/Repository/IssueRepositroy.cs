using Library.DataAccess.Data;
using Library.DataAccess.Repository.IRepository;
using Library.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DataAccess.Repository
{
    public class IssueRepositroy : Repository<Issue>, IIssueRepository
    {
        private readonly ApplicationDbContext _db;

        public IssueRepositroy(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Issue issue)
        {

            var objFromDb = _db.Issue.FirstOrDefault(s => s.IssueId == issue.IssueId);
            if (objFromDb != null)
            {
                objFromDb.UserId = issue.UserId;
                objFromDb.Bookid = issue.Bookid;
                objFromDb.IssueDate = issue.IssueDate;
                objFromDb.ReturnDate = issue.ReturnDate;
                objFromDb.Status = issue.Status;
                _db.SaveChanges();
            }
        }
    }
}
