using EmploymentApp.Core.Entities;
using EmploymentApp.Core.Interfaces;
using EmploymentApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmploymentApp.Infrastructure.Repositories
{
    public class JobRepository: BaseRepository<Job>, IJobRepository
    {
        public JobRepository(EmploymentDbContext context) :base(context) {}

        public IQueryable<Job> GetFullJobs()
        {
            var fullJobs = _entities.Include(x => x.Category)
                .Include(x => x.Status)
                .Include(x => x.TypeSchedule).OrderByDescending(x=>
                x.Date <= DateTime.Now).AsQueryable();
            return fullJobs;
        }

        public async Task<Job> GetFullJob(int jobId)
        {
            var fullJob = await _entities.Include(x => x.Category)
                .Include(x => x.Status)
                .Include(x => x.TypeSchedule).FirstOrDefaultAsync(x => x.Id == jobId);
            return fullJob;
        }
    }
}
