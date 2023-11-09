using CatalogoMVC.Services.Interfaces;
using CategoriaMVC.Models;
using CategoriaMVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CatalogoMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private string token = string.Empty;
        public ProductController(IProductService products, ICategoryService categoryService)
        {
            _productService = products;
            _categoryService = categoryService;
        }

        private string GetToken()
        {
            if (HttpContext.Request.Cookies.ContainsKey("X-Access-Token"))
            {
                token = HttpContext.Request.Cookies["X-Access-Token"].ToString();
            }
            return token;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> Index()
        {

            IEnumerable<ProductViewModel> products = await _productService.GetAllProducts(GetToken());
            if (products is null)
                return View("Error");

            return View(products);
        }
        [HttpGet]
        public async Task<ActionResult> CreateNewProduct()
        {
            IEnumerable<CategoryViewModel> categoryViewModel = await _categoryService.GetCategories();
            ViewBag.CategoryId = new SelectList(await _categoryService.GetCategories(), "Id", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult CreateNewProduct(ProductViewModel productViewModel)
        {
            var response = _productService.Create(productViewModel, GetToken());
            if (response is null)
                return View("Error");
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<ActionResult> EditProduct(int productId)
        {
            var product = await _productService.GetProductById(productId, GetToken());
            ViewBag.CategoryId = new SelectList(await _categoryService.GetCategories(), "Id", "Name");
            if (product is null)
                return View("Error");
            return View(product);
        }
       
        [HttpPost]
        public ActionResult EditProduct(ProductViewModel product)
        {
            var response = _productService.Update(product, product.Id, GetToken());
            if (response is null)
                return View("Error");
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<ActionResult> ProductDetails(int productId)
        {
            var result = await _productService.GetProductById(productId, GetToken());

            if (result is null)
                return View("Error");

            return View(result);
        }

        [HttpGet]
        public async Task<ActionResult> DeleteProduct(int productId)
        {
            var result = await _productService.GetProductById(productId, GetToken());

            if (result is null)
                return View("Error");

            return View(result);
        }
        [HttpPost]
        public async Task<ActionResult> ProductDelected(int Id)
        {
            bool response = await _productService.Delete(Id, GetToken());
            if (!response)
                return View("Error");
             return RedirectToAction("Index");
        }
        
      
    }
}
