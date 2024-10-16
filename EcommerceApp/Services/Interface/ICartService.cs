using commercetools.Sdk.Api.Models.Carts;
using commercetools.Sdk.Api.Models.Products;

namespace EcommerceApp.Services.Interface
{
    public interface ICartService
    {
        Task<ICart> GetOrCreateCart();
        Task<ICart> GetCartByCartrId(string CartId);
        Task<ICart> GetCartByCustomerId(string CustomerId);
        Task<ICart> AddProductToCurrentActiveCart(ICart cart, IProductProjection product, int variantId = 1, int quantity = 1);
    }
}