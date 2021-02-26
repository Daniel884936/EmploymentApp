using Ardalis.Result;
using EmploymentApp.Core.Entities;
using System.Threading.Tasks;

namespace EmploymentApp.Core.Interfaces
{
    public interface IUserLoginService
    {
       Task<Result<UserLogin>> GetByCredentials(UserLogin userLogin);
    }
}
