using commercetools.Sdk.Api.Client;
using commercetools.Sdk.Api.Models.Products;
using EcommerceApp.Services.Interface;
using System.Collections.Generic;

namespace EcommerceApp.Services
{
    public class ProductService : IProductService
    {
        private readonly ProjectApiRoot projectApiRoot;

        public ProductService(ProjectApiRoot projectApiRoot)
        {
            this.projectApiRoot = projectApiRoot;
        }

        public async Task<IList<IProductProjection>> GetAllPublishedProducts()
        {
            try
            {
                var response = await projectApiRoot.ProductProjections().Get().ExecuteAsync();

                if (response == null)
                    throw new Exception("Received null response from the API.");

                return response.Results;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching products: " + ex.Message);
            }
        }

        public async Task<IProductProjection> GetProductById(string ProductId)
        {
            try
            {
                var response = await projectApiRoot.ProductProjections().WithId(ProductId).Get().ExecuteAsync();

                if (response == null)
                    throw new Exception("Received null response from the API.");

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching products: " + ex.Message);
            }
        }
    }



}
