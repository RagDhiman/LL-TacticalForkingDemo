using Shop_BackendForFrontend_API.BaseAddresses;

namespace Shop_BackendForFrontend_API.BaseAddresses
{
    public interface IAccountsAPIBaseAddress : IBaseAddress
    {
    }

    public class AccountsAPIBaseAddress: IAccountsAPIBaseAddress
    {
        public Uri BaseAddress { get; set; }
        public AccountsAPIBaseAddress(string baseAddress)
        {
            this.BaseAddress = new Uri(baseAddress);
        }
    }
}
