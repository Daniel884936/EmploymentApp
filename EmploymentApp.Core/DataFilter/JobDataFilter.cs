using EmploymentApp.Core.Entities;
using EmploymentApp.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmploymentApp.Core.DataFilter
{
    public static class JobDataFilter
    {
        public static IEnumerable<Job> FilterJobs(IEnumerable<Job> jobs, JobQueryFilter jobQueryFilter)
        {
            if (!string.IsNullOrEmpty(jobQueryFilter.Search) && jobs != null)
                jobs = FilterBySearch(jobs, jobQueryFilter.Search);

            if (!string.IsNullOrEmpty(jobQueryFilter.Category) && jobs != null)
                jobs = FilterByCategory( jobs, jobQueryFilter.Category);

            if (!string.IsNullOrEmpty(jobQueryFilter.Company) && jobs != null)
                jobs = FilterByCompany(jobs, jobQueryFilter.Company);

            if (!string.IsNullOrEmpty(jobQueryFilter.Title) && jobs != null)
                jobs = FilterByTitle(jobs, jobQueryFilter.Title);

            if (!string.IsNullOrEmpty(jobQueryFilter.TypeSchedule) && jobs != null)
                jobs = FilterByTypeSchedule(jobs, jobQueryFilter.TypeSchedule);

            if (jobQueryFilter.Date != null && jobs != null)
                jobs = FilterByDate(jobs, jobQueryFilter.Date);
            
            return jobs;
        }

        private static IEnumerable<Job> FilterBySearch(IEnumerable<Job> jobs, string search)
        {
            var searchModify = search.Trim().ToLower();
            return jobs.Where(x => x.Title.ToLower().Trim().Contains(searchModify) ||
                                   x.Category.Name.ToLower().Trim().Contains(searchModify) ||
                                   x.Company.ToLower().Trim().Contains(searchModify) ||
                                   x.TypeSchedule.Name.ToLower().Trim().Contains(searchModify));
        }

        private static IEnumerable<Job> FilterByCategory(IEnumerable<Job> jobs, string category) =>
            jobs.Where(x => x.Category.Name.ToLower().Trim().Contains(category.ToLower()));
            
        private static IEnumerable<Job> FilterByCompany(IEnumerable<Job> jobs, string company) =>
            jobs.Where(x =>x.Company.ToLower().Trim().Contains(company.ToLower()));

        private static IEnumerable<Job> FilterByTitle(IEnumerable<Job> jobs, string title) =>
            jobs.Where(x => x.Title.ToLower().Trim().Contains(title.ToLower()));

        private static IEnumerable<Job> FilterByTypeSchedule(IEnumerable<Job> jobs, string typeSchedule) =>
            jobs.Where(x => x.Title.ToLower().Trim().Contains(typeSchedule.ToLower()));

        private static IEnumerable<Job> FilterByDate(IEnumerable<Job> jobs, DateTime? date) =>
            jobs.Where(x => x.Date <= date);

    }
}
