using EcommerceApp.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public async Task<IActionResult> ParentCategoryList()
        {
            var categories = await categoryService.GetParentCategories();
            return View("CategoryList", categories);
        }

        public async Task<IActionResult> ChildCategoryList(string CategoryId)
        {
            var categories = await categoryService.GetCategoryByParentId(CategoryId);
            return View("CategoryList", categories);
        }
    }
}
