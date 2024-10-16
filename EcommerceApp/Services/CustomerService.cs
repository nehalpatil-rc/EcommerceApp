using commercetools.Base.Client.Error;
using commercetools.Sdk.Api.Client;
using commercetools.Sdk.Api.Models.Customers;
using EcommerceApp.Services.Interface;
using Microsoft.AspNetCore.Http;
namespace EcommerceApp.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ProjectApiRoot projectApiRoot;
        private readonly IHttpContextAccessor httpContextAccessor;
        public CustomerService(ProjectApiRoot projectApiRoot, IHttpContextAccessor httpContextAccessor)
        {
            this.projectApiRoot = projectApiRoot;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<ICustomerPagedQueryResponse> GetAllCustomer()
        {
            try
            {
                var response = await projectApiRoot.Customers().Get().ExecuteAsync();

                if (response == null)
                {
                    throw new Exception("Received null response from the API.");
                }

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching products: " + ex.Message);
            }
        }

        public async Task<ICustomerSignInResult> AddCustomer(CustomerDraft customerDraft)
        {
            try
            {

                if (IsCustomerExists(customerDraft)?.Result.Count >= 1)
                {
                    throw new Exception("Customer already exist");
                }

                ICustomerSignInResult response = await projectApiRoot.Customers().Post(customerDraft).ExecuteAsync();
                if (response == null)
                {
                    throw new Exception("Received null response from the API.");
                }
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while Registarion of customer: " + ex.Message);
            }

        }

        public async Task<ICustomerPagedQueryResponse> IsCustomerExists(CustomerDraft customerDraft)
        {
            try
            {
                var response = await projectApiRoot.Customers()
                             .Get()
                             .WithQuery(c =>
                                 c.Email().Is(customerDraft.Email).Or(c.Key().Is(customerDraft.Key)))
                             .ExecuteAsync();

                if (response == null)
                {
                    throw new Exception("Received null response from the API.");
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ICustomer> GetCustomerBykey(string key)
        {
            try
            {
                var response = await projectApiRoot.Customers()
                        .WithKey(key).Get().ExecuteAsync();

                if (response == null)
                {
                    throw new Exception("Received null response from the API.");
                }
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ICustomerSignInResult> Signin(CustomerSignin customerSignin)
        {
            try
            {
                var response = await projectApiRoot.Login().Post(new CustomerSignin
                {
                    Email = customerSignin.Email,
                    Password = customerSignin.Password
                }).ExecuteAsync();

                var httpContext = httpContextAccessor.HttpContext;
                httpContext.Session.SetString("CustomerEmail", response.Customer.Email);
                httpContext.Session.SetString("CustomerId", response.Customer.Id);
                httpContext.Session.SetString("CustomerKey", response.Customer.Key);
                return response;
            }
            catch (BadRequestException ex)
            {

                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}