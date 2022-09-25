using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shop_Web_UI.DTOs;
using Shop_Web_UI.HTTPRepository;
using Shop_Web_UI.Models;

namespace Shop_Web_UI.Controllers
{
    public class DeliveryController : Controller
    {
        private readonly IHTTPRepository<Delivery> _DeliveryRepository;
        private readonly IMapper _mapper;

        public DeliveryController(IHTTPRepository<Delivery> DeliveryRepository,
            IMapper mapper)
        {
            _DeliveryRepository = DeliveryRepository;
            _DeliveryRepository.APIPath = "api/Delivery";
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ViewResult> Index()
        {
            var Deliverys = await _DeliveryRepository.GetAllAsync();

            var DeliveryModels = _mapper.Map<IEnumerable<DeliveryModel>>(Deliverys);

            return View(DeliveryModels);
        }

        [HttpGet("[controller]/[action]/{Id}")]
        public async Task<IActionResult> Details(int Id)
        {
            var Delivery = await _DeliveryRepository.GetByIdAsync(Id);
            var model = _mapper.Map<DeliveryModel>(Delivery);

            return View(model);
        }
    }
}
