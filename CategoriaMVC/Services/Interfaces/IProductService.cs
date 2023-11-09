
using CategoriaMVC.Models;

namespace CatalogoMVC.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>> GetAllProducts(string token);
        Task<ProductViewModel> GetProductById(int id, string token);
        Task<ProductViewModel> Create(ProductViewModel productVM, string token);
        Task<bool> Update(ProductViewModel productVM,int id,string token);
        Task<bool> Delete(int id, string token);

    }
}
