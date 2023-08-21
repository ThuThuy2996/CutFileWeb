using CutFileWeb.Data;
using CutFileWeb.Interfaces;
using CutFileWeb.Models;
using CutFileWeb.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CutFileWeb.Responsitories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<bool> Add(Category category)
        {
            _context.Add(category);
            return Save();
           
        }

        public Task<bool> Delete(Category category)
        {
            _context.Remove(category);
            return Save();
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.Include(o => o.Products).ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetParentsCategoriesAsync()
        {
            return await _context.Categories.Include(o => o.Products).Where(p => p.ParentId == 0).ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetCategoriesSliceAsync(int offset, int size)
        {
            return await _context.Categories.Include(o => o.Products).Skip(offset).Take(size).ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            var lst = await _context.Categories.Include(o => o.Products).FirstOrDefaultAsync(o => o.CategoryId == id);
            return lst != null ? lst : new Category();
        }

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0 ? true : false;
        }

        public Task<bool> Update(Category category)
        {
            _context.Update(category);
            return Save();
        }

        public async Task<IEnumerable<Category>> GetCategoriesByParentIdAsync(int parentId)
        {          
            return await _context.Categories.Where(p => p.ParentId != 0 && p.ParentId == parentId).ToListAsync();
        }

        public async Task<Category> GetByIdAsyncNoTracking(int id)
        {
            var lst = await _context.Categories.Include(p => p.Products).AsNoTracking().FirstOrDefaultAsync(o => o.CategoryId == id);
            return lst != null ? lst : new Category();
        }
    }
}
