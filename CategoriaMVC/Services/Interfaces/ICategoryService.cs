using CategoriaMVC.Models;

namespace CategoriaMVC.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryViewModel>> GetCategories();
        Task<CategoryViewModel> GetCategoryById(int id);
        Task<CategoryViewModel> CreateNewCategory(CategoryViewModel categoriaVM);
        Task<bool> UpdateCategory(int id, CategoryViewModel categoriaVM);
        Task<bool> DeleteCategory(int id);
    }
}
