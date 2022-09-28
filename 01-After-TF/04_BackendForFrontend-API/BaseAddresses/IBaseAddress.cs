using System;
using System.Collections.Generic;
using System.Text;

namespace Shop_BackendForFrontend_API.BaseAddresses
{
    public interface IBaseAddress
    {
        Uri BaseAddress { get; set; }
    }
}
