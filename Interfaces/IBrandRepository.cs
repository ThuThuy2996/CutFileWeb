using CutFileWeb.Models;

namespace CutFileWeb.Interfaces
{
    public interface IBrandRepository
    {
        Task<IEnumerable<Brand>> GetAllBrandsAsync();
        Task<IEnumerable<Brand>> GetSliceAsync(int offset, int size);
        Task<Brand> GetBrandsByIdAsync(int id);
        Task<Brand> GetBrandsByName(string name);
        Task<bool> Add(Brand brand);
        Task<bool> Update(Brand brand);
        Task<bool> Delete(Brand brand);
        Task<bool> Save();

    }
}
