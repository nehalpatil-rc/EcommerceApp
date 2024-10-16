using commercetools.Sdk.Api.Models.Customers;

namespace EcommerceApp.Services.Interface
{
    public interface ICustomerService
    {
        Task<ICustomerPagedQueryResponse> GetAllCustomer();
        Task<ICustomerSignInResult> AddCustomer(CustomerDraft customerDraft);
        Task<ICustomerPagedQueryResponse> IsCustomerExists(CustomerDraft customerDraft);
        Task<ICustomer> GetCustomerBykey(string key);
        Task<ICustomerSignInResult> Signin(CustomerSignin customerSignin);
    }
}