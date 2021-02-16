using EmploymentApp.Core.QueryFilters;
using Microsoft.AspNetCore.Http;

namespace EmploymentApp.Infrastructure.Interfaces
{
    public interface IUriService
    {
         string GetPaginationNextUrl(BaseQueryFilter filter, HttpRequest Request, bool hasNextPage);
         string GetPaginationPreviousUrl(BaseQueryFilter filter, HttpRequest request, bool hasPreviousPage);
    }
}
