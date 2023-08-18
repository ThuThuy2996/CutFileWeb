namespace CutFileWeb.Interfaces
{
    public interface IUploadRepository
    {
        Task<string> UploadFileAsync(IFormFile file);

        Task<string> UploadImageAsync(IFormFile file);

        Task<bool> DeleteFileAsync(string publicUrl);
    }
}
