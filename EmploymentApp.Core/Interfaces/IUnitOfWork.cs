using EmploymentApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentApp.Core.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
         IRepository<Category> CategoryRepository { get; }
         IRepository<Status> StatusRepository { get; }
         IRepository<TypeSchedule> TypeScheduleRepository { get; }
         void SaveChanges();
         Task SaveChangesAsync();
    }
}
