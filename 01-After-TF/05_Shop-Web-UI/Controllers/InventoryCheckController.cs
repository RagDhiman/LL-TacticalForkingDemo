using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shop_Web_UI.DTOs;
using Shop_Web_UI.HTTPRepository;
using Shop_Web_UI.Models;

namespace Shop_Web_UI.Controllers
{
    public class InventoryCheckController : Controller
    {
        private readonly IHTTPRepository<InventoryCheck> _InventoryCheckRepository;
        private readonly IMapper _mapper;

        public InventoryCheckController(IHTTPRepository<InventoryCheck> InventoryCheckRepository,
            IMapper mapper)
        {
            _InventoryCheckRepository = InventoryCheckRepository;
            _InventoryCheckRepository.APIPath = "api/InventoryCheck";
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ViewResult> Index()
        {
            var InventoryChecks = await _InventoryCheckRepository.GetAllAsync();

            var InventoryCheckModels = _mapper.Map<IEnumerable<InventoryCheckModel>>(InventoryChecks);

            return View(InventoryCheckModels);
        }

        [HttpGet("[controller]/[action]/{Id}")]
        public async Task<IActionResult> Details(int Id)
        {
            var InventoryCheck = await _InventoryCheckRepository.GetByIdAsync(Id);
            var model = _mapper.Map<InventoryCheckModel>(InventoryCheck);

            return View(model);
        }
    }
}
