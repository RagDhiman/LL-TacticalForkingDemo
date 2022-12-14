using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ShopDomain.DataAccess;
using ShopDomain.Model;
using ShopWebAPI.Model;

namespace ShopWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private IRepository<Product> _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;


        public ProductController(IRepository<Product> repository, IMapper mapper, LinkGenerator linkGenerator)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }

        [HttpGet]
        public async Task<ActionResult<ProductModel[]>> Get()
        {
            try
            {
                var results = await _repository.GetAllAsync();

                return _mapper.Map<ProductModel[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
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
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
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
                    return BadRequest("Id in Use");
                }

                //save and return
                var newProduct = _mapper.Map<Product>(model);

                if (!await _repository.AddAsync(newProduct))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "Product",
                            new { newProduct.Id });

                    return Created(location, _mapper.Map<ProductModel>(newProduct));
                }

            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<ProductModel>> Put(ProductModel updatedModel)
        {
            try
            {
                var currentProduct = await _repository.GetByIdAsync(updatedModel.Id);
                if (currentProduct == null) return NotFound($"Could not find Product with Id of {updatedModel.Id}");

                _mapper.Map(updatedModel, currentProduct);

                if (await _repository.SaveAsync())
                {
                    return _mapper.Map<ProductModel>(currentProduct);
                }
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
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
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest("Failed to delete the Product");
        }

    }
}
