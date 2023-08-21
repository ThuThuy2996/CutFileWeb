using CutFileWeb.Models;

namespace CutFileWeb.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategoriesSliceAsync(int offset, int size);
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<IEnumerable<Category>> GetParentsCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(int id);
        Task<Category> GetByIdAsyncNoTracking(int id);
        Task<IEnumerable<Category>> GetCategoriesByParentIdAsync(int parentId);
        Task<bool> Add(Category category);
        Task<bool> Update(Category category);
        Task<bool> Delete(Category category);
        Task<bool> Save();
    }
}
