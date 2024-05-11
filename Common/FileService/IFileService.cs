namespace f00die_finder_be.Common.FileService
{
    public interface IFileService
    {
        Task<string> UploadImageAsync(IFormFile file);
    }
}
