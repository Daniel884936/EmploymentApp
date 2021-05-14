using EmploymentApp.Core.CustomEntities;
using EmploymentApp.Core.Entities;

namespace EmploymentApp.Infrastructure.Interfaces
{
    public interface ITokenService
    {
        Token GenerateToken(User user);
    }
}
