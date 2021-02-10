using EmploymentApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace EmploymentApp.Core.Interfaces
{
    public interface IJobRepository: IRepository<Job>
    {
        Task<Job> GetFullJob(int jobId);
        IEnumerable<Job> GetFullJobs();
    }
}
