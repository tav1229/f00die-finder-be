namespace f00die_finder_be.Common.FileService
{
    public interface IFileService
    {
        Task<string> UploadFileGetUrlAsync(IFormFile file);
        Task DeleteFileAsync(List<string> fileNames);

    }
}
