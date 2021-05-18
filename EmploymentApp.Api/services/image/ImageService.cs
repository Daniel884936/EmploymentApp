using EmploymentApp.Api.Handlers.FileStorageHandler;
using EmploymentApp.Core.CustomEntities;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace EmploymentApp.Api.services.Image
{
    public class ImageService : IImageService
    {
        private readonly IFileStorage _fileStorage;
        public ImageService(IFileStorage fileStorage)
        {
            _fileStorage = fileStorage;
        }

        public async Task<bool> Delete(string imageUrl, string imageContainer)
        {
            try
            {
                await _fileStorage.Delete(imageUrl, imageContainer);
            }
            catch 
            {
                return false;
            }
            return true;
        }

        public async Task<string> Save(IFormFile image, string containerName)
        {
            using var stream = new MemoryStream();
            await image.CopyToAsync(stream);
            var fileBytes = stream.ToArray();
            return await _fileStorage.Create(new FileHandler
            {
                File = fileBytes,
                ContentType = image.ContentType,
                Extention = Path.GetExtension(image.FileName),
                Container = containerName,
                Name = Guid.NewGuid().ToString()
            });
        }

        public async Task<string> Update(string oldImageUrl, IFormFile newImage, string containerName)
        {
            if (!string.IsNullOrEmpty(oldImageUrl))
                await _fileStorage.Delete(oldImageUrl, containerName);
            if (newImage != null)
            {
                var imgUrl = await Save(newImage, containerName);
                return imgUrl;
            }
            return null;
        }
    }
}
