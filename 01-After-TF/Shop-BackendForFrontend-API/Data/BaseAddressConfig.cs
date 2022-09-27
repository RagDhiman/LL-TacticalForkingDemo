using System;
using System.Collections.Generic;
using System.Text;

namespace Shop_BackendForFrontend_API.Data
{
    public class BaseAddressConfig : IBaseAddress
    {
        public Uri BaseAddress { get; set;  }
    }
}
