using CutFileWeb.Models;

namespace CutFileWeb.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsSliceAsync(int pageIndex, int pageSize);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductById(int id);
        Task<Product> GetProductByIdAsyncNoTracking(int id);
        Task<IEnumerable<Product>> GetProductByName(string name);
        Task<bool> Add(Product product);
        Task<bool> Update(Product product);
        Task<bool> Delete(Product product);
        Task<bool> Save();
    }
}
