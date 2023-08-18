using CutFileWeb.Data;
using CutFileWeb.Interfaces;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace CutFileWeb.Repositories
{
    public class UploadRepository : IUploadRepository
    {     
        private IHostingEnvironment _environment;
        public UploadRepository(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        public Task<bool> DeleteFileAsync(string publicUrl)
        {
            throw new NotImplementedException();
        }

        public async Task<string> UploadFileAsync(IFormFile fileUpload)
        {           
            string result = "";
            string folder = Path.Combine(_environment.ContentRootPath, "Uploads", "CutFiles");
           
            if (fileUpload != null)
            {

                string filePath = Path.Combine(folder, fileUpload.FileName);
               
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);                  
                }

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await fileUpload.CopyToAsync(fileStream);
                }        
                result = Path.Combine( "Uploads", "CutFiles", fileUpload.FileName);
            }
            return result;
        }

        public async Task<string> UploadImageAsync(IFormFile fileUpload)
        {           
            string result = "";
            string folder = Path.Combine(_environment.ContentRootPath, "Uploads", "Images");

            if (fileUpload != null)
            {
                string filePath = Path.Combine(folder, fileUpload.FileName);

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await fileUpload.CopyToAsync(fileStream);
                }
                result = Path.Combine("Uploads", "Images", fileUpload.FileName);
            }
            return result;
        }
    }
}
