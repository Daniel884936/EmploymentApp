using Ardalis.Result;
using EmploymentApp.Core.Entities;
using EmploymentApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmploymentApp.Core.Services
{
    public class RoleService: IRoleServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public RoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Result<IEnumerable<Role>> GetAll()
        {
            IEnumerable<Role> roles;
            try
            {
                roles = _unitOfWork.RoleRepository.GetAll();
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<Role>>.Error(new string[] { ex.Message });
            }
            var result = Result<IEnumerable<Role>>.Success(roles);
            return result;
        }
    }
}
