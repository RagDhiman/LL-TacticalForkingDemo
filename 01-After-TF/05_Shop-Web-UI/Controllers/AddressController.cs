using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shop_Web_UI.DTOs;
using Shop_Web_UI.HTTPRepository;
using Shop_Web_UI.Models;

namespace Shop_Web_UI.Controllers
{
    public class AddressController : Controller
    {
        private readonly IHTTPRepository<Address> _AddressRepository;
        private readonly IMapper _mapper;

        public AddressController(IHTTPRepository<Address> AddressRepository,
            IMapper mapper)
        {
            _AddressRepository = AddressRepository;
            _AddressRepository.APIPath = "api/Address";
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ViewResult> Index()
        {
            var Addresss = await _AddressRepository.GetAllAsync();

            var AddressModels = _mapper.Map<IEnumerable<AddressModel>>(Addresss);

            return View(AddressModels);
        }

        [HttpGet("[controller]/[action]/{Id}")]
        public async Task<IActionResult> Details(int Id)
        {
            var Address = await _AddressRepository.GetByIdAsync(Id);
            var model = _mapper.Map<AddressModel>(Address);

            return View(model);
        }
    }
}
