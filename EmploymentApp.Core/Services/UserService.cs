using Ardalis.Result;
using EmploymentApp.Core.Entities;
using EmploymentApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using EmploymentApp.Core.QueryFilters;
using EmploymentApp.Core.DataFilter;

namespace EmploymentApp.Core.Services
{
    public class UserService: IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<User>> Add(User user)
        {
            try
            {
                var userLogin = user.UserLogin.ElementAt(0);
                var userLoginDb = await _unitOfWork.UserLoginRepository.GetByEmail(userLogin.Email);
                if (userLoginDb != null) {
                    return Result<User>.Invalid(new List<ValidationError> { 
                        new ValidationError { ErrorMessage = "User already exist"} 
                    });
                }
                await _unitOfWork.UserRepository.Add(user);
                await _unitOfWork.SaveChangesAsync();
                //var userRole = await _unitOfWork.RoleRepository.GetById(userLogin.RoleId);
                //userLogin.Role = userRole;
                userLogin.Role = new Role {  
                    Id = userLogin.RoleId, Name = UserRole(userLogin.RoleId)
                };
            }
            catch (Exception ex)
            {
                return Result<User>.Error(new string[] { ex.Message });
            }
            return Result<User>.Success(user);
        }

        public Result<IEnumerable<User>> GetAll(UserQueryFilter userQueryFilter)
        {
            IEnumerable<User> users;
            try
            {
                users = _unitOfWork.UserRepository.GetFullUsers();
                if (users != null)
                {
                    users = UserDataFilter.FilterUsers(users, userQueryFilter);
                }
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<User>>.Error(new[] { ex.Message });
            }
            var result = Result<IEnumerable<User>>.Success(users);
            return result;
        }

        public async Task<Result<User>> GetById(int id)
        {
            User user;
            try
            {
                user = await _unitOfWork.UserRepository.GetFullUser(id);
            }
            catch (Exception ex)
            {
                return Result<User>.Error(new[] { ex.Message });
            }
            var result = Result<User>.Success(user);
            return result;
        }

        public async Task<Result<bool>> Remove(int id)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.GetById(id);
                if (user == null)
                    return Result<bool>.NotFound();
                _unitOfWork.UserRepository.Remove(user);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Result<bool>.Error(new[] { ex.Message });
            }
            var result = Result<bool>.Success(true);
            return result;
        }

        public async Task<Result<bool>> Update(User user)
        {
            try
            {
                var userTraking = await _unitOfWork.UserRepository.GetById(user.Id);
                if (userTraking == null)
                    return Result<bool>.NotFound();
                _unitOfWork.UserRepository.Update(userTraking);
                SetUserToUpdate(userTraking, user);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Result<bool>.Error(new[] { ex.Message });
            }
            var result = Result<bool>.Success(true);
            return result;
        }

        private void SetUserToUpdate(User userToUpdate, User user)
        {
            userToUpdate.Bithdate = user.Bithdate;
            userToUpdate.Name = user.Name;
            userToUpdate.Surnames = user.Surnames;
        }

        private string UserRole(int roleId)
        {
            Dictionary<int, string> roles = new Dictionary<int, string>
            {
                {1,"Admin"},{2,"Poster"},{3,"User" }
            };
            return roles[roleId];
        }
    }
}
