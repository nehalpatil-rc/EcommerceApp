using commercetools.Sdk.Api.Models.Categories;
using commercetools.Sdk.Api.Models.Customers;

namespace EcommerceApp.Services.Interface
{
    public interface ICategoryService
    {
        Task<IList<ICategory>> GetParentCategories();

        Task<IList<ICategory>> GetCategoryByParentId(string CategoryId);
    }
}
