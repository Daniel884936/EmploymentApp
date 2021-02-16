using EmploymentApp.Core.QueryFilters;
using EmploymentApp.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;

namespace EmploymentApp.Infrastructure.Serices
{
    public class UriService: IUriService
    {
        private readonly string _baseUrl;
        private string modifiedUri;
        public UriService(string baseUrl)
        {
            _baseUrl = baseUrl;
        }
        public string GetPaginationNextUrl(BaseQueryFilter filter, HttpRequest request, bool hasNextPage)
        {
            
            if (hasNextPage)
            {
                modifiedUri =  GetCustomUrl(filter, request, true);
                return modifiedUri;
            }
            return null;
        }

        public string GetPaginationPreviousUrl(BaseQueryFilter filter, HttpRequest request, bool hasPreviousPage)
        {
            if (hasPreviousPage)
            {
                modifiedUri =  GetCustomUrl(filter, request, false);
                return modifiedUri;
            }
            return null;
        }

        //this is a dynamic function to create a custom url 
        private string GetCustomUrl(BaseQueryFilter filter, HttpRequest request, bool addOnePageItem)
        {
            var baseRouteUrl = $"{ _baseUrl}{ request.Path.Value}";
            var modifiedUri = baseRouteUrl;
            if (addOnePageItem)
            {
                modifiedUri = QueryHelpers.AddQueryString(modifiedUri, nameof(filter.PageNumber), (filter.PageNumber + 1).ToString());
            }
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, nameof(filter.PageNumber), (filter.PageNumber - 1).ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, nameof(filter.PageSize), (filter.PageSize).ToString());
            foreach (var item in request.Query)
            {
                //Jump if it has page number or page size from query
                if (item.Key == nameof(filter.PageNumber) || item.Key == nameof(filter.PageSize))
                    continue;

                modifiedUri = QueryHelpers.AddQueryString(modifiedUri, item.Key, item.Value);
            }
            return modifiedUri;
        }
    }
}
