using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shop_Web_UI.DTOs;
using Shop_Web_UI.HTTPRepository;
using Shop_Web_UI.Models;

namespace Shop_Web_UI.Controllers
{
    public class StockController : Controller
    {
        private readonly IHTTPRepository<Stock> _StockRepository;
        private readonly IMapper _mapper;

        public StockController(IHTTPRepository<Stock> StockRepository,
            IMapper mapper)
        {
            _StockRepository = StockRepository;
            _StockRepository.APIPath = "api/Stock";
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ViewResult> Index()
        {
            var Stocks = await _StockRepository.GetAllAsync();

            var StockModels = _mapper.Map<IEnumerable<StockModel>>(Stocks);

            return View(StockModels);
        }

        [HttpGet("[controller]/[action]/{Id}")]
        public async Task<IActionResult> Details(int Id)
        {
            var Stock = await _StockRepository.GetByIdAsync(Id);
            var model = _mapper.Map<StockModel>(Stock);

            return View(model);
        }
    }
}
