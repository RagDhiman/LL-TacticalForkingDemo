using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shop_Web_UI.DTOs;
using Shop_Web_UI.HTTPRepository;
using Shop_Web_UI.Models;

namespace Shop_Web_UI.Controllers
{
    public class CreditCardController : Controller
    {
        private readonly IHTTPRepository<CreditCard> _CreditCardRepository;
        private readonly IMapper _mapper;

        public CreditCardController(IHTTPRepository<CreditCard> CreditCardRepository,
            IMapper mapper)
        {
            _CreditCardRepository = CreditCardRepository;
            _CreditCardRepository.APIPath = "api/CreditCard";
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ViewResult> Index()
        {
            var CreditCards = await _CreditCardRepository.GetAllAsync();

            var CreditCardModels = _mapper.Map<IEnumerable<CreditCardModel>>(CreditCards);

            return View(CreditCardModels);
        }

        [HttpGet("[controller]/[action]/{Id}")]
        public async Task<IActionResult> Details(int Id)
        {
            var CreditCard = await _CreditCardRepository.GetByIdAsync(Id);
            var model = _mapper.Map<CreditCardModel>(CreditCard);

            return View(model);
        }
    }
}
