using commercetools.Sdk.Api.Models.Customers;
using EcommerceApp.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ICustomerService customerService;
     

        public AccountController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(CustomerDraft customerDraft)
        {
            try
            {
                var customer = await customerService.AddCustomer(customerDraft);

                var customerSignin = new CustomerSignin
                {
                    Email = customerDraft.Email,
                    Password = customerDraft.Password 
                };
                var CustomerSigninResult = await customerService.Signin(customerSignin);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex )
            {
                string errorMessage = ex.Message;
                return RedirectToAction("Error", new { message = errorMessage });
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(CustomerSignin customerSignin, string returnUrl = null)
        {
            try
            {
                var CustomerSigninResult = await customerService.Signin(customerSignin);        
              
                return Redirect(returnUrl ?? Url.Action("Index", "Home"));
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                return RedirectToAction("Error", new { message = errorMessage });
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); 
            return RedirectToAction("Login");
        }

        public IActionResult Details()
        {
            TempData["Email"] = HttpContext.Session.GetString("CustomerEmail");
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Error(string message)
        {
            ViewBag.ErrorMessage = message ?? "An unexpected error occurred. Please try again.";
            return View();
        }
    }
}