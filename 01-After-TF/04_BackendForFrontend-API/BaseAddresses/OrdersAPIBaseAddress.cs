using Shop_BackendForFrontend_API.BaseAddresses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop_BackendForFrontend_API.BaseAddresses
{
    public interface IOrdersAPIBaseAddress : IBaseAddress
    {
    }

    public class OrdersAPIBaseAddress : IOrdersAPIBaseAddress
    {
        public Uri BaseAddress { get; set; }
        public OrdersAPIBaseAddress(string baseAddress)
        {
            this.BaseAddress = new Uri(baseAddress);
        }
    }
}
