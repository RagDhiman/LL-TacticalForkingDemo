using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shop_Web_UI.DTOs;
using Shop_Web_UI.HTTPRepository;
using Shop_Web_UI.Models;

namespace Shop_Web_UI.Controllers
{
    public class OrderHistoryController : Controller
    {
        private readonly IHTTPRepository<OrderHistory> _OrderHistoryRepository;
        private readonly IMapper _mapper;

        public OrderHistoryController(IHTTPRepository<OrderHistory> OrderHistoryRepository,
            IMapper mapper)
        {
            _OrderHistoryRepository = OrderHistoryRepository;
            _OrderHistoryRepository.APIPath = "api/OrderHistory";
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ViewResult> Index()
        {
            var OrderHistorys = await _OrderHistoryRepository.GetAllAsync();

            var OrderHistoryModels = _mapper.Map<IEnumerable<OrderHistoryModel>>(OrderHistorys);

            return View(OrderHistoryModels);
        }

        [HttpGet("[controller]/[action]/{Id}")]
        public async Task<IActionResult> Details(int Id)
        {
            var OrderHistory = await _OrderHistoryRepository.GetByIdAsync(Id);
            var model = _mapper.Map<OrderHistoryModel>(OrderHistory);

            return View(model);
        }
    }
}
