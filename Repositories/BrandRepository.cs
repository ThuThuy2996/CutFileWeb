using CutFileWeb.Data;
using CutFileWeb.Interfaces;
using CutFileWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace CutFileWeb.Responsitories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly ApplicationDbContext _context;
        public BrandRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(Brand brand)
        {
            _context.Add(brand);
            return await Save();
        }

        public async Task<bool> Delete(Brand brand)
        {
            _context.Remove(brand);
            return await Save();
        }

        public async Task<IEnumerable<Brand>> GetAllBrandsAsync()
        {
            return await _context.Brands.ToListAsync();
        }

        public async Task<Brand> GetBrandsByIdAsync(int id)
        {
            return await _context.Brands.FirstOrDefaultAsync(o => o.BrandId == id);
        }

        public Task<Brand> GetBrandsByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Brand>> GetSliceAsync(int offset, int size)
        {
            return await _context.Brands.Skip(offset).Take(size).ToListAsync();
        }

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0 ? true : false;
        }

        public Task<bool> Update(Brand brand)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(Brand brand)
        {
            try
            {
                _context.Update(brand);
                await Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrandsExists(brand.BrandId))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
            return true;
        }
        private bool BrandsExists(int id)
        {
            return (_context.Brands?.Any(e => e.BrandId == id)).GetValueOrDefault();
        }
    }
}
