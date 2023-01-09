using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ITSadok.DotNetMentorship.Admin.Data.Repository.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task PatchAsync(int Id, JsonPatchDocument obj);
        void InsertAsync(T obj);
        void Update(T obj);
        void Delete(T obj);
    }
}

