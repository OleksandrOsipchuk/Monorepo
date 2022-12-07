using System;
using System.Collections.Generic;
using System.Text;

namespace ITSadok.DotNetMentorship.Admin.Data
{
    public interface IUnitOfWork
    {
        void Save();
    }
}
