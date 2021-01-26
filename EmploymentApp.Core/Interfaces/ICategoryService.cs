using EmploymentApp.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmploymentApp.Core.Interfaces
{
    public interface ICategoryService
    {
        Task Add(Category category);
        IEnumerable<Category> GetAll();
        Task<Category> GetById(int id);
        Task<bool> Remove(int id);
        Task<bool> Update(Category category);
    }
}