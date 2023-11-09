using CategoriaMVC.Models;
using CategoriaMVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CategoriaMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryViewModel>>> Index()
        {
            var result = await _categoryService.GetCategories();
            if (result is null)
            {
                return View("Error");
            }
            return View(result);
        }
        [HttpGet]
        public IActionResult CreateNewCategory()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult<CategoryViewModel>> CreateNewCategory(CategoryViewModel category)
        {

            if (ModelState.IsValid)
            {
                var result = await _categoryService.CreateNewCategory(category);
                if (result is not null)
                {
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Error = "Errp ao criar Categoria";
            return View(category);
        }
        [HttpGet]
        public async Task<ActionResult> EditCategory(int id)
        {
            var result = await _categoryService.GetCategoryById(id);
            if (result is null)
            {
                ViewBag.Error = "Erro ao Editar Categoria";
                return View("Error");
            }
            return View(result);
        }
        [HttpPost]
        public async Task<ActionResult<CategoryViewModel>> EditCategory(CategoryViewModel category)
        {

            if (ModelState.IsValid)
            {
                bool updated = await _categoryService.UpdateCategory(category.Id, category);
                if (updated)
                {
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Error = "Erro ao criar Categoria";
            return View(category);
        }
        [HttpGet]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            CategoryViewModel category = await _categoryService.GetCategoryById(id);
            if (category is null)
            {
                return View("Error");
            }
            return View(category);
        }
        [HttpPost, ActionName("DeleteCategory")]
        public async Task<ActionResult> DeleteCategoryConfirmed(int id)
        {
            bool deleted = await _categoryService.DeleteCategory(id);
            if (deleted is true)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Error = "Erro ao excluir Categoria";
            return View("Error");
        }

    }
}
