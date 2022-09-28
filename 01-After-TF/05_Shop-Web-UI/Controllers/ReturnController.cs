using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shop_Web_UI.DTOs;
using Shop_Web_UI.HTTPRepository;
using Shop_Web_UI.Models;

namespace Shop_Web_UI.Controllers
{
    public class ReturnController : Controller
    {
        private readonly IHTTPRepository<Return> _ReturnRepository;
        private readonly IMapper _mapper;

        public ReturnController(IHTTPRepository<Return> ReturnRepository,
            IMapper mapper)
        {
            _ReturnRepository = ReturnRepository;
            _ReturnRepository.APIPath = "api/Return";
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ViewResult> Index()
        {
            var Returns = await _ReturnRepository.GetAllAsync();

            var ReturnModels = _mapper.Map<IEnumerable<ReturnModel>>(Returns);

            return View(ReturnModels);
        }

        [HttpGet("[controller]/[action]/{Id}")]
        public async Task<IActionResult> Details(int Id)
        {
            var Return = await _ReturnRepository.GetByIdAsync(Id);
            var model = _mapper.Map<ReturnModel>(Return);

            return View(model);
        }
    }
}
