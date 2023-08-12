using CutFileWeb.Models;

namespace CutFileWeb.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsSliceAsync(int? pageIndex = null, int? pageSize = null);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductById(int id);
        Task<Product> GetProductByName(string name);
        Task<bool> Add(Product product);
        Task<bool> Update(Product product);
        Task<bool> Delete(Product product);
        Task<bool> Save();
    }
}
