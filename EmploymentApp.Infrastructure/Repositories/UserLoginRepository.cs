using EmploymentApp.Core.Entities;
using EmploymentApp.Core.Interfaces;
using EmploymentApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace EmploymentApp.Infrastructure.Repositories
{
    class UserLoginRepository : BaseRepository<UserLogin>, IUserLoginRepository
    {
        public UserLoginRepository(EmploymentDbContext context) : base(context) { }
      

        public async Task<UserLogin> GetByEmail(string email)
        {
            return await _entities.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
