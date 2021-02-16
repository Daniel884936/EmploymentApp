using Ardalis.Result;
using EmploymentApp.Core.CustomEntities;
using EmploymentApp.Core.Entities;
using EmploymentApp.Core.QueryFilters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmploymentApp.Core.Interfaces
{
    public interface IUserService
    {
        Task<Result<User>> Add(User user);
        Result<PagedList<User>> GetAll(UserQueryFilter userQueryFilter);
        Task<Result<User>> GetById(int id);
        Task<Result<bool>> Update(User user);
        Task<Result<bool>> Remove(int id);
    }
}
