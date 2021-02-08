using Ardalis.Result;
using EmploymentApp.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmploymentApp.Core.Interfaces
{
    public interface IUserService
    {
        Task<Result<User>> Add(User user);
        Result<IEnumerable<User>> GetAll();
        Task<Result<User>> GetById(int id);
        Task<Result<bool>> Update(User user);
        Task<Result<bool>> Remove(int id);
    }
}
