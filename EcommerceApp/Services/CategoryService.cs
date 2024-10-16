using commercetools.Sdk.Api.Client;
using commercetools.Sdk.Api.Models.Categories;
using EcommerceApp.Services.Interface;

namespace EcommerceApp.Services
{
    public class CategoryService : ICategoryService
    {

        private readonly ProjectApiRoot projectApiRoot;

        public CategoryService(ProjectApiRoot projectApiRoot)
        {
            this.projectApiRoot = projectApiRoot;
        }

        public async Task<IList<ICategory>> GetParentCategories()
        {
            var response = await projectApiRoot.Categories().Get().WithWhere("parent is not defined").ExecuteAsync();

            return response.Results;
        }

        public async Task<IList<ICategory>> GetCategoryByParentId(string CategoryId)
        {
            var response = await projectApiRoot.Categories()
                    .Get()
                    .WithWhere($"parent(id=\"{CategoryId}\")")
                    .ExecuteAsync();

            return response.Results;
        }
    }
}