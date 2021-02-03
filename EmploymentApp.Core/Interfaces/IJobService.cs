using Ardalis.Result;
using EmploymentApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentApp.Core.Interfaces
{
    public interface IJobService
    {
        Task<Result<Job>> Add(Job job);
        Result<IEnumerable<Job>> GetAll();
        Task<Result<Job>> GetById(int id);
        Task<Result<bool>> Remove(int id);
        Task<Result<bool>> Update(Job job);
    }
}
