using Ardalis.Result;
using EmploymentApp.Core.Entities;
using EmploymentApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmploymentApp.Core.Services
{
    public class TypeScheduleService: ITypeScheduleService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TypeScheduleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Result<IEnumerable<TypeSchedule>> GetAll()
        {
            IEnumerable<TypeSchedule> typeSchedule;
            try
            {
                typeSchedule = _unitOfWork.TypeScheduleRepository.GetAll();
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<TypeSchedule>>.Error(new string[] { ex.Message });
            }
            var result = Result<IEnumerable<TypeSchedule>>.Success(typeSchedule);
            return result;
        }
    }
}
