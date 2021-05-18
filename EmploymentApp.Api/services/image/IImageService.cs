using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace EmploymentApp.Api.services.Image
{
    public interface IImageService
    {
        Task<string> Save(IFormFile image, string containerName);

        ///<summary>
        ///replace old img from directory 
        ///return new img url
        ///</summary>
        Task<string> Update(string oldImageUrl, IFormFile newImage, string containerName);
        Task<bool> Delete(string imageUrl, string imageContainer);
    }
}
