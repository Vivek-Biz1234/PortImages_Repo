using Microsoft.AspNetCore.Http;

namespace PORTIMAGES.Common.Helpers
{
    public class FileHelper
    {
        private readonly string _rootPath; // e.g., wwwroot path

        public FileHelper(string rootPath)
        {
            _rootPath = rootPath;
        }

        /// <summary>
        /// Save file to disk with dynamic folder and filename
        /// </summary>
        /// <param name="file">IFormFile from frontend</param>
        /// <param name="folderName">subfolder name (e.g., "MakerLogos")</param>
        /// <param name="allowedExtensions">e.g., ".jpg,.png"</param>
        /// <param name="maxSizeMB">max size in MB</param>
        /// <param name="returnFullPath">true=return relative path, false=filename only</param>
        /// <returns>saved file path or null if failed</returns>
        public async Task<string?> SaveFileAsync(IFormFile file,
            string folderName, string allowedExtensions = ".jpg,.png,.jpeg",
            int maxSizeMB = 5, bool returnFullPath = true)
        {
            if (file == null || file.Length == 0)
                return null;

            // Validate extension
            var ext = Path.GetExtension(file.FileName).ToLower();
            if (!allowedExtensions.Split(',').Contains(ext))
                throw new Exception("Invalid file type");

            // Validate size
            if (file.Length > maxSizeMB * 1024 * 1024)
                throw new Exception($"File size cannot exceed {maxSizeMB} MB");

            // Create folder if not exist
            var folderPath = Path.Combine(_rootPath, folderName);
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            // Generate unique filename
            var fileName = $"{Guid.NewGuid()}{ext}";
            var fullPath = Path.Combine(folderPath, fileName);

            // Save file
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return returnFullPath
                ? "/" + Path.Combine(folderName, fileName).Replace("\\", "/") // relative path
                : fileName;
        }

        /// <summary>
        /// Deletes a file safely from wwwroot
        /// </summary>
        public bool DeleteFile(string? relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath))
                return false;

            // Remove leading slash if exists
            relativePath = relativePath.TrimStart('/');

            string fullPath = Path.Combine(_rootPath, relativePath);

            if (!File.Exists(fullPath))
                return false;

            File.Delete(fullPath);
            return true;
        }

        public string GetPhysicalPath(string relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath))
                return string.Empty;

            relativePath = relativePath.TrimStart('/', '\\');
            return Path.Combine(_rootPath, relativePath);
        }

        public async Task<string?> SaveFileFromUrlAsync(
     string fileUrl,
     string folderName)
        {
            if (string.IsNullOrWhiteSpace(fileUrl))
                return null;

            try
            {
                using var httpClient = new HttpClient();

                var response = await httpClient.GetAsync(fileUrl);

                if (!response.IsSuccessStatusCode)
                    return null;

                var bytes = await response.Content.ReadAsByteArrayAsync();

                var ext = Path.GetExtension(fileUrl);
                var fileName = Path.GetFileName(fileUrl);

                var folderPath = Path.Combine(_rootPath, folderName);

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                var destinationPath = Path.Combine(folderPath, fileName);
                 
                if (!File.Exists(destinationPath))
                {
                    await File.WriteAllBytesAsync(destinationPath, bytes);
                }

                return "/" + Path.Combine(folderName, fileName).Replace("\\", "/");
            }
            catch
            {
                return null;
            }
        }

    }
}
