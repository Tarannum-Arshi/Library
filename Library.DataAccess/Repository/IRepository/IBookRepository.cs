using Library.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.DataAccess.Repository.IRepository
{
    public interface IBookRepository : IRepository<Book>
    {
        void Update(Book book);

    }
}
