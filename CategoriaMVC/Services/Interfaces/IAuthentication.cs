using CategoriaMVC.Models;
using TccMvc.Models;

namespace CatalogoMVC.Services.Interfaces
{
    public interface IAuthentication
    {
        Task<TokenViewModel> Authentication(UserViewModel userViewModel);
    }
}
