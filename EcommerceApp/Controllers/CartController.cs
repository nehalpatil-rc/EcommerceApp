using EcommerceApp.Models;
using EcommerceApp.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.Controllers
{
    public class CartController : Controller
    {

        private readonly ICartService cartService;

        private readonly IProductService productService;

        private readonly ICustomerService customerService;
        public CartController(ICartService cartService, IProductService productService, ICustomerService customerService)
        {
            this.cartService = cartService;
            this.productService = productService;
            this.customerService = customerService;
        }
        public async Task<IActionResult> GetCart()
        {
            var customerKey = HttpContext.Session.GetString("CustomerKey");
            if (string.IsNullOrEmpty(customerKey))
            {
                // Handle unauthenticated user
                return RedirectToAction("Login", "Account");
            }
            var cart = await cartService.GetOrCreateCart();
            return View(cart.LineItems);
        }

        public async Task<IActionResult> AddToCart(string productId)
        {
            var customerKey = HttpContext.Session.GetString("CustomerKey");
            if (string.IsNullOrEmpty(customerKey))
            {
                // Handle unauthenticated user
                return RedirectToAction("Login", "Account");
            }
            var cart = await cartService.GetOrCreateCart();

            var product= await productService.GetProductById(productId);

            var result = await cartService.AddProductToCurrentActiveCart(cart, product);
            return View("Cart");
        }
    }
}
