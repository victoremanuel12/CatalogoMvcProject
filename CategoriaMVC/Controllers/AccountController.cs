using CatalogoMVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using TccMvc.Models;

namespace CatalogoMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthentication _autenticationService;
        public AccountController(IAuthentication autenticationService)
        {
            _autenticationService = autenticationService;
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(UserViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Login Inválido");
                return View(viewModel);
            }
            var result = await _autenticationService.Authentication(viewModel);
            // adicioa o cookie na coleção de cookie do Response
            // identificar como X-Access-Token,com o valor result.Token e as opções definidas.
            Response.Cookies.Append("X-Access-Token", result.Token, new CookieOptions()
            {
                //impede que o cookie seja observado por terceiro nao autorizado.
                Secure = true,
                //impede que scripts do lado do cliente acesse o cookie
                HttpOnly = true,
                // envia o cookie apenas pelo site primario(mostrado na url)
                SameSite = SameSiteMode.Strict
            });
            return Redirect("/");
        }
    }
}
