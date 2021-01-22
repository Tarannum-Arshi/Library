using Library.DataAccess.Data;
using Library.DataAccess.Repository.IRepository;
using Library.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DataAccess.Repository
{
     public class AdminRepository : Repository<Admin>, IAdminRepository
    {
        private readonly ApplicationDbContext _db;

        public AdminRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Admin admin)
        {
            var objFromDb = _db.Admin.FirstOrDefault(s => s.UserId == admin.UserId);
            if (objFromDb != null)
            {
                objFromDb.Name = admin.Name;
                objFromDb.Phone = admin.Phone;
                objFromDb.Email = admin.Email;
                objFromDb.Password = admin.Password;
                objFromDb.Category = admin.Category;
                _db.SaveChanges();
            }
        }
    }
}
