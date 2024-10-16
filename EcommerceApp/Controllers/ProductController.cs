using EcommerceApp.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }
        public async Task<IActionResult> FeaturedProducts()
        {
            var products = await productService.GetAllPublishedProducts();
            return View(products);
        }
    }
}