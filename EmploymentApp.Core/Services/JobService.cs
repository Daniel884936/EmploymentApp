using Ardalis.Result;
using EmploymentApp.Core.Entities;
using EmploymentApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmploymentApp.Core.Services
{
    public class JobService: IJobService
    {
        private readonly IUnitOfWork _unitOfWork;
        public JobService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Job>> Add(Job job)
        {
            try
            {
                await _unitOfWork.JobRepository.Add(job);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Result<Job>.Error(new string[] { ex.Message });
            }
            return Result<Job>.Success(job);
        }

        public Result<IEnumerable<Job>> GetAll()
        {
            IEnumerable<Job> jobs;
            try
            {
                jobs = _unitOfWork.JobRepository.GetAll();
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<Job>>.Error(new[] { ex.Message });
            }
            var result = Result<IEnumerable<Job>>.Success(jobs);
            return result;
        }

        public async Task<Result<Job>> GetById(int id)
        {
            Job job;
            try
            {
                job = await _unitOfWork.JobRepository.GetById(id);
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
