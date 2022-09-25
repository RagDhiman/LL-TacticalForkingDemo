using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shop_Web_UI.DTOs;
using Shop_Web_UI.HTTPRepository;
using Shop_Web_UI.Models;

namespace Shop_Web_UI.Controllers
{
    public class OrderController : Controller
    {
        private readonly IHTTPRepository<Order> _OrderRepository;
        private readonly IMapper _mapper;

        public OrderController(IHTTPRepository<Order> OrderRepository,
            IMapper mapper)
        {
            _OrderRepository = OrderRepository;
            _OrderRepository.APIPath = "api/Order";
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ViewResult> Index()
        {
            var Orders = await _OrderRepository.GetAllAsync();

            var OrderModels = _mapper.Map<IEnumerable<OrderModel>>(Orders);

            return View(OrderModels);
        }

        [HttpGet("[controller]/[action]/{Id}")]
        public async Task<IActionResult> Details(int Id)
        {
            var Order = await _OrderRepository.GetByIdAsync(Id);
            var model = _mapper.Map<OrderModel>(Order);

            return View(model);
        }
    }
}
