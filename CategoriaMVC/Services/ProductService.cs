
using CatalogoMVC.Services.Interfaces;
using CategoriaMVC.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace CatalogoMVC.Services
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const string endpoint = "api/1/produtos";
        private readonly JsonSerializerOptions _serializerOptions;
        private ProductViewModel _productViewModel;
        private IEnumerable<ProductViewModel> _productsVM;
        public ProductService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }
        private static void PutTokenbInHeaderAutorization(string token, HttpClient client)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        public async Task<IEnumerable<ProductViewModel>> GetAllProducts(string token)
        {
            try
            {

                var client = _httpClientFactory.CreateClient("ProductApi");
                PutTokenbInHeaderAutorization(token, client);
                using (var response = await client.GetAsync(endpoint))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var apiResponse = await response.Content.ReadAsStreamAsync();
                        _productsVM = await JsonSerializer.DeserializeAsync<IEnumerable<ProductViewModel>>(apiResponse, _serializerOptions);
                    }
                    else
                    {
                        return null;
                    }
                }
                return _productsVM;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }

        }

        public async Task<ProductViewModel> GetProductById(int id, string token)
        {
            var client = _httpClientFactory.CreateClient("ProductApi");
            PutTokenbInHeaderAutorization(token, client);
            using (var response = await client.GetAsync($"{endpoint}/{id}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStreamAsync();
                    _productViewModel = await JsonSerializer.DeserializeAsync<ProductViewModel>(responseJson);

                }
                else
                {
                    return null;
                }

            }
            return _productViewModel;
        }
        public async Task<ProductViewModel> Create(ProductViewModel productVM, string token)
        {
            var client = _httpClientFactory.CreateClient("ProductApi");
            PutTokenbInHeaderAutorization(token, client);
            string product = JsonSerializer.Serialize(productVM);
            StringContent content = new StringContent(product, Encoding.UTF8, "application/json");
            using (var response = await client.PostAsync(endpoint, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    _productViewModel = await JsonSerializer.DeserializeAsync<ProductViewModel>(apiResponse, _serializerOptions);
                }
                else
                {
                    return null;
                }
            }
            return _productViewModel;
        }
        public async Task<bool> Update(ProductViewModel productVM, int id, string token)
        {
            var client = _httpClientFactory.CreateClient("ProductApi");
            PutTokenbInHeaderAutorization(token, client);

            using (var response = await client.PutAsJsonAsync($"{endpoint}/{id}", productVM))
            {
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<bool> Delete(int id, string token)
        {
            var client = _httpClientFactory.CreateClient("ProductApi");
            PutTokenbInHeaderAutorization(token, client);

            using (var response = await client.DeleteAsync($"{endpoint}/{id}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


    }
}
