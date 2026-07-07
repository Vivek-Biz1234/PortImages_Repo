using PORTIMAGES.Application.Products.DTOs;
using PORTIMAGES.Application.User.Interfaces;
using PORTIMAGES.Common.Helpers;
using System.IO.Compression;

namespace PORTIMAGES.Infrastructure.Repositories.User
{
    public class ImageDownloadRepository : IImageDownloadRepository
    {
        private readonly IUserProductRepository _productRepository;
        private readonly FileHelper _fileHelper;

        public ImageDownloadRepository(IUserProductRepository productRepository, FileHelper fileHelper)
        {
            _productRepository = productRepository;
            _fileHelper = fileHelper;
        }

        public async Task<byte[]> PrepareImagesZipAsync(List<long> productIds, int clientId)
        {
            var productsWithImages = new List<ViewProductDetailsDTO>();            
            foreach (var id in productIds)
            {
                var resp = await _productRepository.GetProductDetailsByIdAsync(id, clientId);

                if (resp.Status == 1 &&
                    resp.Data != null &&
                    resp.Data.Images != null &&
                    resp.Data.Images.Any())
                {
                    productsWithImages.Add(resp.Data);
                }
            }

            if (!productsWithImages.Any())
                return null;
        
            using var ms = new MemoryStream();

            using (var zip = new ZipArchive(ms, ZipArchiveMode.Create, true))
            {
                foreach (var product in productsWithImages)
                {
                    string terminal = SanitizeFolderName(product.TerminalName);
                    string yard = string.IsNullOrWhiteSpace(product.YardInDate)
                        ? "UnknownDate"
                        : SanitizeFolderName(product.YardInDate);

                    string chassis = SanitizeFolderName(product.ChassisNo);

                    foreach (var imgPath in product.Images)
                    {                        
                        string physicalPath = _fileHelper.GetPhysicalPath(imgPath);

                        if (!File.Exists(physicalPath))
                            continue;
                       
                        string entryPath = Path.Combine(
                            "Photos",
                            terminal,
                            yard,
                            chassis,
                            Path.GetFileName(physicalPath)
                        ).Replace("\\", "/");
            
                        zip.CreateEntryFromFile(
                            physicalPath,
                            entryPath,
                            CompressionLevel.Fastest
                        );
                    }
                }
            } 

            if (ms.Length < 1024)
                return null;

            return ms.ToArray();
        } 


        private string SanitizeFolderName(string name)
        {
            foreach (var c in Path.GetInvalidFileNameChars())
                name = name.Replace(c, '_');
            return name;
        }
    }
}
