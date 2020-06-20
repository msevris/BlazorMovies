using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BlazorMovies.Server.Helpers
{
    public class InAppStorageService : IFileStorageService
    {
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public InAppStorageService(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            _env = env;
            _httpContextAccessor = httpContextAccessor;
        }

        public Task DeleteFile(string fileRoute, string containerName)
        {
            // extract the fileName and the rootDirectory
            var fileName = Path.GetFileName(fileRoute);
            string fileDirectory = Path.Combine(_env.WebRootPath, containerName, fileName);
            if (File.Exists(fileDirectory))
            {
                File.Delete(fileDirectory);
            }

            // bcs we dont do async return FromResult(0)
            return Task.FromResult(0);
        }

        public async Task<string> EditFile(byte[] content, string extension, string containerName, string fileRoute)
        {
            if (!string.IsNullOrEmpty(fileRoute))
            {
                await DeleteFile(fileRoute, containerName);
            }

            return await SaveFile(content, extension, containerName);
        }

        public async Task<string> SaveFile(byte[] content, string extension, string containerName)
        {
            var fileName = $"{Guid.NewGuid()}.{extension}";
            string folder = Path.Combine(_env.WebRootPath, containerName);

            // if directory does NOT exist? => create
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string savingPath = Path.Combine(folder, fileName);
            await File.WriteAllBytesAsync(savingPath, content);

            // generate the URL to store into the DB
            var currentUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";

            // path for DB
            var pathForDatabase = Path.Combine(currentUrl, containerName, fileName);

            return pathForDatabase;
        }
    }
}
