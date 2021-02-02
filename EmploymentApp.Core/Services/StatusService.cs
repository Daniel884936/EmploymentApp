using Ardalis.Result;
using EmploymentApp.Core.Entities;
using EmploymentApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmploymentApp.Core.Services
{
    public class StatusService : IStatusService
    {
        private readonly IUnitOfWork _unitOfWork;
        public StatusService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Result<IEnumerable<Status>> GetAll()
        {
            IEnumerable <Status> status;
            try
            {
                 status = _unitOfWork.StatusRepository.GetAll();
            }catch(Exception ex)
            {
                return Result<IEnumerable<Status>>.Error(new string[] { ex.Message });
            }
            var resut = Result<IEnumerable<Status>>.Success(status);
            return resut;
        }
    }
}
