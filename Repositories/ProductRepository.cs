using CutFileWeb.Interfaces;
using CutFileWeb.Models;

namespace CutFileWeb.Responsitories
{
    public class ProductRepository : IProductRepository
    {
        public Task<bool> Add(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProductByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetProductsSliceAsync(int? pageIndex = null, int? pageSize = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Save()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
