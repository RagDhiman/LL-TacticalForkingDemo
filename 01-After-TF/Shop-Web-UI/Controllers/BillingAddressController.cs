using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shop_Web_UI.DTOs;
using Shop_Web_UI.HTTPRepository;
using Shop_Web_UI.Models;

namespace Shop_Web_UI.Controllers
{
    public class BillingAddressController : Controller
    {
        private readonly IHTTPRepository<BillingAddress> _BillingAddressRepository;
        private readonly IMapper _mapper;

        public BillingAddressController(IHTTPRepository<BillingAddress> BillingAddressRepository,
            IMapper mapper)
        {
            _BillingAddressRepository = BillingAddressRepository;
            _BillingAddressRepository.APIPath = "api/BillingAddress";
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ViewResult> Index()
        {
            var BillingAddresss = await _BillingAddressRepository.GetAllAsync();

            var BillingAddressModels = _mapper.Map<IEnumerable<BillingAddressModel>>(BillingAddresss);

            return View(BillingAddressModels);
        }

        [HttpGet("[controller]/[action]/{Id}")]
        public async Task<IActionResult> Details(int Id)
        {
            var BillingAddress = await _BillingAddressRepository.GetByIdAsync(Id);
            var model = _mapper.Map<BillingAddressModel>(BillingAddress);

            return View(model);
        }
    }
}
