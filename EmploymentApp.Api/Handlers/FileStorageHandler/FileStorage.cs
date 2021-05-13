using EmploymentApp.Core.CustomEntities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace EmploymentApp.Api.Handlers.FileStorageHandler
{
    public class FileStorage : IFileStorage
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FileStorage(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<string> Create(FileHandler fileHandler)
        {
            var wwwrootPath = _webHostEnvironment.WebRootPath;

            if (string.IsNullOrEmpty(wwwrootPath))
                Directory.CreateDirectory("wwwroot");

            var fileFolder = Path.Combine(wwwrootPath, fileHandler.Container);

            if (!Directory.Exists(fileFolder))
                Directory.CreateDirectory(fileFolder);

            string finalName = $"{fileHandler.Name}{fileHandler.Extention}";
            string finalPath = Path.Combine(fileFolder, finalName);
            await File.WriteAllBytesAsync(finalPath, fileHandler.File);

            string currentUrl = $"{ _httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
            string dbUrl = Path.Combine(currentUrl, fileHandler.Container, finalName).Replace("\\","/");
            return dbUrl;
        }

        public Task Delete(string route, string cotainer)
        {
            var wwwrootPath = _webHostEnvironment.WebRootPath;

            if (string.IsNullOrEmpty(wwwrootPath))
                throw new Exception();

            var fileName = Path.GetFileName(route);
            var finalPath = Path.Combine(wwwrootPath, cotainer, fileName);

            if (File.Exists(finalPath))
                File.Delete(finalPath);

            return Task.CompletedTask;
        }
    }
}
