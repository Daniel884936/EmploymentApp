using EmploymentApp.Core.Entities;
using System.Threading.Tasks;

namespace EmploymentApp.Core.Interfaces
{
    public interface IUserLoginRepository: IRepository<UserLogin>
    {
        Task< UserLogin> GetByEmail(string email);
    }
}
