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

        public async Task<bool> Delete(Product product)
        {
            _context.Remove(product);
            return await Save();
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products.Include(p => p.Category).Include(o => o.ProductBrand).ToListAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _context.Products.Where(o => o.ProductId == id).FirstOrDefaultAsync();
        }

        public async Task<Product> GetProductByIdAsyncNoTracking(int id)
        {
            return await _context.Products.Where(o => o.ProductId == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            return await _context.Products.Where(o => o.ProductName.Contains(name)).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsSliceAsync(int pageIndex, int pageSize)
        {
            return await _context.Products.Skip(pageIndex).Take(pageSize).ToListAsync();
        }

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> Update(Product product)
        {
            _context.Update(product);
            return await Save();
        }
    }
}
