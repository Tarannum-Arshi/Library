using System;
using System.Collections.Generic;
using System.Text;

namespace Library.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IAdminRepository Admin { get; }
        IBookRepository Book { get; }
        IIssueRepository Issue { get; }
        ISPCall SPCall { get; }

        void Save();
    }
}
