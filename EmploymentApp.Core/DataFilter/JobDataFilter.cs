using EmploymentApp.Core.Entities;
using EmploymentApp.Core.QueryFilters;
using System;
using System.Linq;

namespace EmploymentApp.Core.DataFilter
{
    public static class JobDataFilter
    {
        static public IQueryable<Job> FilterJobs(JobQueryFilter jobQueryFilter, IQueryable<Job> jobs)
        {
            if (jobQueryFilter.Category != null)
                jobs = FilterByCategory( jobs, jobQueryFilter.Category);

            if (jobQueryFilter.Company != null && jobs != null)
                jobs = FilterByCompany(jobs, jobQueryFilter.Company);

            if (jobQueryFilter.Title != null && jobs != null)
                jobs = FilterByTitle(jobs, jobQueryFilter.Title);

            if (jobQueryFilter.TypeSchedule != null && jobs != null)
                jobs = FilterByTypeSchedule(jobs, jobQueryFilter.TypeSchedule);

            if (jobQueryFilter.Date != null && jobs != null)
                jobs = FilterByDate(jobs, jobQueryFilter.Date);
            
            return jobs;
        }

        private static IQueryable<Job> FilterByCategory(IQueryable<Job> jobs, string category) =>
            jobs.Where(x => x.Category.Name.ToLower().Contains(category.ToLower()));
            
        private static IQueryable<Job> FilterByCompany(IQueryable<Job> jobs, string company) =>
            jobs.Where(x =>x.Company.ToLower().Contains(company.ToLower()));

        private static IQueryable<Job> FilterByTitle(IQueryable<Job> jobs, string title) =>
            jobs.Where(x => x.Title.ToLower().Contains(title.ToLower()));

        private static IQueryable<Job> FilterByTypeSchedule(IQueryable<Job> jobs, string typeSchedule) =>
            jobs.Where(x => x.Title.ToLower().Contains(typeSchedule.ToLower()));

        private static IQueryable<Job> FilterByDate(IQueryable<Job> jobs, DateTime? date) =>
            jobs.Where(x => x.Date <= date);


    }
}
