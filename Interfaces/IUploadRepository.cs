namespace CutFileWeb.Interfaces
{
    public interface IUploadRepository
    {
        Task<string> UploadFileAsync(IFormFile file);

        Task<string> UploadImageAsync(IFormFile file);

        void DeleteFileAsync(string publicUrl);
    }
}
