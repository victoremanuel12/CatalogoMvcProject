using CategoriaMVC.Models;
using CategoriaMVC.Services.Interfaces;
using System.Text;
using System.Text.Json;

namespace CategoriaMVC.Services
{
    public class CategoryService : ICategoryService
    {
        private const string apiEndpoint = "/api/1/categorias/";
        private readonly JsonSerializerOptions _options;
        private readonly IHttpClientFactory _httpClientFactory;

        private CategoryViewModel categoryViewModel;
        private IEnumerable<CategoryViewModel> categoriesViewModel;
        public CategoryService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }
        public async Task<IEnumerable<CategoryViewModel>> GetCategories()
        {
            var client = _httpClientFactory.CreateClient("CategoryApi");
            using (var response = await client.GetAsync(apiEndpoint))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    categoriesViewModel = await JsonSerializer
                              .DeserializeAsync<IEnumerable<CategoryViewModel>>
                              (apiResponse, _options);
                    return categoriesViewModel;
                }
                else
                {
                    return null;
                }
            }
        }
        public async Task<CategoryViewModel> GetCategoryById(int id)
        {
            var client = _httpClientFactory.CreateClient("CategoryApi");
            using (var response = await client.GetAsync(apiEndpoint + id))

                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    categoryViewModel = await JsonSerializer.DeserializeAsync<CategoryViewModel>(apiResponse, _options);
                    return categoryViewModel;
                }
                else
                {
                    return null;
                }
        }
        public async Task<CategoryViewModel> CreateNewCategory(CategoryViewModel categoriaVM)
        {
            var client = _httpClientFactory.CreateClient("CategoryApi");
            var categoria = JsonSerializer.Serialize(categoriaVM);
            StringContent content = new StringContent(categoria, Encoding.UTF8, "application/json");
            using (var response = await client.PostAsync(apiEndpoint, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    categoriaVM = await JsonSerializer
                                 .DeserializeAsync<CategoryViewModel>
                                 (apiResponse, _options);
                }
                else
                {
                    return null;
                }
            }
            return categoriaVM;
        }
        public async Task<bool> UpdateCategory(int id, CategoryViewModel categoriaVM)
        {
            var client = _httpClientFactory.CreateClient("CategoryApi");
            using (var response = await client.PutAsJsonAsync(apiEndpoint + id, categoriaVM))
                if (response.IsSuccessStatusCode)
                {

                    return true;
                }

            return false;
        }


        public async Task<bool> DeleteCategory(int id)
        {
            var client = _httpClientFactory.CreateClient("CategoryApi");
            using (var response = await client.DeleteAsync(apiEndpoint + id))
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            return false;
        }



    }
}
