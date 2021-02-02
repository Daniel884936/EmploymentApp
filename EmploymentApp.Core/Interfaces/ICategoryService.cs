using EmploymentApp.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.Result;

namespace EmploymentApp.Core.Interfaces
{
    public interface ICategoryService
    {
        Task<Result<Category>> Add(Category category);
        Result<IEnumerable<Category>> GetAll();
        Task<Result<Category>> GetById(int id);
        Task<Result<bool>> Remove(int id);
        Task<Result<bool>> Update(Category category);
    }
}