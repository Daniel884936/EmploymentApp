using EmploymentApp.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmploymentApp.Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task Add(T entity);
        IEnumerable<T> GetAll();
        Task<T> GetById(int id);
        void Remove(T entity);
        void Update(T entity);
    }
}