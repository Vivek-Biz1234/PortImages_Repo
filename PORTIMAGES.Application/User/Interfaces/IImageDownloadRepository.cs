namespace PORTIMAGES.Application.User.Interfaces
{
    public interface IImageDownloadRepository
    {
        Task<byte[]> PrepareImagesZipAsync(List<long> productIds, int clientId);
    }
}
