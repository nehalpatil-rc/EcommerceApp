using commercetools.Base.Client.Error;
using commercetools.Sdk.Api.Client;
using commercetools.Sdk.Api.Models.Carts;
using commercetools.Sdk.Api.Models.Customers;
using commercetools.Sdk.Api.Models.Products;
using EcommerceApp.Services.Interface;
using Microsoft.AspNetCore.Http;

namespace EcommerceApp.Services
{
    public class CartService : ICartService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ProjectApiRoot projectApiRoot;

        public CartService(ProjectApiRoot projectApiRoot, IHttpContextAccessor httpContextAccessor)
        {
            this.projectApiRoot = projectApiRoot;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<ICart> AddProductToCurrentActiveCart(ICart cart, IProductProjection product, int variantId = 1, int quantity = 1)
        {
            var myCartUpdate = new CartUpdate
            {
                Version = cart.Version,
                Actions = new List<ICartUpdateAction>
                {
                    new CartAddLineItemAction
                    {
                        ProductId = product.Id,
                        VariantId = variantId,
                        Quantity = quantity
                    }
                }
            };
            var updatedCart = await projectApiRoot.Carts().WithId(cart.Id).Post(myCartUpdate).ExecuteAsync();
            return updatedCart;
        }

        public async Task<ICart> GetOrCreateCart()
        {
            var httpContext = httpContextAccessor.HttpContext;
            var CustomerEmail = httpContext.Session.GetString("CustomerEmail");
            var CustomerId = httpContext.Session.GetString("CustomerId");

            var cart = await GetCartByCustomerId(CustomerId) ?? await projectApiRoot.Carts().Post(
                    new CartDraft
                    {
                        CustomerId = CustomerEmail,
                        CustomerEmail = CustomerEmail,
                        Currency = "EUR",
                        Country = "DE",
                    }).ExecuteAsync();
            return cart;
        }

        public async Task<ICart> GetCartByCartrId(string CartId)
        {
            ICart cart = null;
            try
            {
                cart = await projectApiRoot.Carts().WithId(CartId).Get().ExecuteAsync();
            }
            catch (NotFoundException ed)
            {
            }
            catch (Exception ex)
            {
            }
            return cart;
        }

        public async Task<ICart> GetCartByCustomerId(string CustomerId)
        {
            ICart cart = null;
            try
            {
                cart = await projectApiRoot.Carts().WithCustomerId(CustomerId).Get().ExecuteAsync();
            }
            catch (NotFoundException ed)
            {
            }
            catch (Exception ex)
            {
            }
            return cart;
        }
    }
}