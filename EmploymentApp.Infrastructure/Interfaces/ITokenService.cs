using EmploymentApp.Core.Entities;

namespace EmploymentApp.Infrastructure.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
