using EmploymentApp.Core.Entities;
using EmploymentApp.Core.Interfaces;
using EmploymentApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentApp.Infrastructure.Repositories
{
    public class UserRepository: BaseRepository<User>, IUserRepository
    {
        public UserRepository(EmploymentDbContext context): base(context)  {}

        public async Task<User> GetFullUser(int userId)
        {
            var fullUser = await _entities.Include(x => x.UserLogin)
                .ThenInclude(x=>x.Role).FirstOrDefaultAsync(x=>x.Id==userId); 
            return fullUser;
        }

        public IEnumerable<User> GetFullUsers()
        {
            var fullUsers =  _entities.Include(x => x.UserLogin)
                .ThenInclude(x => x.Role).AsEnumerable();
            return fullUsers;
        }
    }
}
