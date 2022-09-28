using System;
using System.Collections.Generic;
using System.Text;

namespace Shop_BackendForFrontend_API.BaseAddresses
{
    public class BaseAddressConfig : IBaseAddress
    {
        public Uri BaseAddress { get; set; }
    }
}
