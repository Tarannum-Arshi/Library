using Library.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.DataAccess.Repository.IRepository
{
    public interface IIssueRepository :IRepository<Issue>
    {
        void Update(Issue issue);

    }
}
