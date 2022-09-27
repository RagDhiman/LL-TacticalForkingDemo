using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shop_Web_UI.DTOs;
using Shop_Web_UI.HTTPRepository;
using Shop_Web_UI.Models;

namespace Shop_Web_UI.Controllers
{
    public class SupplierController : Controller
    {
        private readonly IHTTPRepository<Supplier> _SupplierRepository;
        private readonly IMapper _mapper;

        public SupplierController(IHTTPRepository<Supplier> SupplierRepository,
            IMapper mapper)
        {
            _SupplierRepository = SupplierRepository;
            _SupplierRepository.APIPath = "api/Supplier";
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ViewResult> Index()
        {
            var Suppliers = await _SupplierRepository.GetAllAsync();

            var SupplierModels = _mapper.Map<IEnumerable<SupplierModel>>(Suppliers);

            return View(SupplierModels);
        }

        [HttpGet("[controller]/[action]/{Id}")]
        public async Task<IActionResult> Details(int Id)
        {
            var Supplier = await _SupplierRepository.GetByIdAsync(Id);
            var model = _mapper.Map<SupplierModel>(Supplier);

            return View(model);
        }
    }
}
