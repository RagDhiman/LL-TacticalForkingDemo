using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shop_BackendForFrontend_API.BaseAddresses;
using Shop_BackendForFrontend_API.Data;
using Shop_BackendForFrontend_API.Domain;
using Shop_BackendForFrontend_API.Model;

namespace Shop_BackendForFrontend_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IHTTPRepository<Product> _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IHTTPRepository<Product> repository, IStockAPIBaseAddress baseAddress, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<ProductController> logger)
        {
            _repository = repository;
            _repository.SetBaseAddress(baseAddress, "api/Product");

            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<ProductModel[]>> Get()
        {
            try
            {
                var results = await _repository.GetAllAsync();

                return _mapper.Map<ProductModel[]>(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<ProductModel>> Get(int Id)
        {
            try
            {
                var result = await _repository.GetByIdAsync(Id);

                if (result == null) return NotFound();

                return _mapper.Map<ProductModel>(result);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ProductModel>> Post(ProductModel model)
        {
            try
            {
                //Make sure ProductId is not already taken
                var existing = await _repository.GetByIdAsync(model.Id);
                if (existing != null)
                {
                    return BadRequest("Product Id in Use");
                }

                //map
                var Product = _mapper.Map<Product>(model);

                //save and return
                if (!await _repository.AddAsync(Product))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "Product",
                            new { Id = Product.Id });

                    return Created(location, _mapper.Map<ProductModel>(Product));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<ProductModel>> Put(int Id, ProductModel updatedModel)
        {
            try
            {
                var currentProduct = await _repository.GetByIdAsync(Id);
                if (currentProduct == null) return NotFound($"Could not find Product with Id of {Id}");

                _mapper.Map(updatedModel, currentProduct);

                if (await _repository.UpdateAsync(currentProduct))
                {
                    return _mapper.Map<ProductModel>(currentProduct);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }

            return BadRequest();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                var Product = await _repository.GetByIdAsync(Id);
                if (Product == null) return NotFound();

                if (await _repository.DeleteAsync(Product))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }

            return BadRequest("Failed to delete the Product");
        }

    }
}
