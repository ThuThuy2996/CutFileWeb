using CutFileWeb.Data;
using CutFileWeb.Interfaces;
using CutFileWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace CutFileWeb.Responsitories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(Product product)
        {
            _context.Add(product);
            return await Save();
        }

        public Task<bool> Delete(Product product)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
           return await _context.Products.Include(i => i.Category).Include(p => p.Brand).ToListAsync();
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

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0 ? true : false;
        }

        public Task<bool> Update(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
