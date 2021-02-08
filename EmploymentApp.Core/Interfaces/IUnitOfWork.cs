using EmploymentApp.Core.Entities;
using System;
using System.Threading.Tasks;

namespace EmploymentApp.Core.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
         IRepository<Category> CategoryRepository { get; }
         IRepository<Status> StatusRepository { get; }
         IRepository<Role> RoleRepository { get; }
         IRepository<TypeSchedule> TypeScheduleRepository { get; }
         IJobRepository JobRepository { get; }
         IUserRepository UserRepository { get; }
         IUserLoginRepository UserLoginRepository { get; }

         void SaveChanges();
         Task SaveChangesAsync();
    }
}
