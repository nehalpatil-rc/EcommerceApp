using commercetools.Sdk.Api.Models.Products;

namespace EcommerceApp.Services.Interface
{
    public interface IProductService
    {
        Task<IList<IProductProjection>> GetAllPublishedProducts();
        Task<IProductProjection> GetProductById(string ProductId);
    }
}