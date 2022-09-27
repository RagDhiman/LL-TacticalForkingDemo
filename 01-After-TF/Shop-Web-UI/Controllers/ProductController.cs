using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shop_Web_UI.DTOs;
using Shop_Web_UI.HTTPRepository;
using Shop_Web_UI.Models;

namespace Shop_Web_UI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IHTTPRepository<Product> _ProductRepository;
        private readonly IMapper _mapper;

        public ProductController(IHTTPRepository<Product> ProductRepository,
            IMapper mapper)
        {
            _ProductRepository = ProductRepository;
            _ProductRepository.APIPath = "api/Product";
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ViewResult> Index()
        {
            var Products = await _ProductRepository.GetAllAsync();

            var ProductModels = _mapper.Map<IEnumerable<ProductModel>>(Products);

            return View(ProductModels);
        }

        [HttpGet("[controller]/[action]/{Id}")]
        public async Task<IActionResult> Details(int Id)
        {
            var Product = await _ProductRepository.GetByIdAsync(Id);
            var model = _mapper.Map<ProductModel>(Product);

            return View(model);
        }
    }
}
