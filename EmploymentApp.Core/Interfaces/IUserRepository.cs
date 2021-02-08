using EmploymentApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentApp.Core.Interfaces
{
    public interface IUserRepository: IRepository<User>
    {
        Task<User> GetFullUser(int userId);
        IEnumerable<User> GetFullUsers();
    }
}
