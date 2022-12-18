using ITSadok.DotNetMentorship.Admin.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ITSadok.DotNetMentorship.Admin.Data.Repository.Interfaces
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        Task<Student> GetByTelegramIdAsync(long TelegramIdIn);
    }
}
