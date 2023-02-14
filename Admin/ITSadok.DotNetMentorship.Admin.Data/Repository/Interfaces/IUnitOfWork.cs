using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ITSadok.DotNetMentorship.Admin.Data.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        Task<bool> SaveAsync();
    }
}
