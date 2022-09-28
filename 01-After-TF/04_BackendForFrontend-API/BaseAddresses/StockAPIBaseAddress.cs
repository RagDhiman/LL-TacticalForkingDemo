using Shop_BackendForFrontend_API.BaseAddresses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop_BackendForFrontend_API.BaseAddresses
{
    public interface IStockAPIBaseAddress : IBaseAddress
    {
    }
    public class StockAPIBaseAddress : IStockAPIBaseAddress
    {
        public Uri BaseAddress { get; set; }
        public StockAPIBaseAddress(string baseAddress)
        {
            this.BaseAddress = new Uri(baseAddress);
        }
    }
}
