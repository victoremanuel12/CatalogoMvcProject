using CatalogoMVC.Services.Interfaces;
using CategoriaMVC.Models;
using System.Text;
using System.Text.Json;
using TccMvc.Models;

namespace CatalogoMVC.Services
{
    public class AuthenticationService : IAuthentication
    {
        private readonly IHttpClientFactory _clientFactory;
        const string apiEndpoint = "/api/autoriza/login/";
        private readonly JsonSerializerOptions _serializerOptions;
        private TokenViewModel tokenUser;
        public AuthenticationService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<TokenViewModel> Authentication(UserViewModel userViewModel)
        {
            var client = _clientFactory.CreateClient("AutenticaApi");
            var user =  JsonSerializer.Serialize(userViewModel);
            StringContent content = new StringContent(user, Encoding.UTF8, "application/json");
            using (var response = await client.PostAsync(apiEndpoint, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse  = await response.Content.ReadAsStreamAsync();
                    tokenUser = await JsonSerializer.DeserializeAsync<TokenViewModel>(apiResponse,_serializerOptions);
                }
                else
                {
                    return null;
                }
            }
            return tokenUser;


        }
    }
}
