using Ardalis.Result;
using EmploymentApp.Core.CustomEntities;
using EmploymentApp.Core.Entities;
using EmploymentApp.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentApp.Core.Interfaces
{
    public interface IJobService
    {
        Task<Result<Job>> Add(Job job);
        Result<PagedList<Job>> GetAll(JobQueryFilter jobQueryFilter);
        Task<Result<Job>> GetById(int id);
        Task<Result<bool>> Remove(int id);
        Task<Result<bool>> Update(Job job);
    }
}
