using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        void Save();
    }
}
