using Ardalis.Result;
using EmploymentApp.Core.Entities;
using EmploymentApp.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace EmploymentApp.Core.Services
{
    public class UserLoginService : IUserLoginService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserLoginService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<UserLogin>> GetByCredentials(UserLogin userLogin)
        {
            try
            {
                userLogin = await _unitOfWork.UserLoginRepository
                    .GetFullUserLoginByCredentials(userLogin.Email, userLogin.Password);
            }
            catch (Exception ex)
            {
                return Result<UserLogin>.Error(new[] { ex.Message });
            }
            var result = Result<UserLogin>.Success(userLogin);
            return result;
        }
    }
}
