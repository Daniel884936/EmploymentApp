using Ardalis.Result;
using EmploymentApp.Core.CustomEntities;
using EmploymentApp.Core.DataFilter;
using EmploymentApp.Core.Entities;
using EmploymentApp.Core.Interfaces;
using EmploymentApp.Core.QueryFilters;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmploymentApp.Core.Services
{
    public class JobService: IJobService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        public JobService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _paginationOptions = options.Value;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Job>> Add(Job job)
        {
            try
            {
                job.Date = DateTime.Now;
                await _unitOfWork.JobRepository.Add(job);
                await _unitOfWork.SaveChangesAsync();
                job = await _unitOfWork.JobRepository.GetFullJob(job.Id);
            }
            catch (Exception ex)
            {
                return Result<Job>.Error(new string[] { ex.Message });
            }
            return Result<Job>.Success(job);
        }

        public Result<PagedList<Job>> GetAll(JobQueryFilter filter)
        {
            IEnumerable<Job> jobs;
            PagedList<Job> pagedJobs = null;
            try
            {
                jobs = _unitOfWork.JobRepository.GetFullJobs();
                if (jobs != null)
                    jobs = JobDataFilter.FilterJobs(jobs, filter);

                if (jobs != null)
                {
                    filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
                    filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

                    pagedJobs = PagedList<Job>.Create(jobs,
                        filter.PageNumber, filter.PageSize);
                }
            }
            catch (Exception ex)
            {
                return Result<PagedList<Job>>.Error(new[] { ex.Message });
            }
            var result = Result<PagedList<Job>>.Success(pagedJobs);
            return result;
        }

        public async Task<Result<Job>> GetById(int id)
        {
            Job job;
            try
            {
                job = await _unitOfWork.JobRepository.GetFullJob(id);
            }
            catch (Exception ex)
            {
                return Result<Job>.Error(new[] { ex.Message });
            }
            var result = Result<Job>.Success(job);
            return result;
        }

        public async Task<Result<bool>> Remove(int id)
        {
            try
            {
                var job = await _unitOfWork.JobRepository.GetById(id);
                if (job == null)
                    return Result<bool>.NotFound();
                _unitOfWork.JobRepository.Remove(job);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Result<bool>.Error(new[] { ex.Message });
            }
            var result = Result<bool>.Success(true);
            return result;
        }

        public async Task<Result<bool>> Update(Job job)
        {
            try
            {
                var jobTraking = await _unitOfWork.JobRepository.GetById(job.Id);
                if (jobTraking == null)
                    return Result<bool>.NotFound();
                SetJobToUpdate(jobTraking, job);
                _unitOfWork.JobRepository.Update(jobTraking);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Result<bool>.Error(new[] { ex.Message });
            }
            var result = Result<bool>.Success(true);
            return result;
        }

        private void SetJobToUpdate(Job jobToUpdate ,Job job)
        {
            jobToUpdate.CategoryId = job.CategoryId;
            jobToUpdate.Description = job.Description;
            jobToUpdate.Img = job.Img;
            jobToUpdate.Lat = job.Lat;
            jobToUpdate.Long = job.Long;
            jobToUpdate.StatusId = job.StatusId;
            jobToUpdate.Title = job.Title;
            jobToUpdate.Company = job.Company;
            jobToUpdate.TypeScheduleId = job.TypeScheduleId;
        }
    }
}
