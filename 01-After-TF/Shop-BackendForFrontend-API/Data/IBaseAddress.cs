using System;
using System.Collections.Generic;
using System.Text;

namespace Shop_BackendForFrontend_API.Data
{
    public interface IBaseAddress
    {
        Uri BaseAddress { get; set; }
    }
}
