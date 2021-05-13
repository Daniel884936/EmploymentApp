using EmploymentApp.Core.CustomEntities;
using System.Threading.Tasks;

namespace EmploymentApp.Api.Handlers.FileStorageHandler
{
    public interface IFileStorage
    {
        Task<string> Create(FileHandler fileHandler);
        Task Delete(string route, string cotainer);
    }
}
