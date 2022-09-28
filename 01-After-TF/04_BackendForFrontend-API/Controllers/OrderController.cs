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
    public class OrderController : Controller
    {
        private readonly IHTTPRepository<Order> _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IHTTPRepository<Order> repository, IOrdersAPIBaseAddress baseAddress, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<OrderController> logger)
        {
            _repository = repository;
            _repository.SetBaseAddress(baseAddress, "api/Order");

            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<OrderModel[]>> Get()
        {
            try
            {
                var results = await _repository.GetAllAsync();

                return _mapper.Map<OrderModel[]>(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<OrderModel>> Get(int Id)
        {
            try
            {
                var result = await _repository.GetByIdAsync(Id);

                if (result == null) return NotFound();

                return _mapper.Map<OrderModel>(result);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPost]
        public async Task<ActionResult<OrderModel>> Post(OrderModel model)
        {
            try
            {
                //Make sure OrderId is not already taken
                var existing = await _repository.GetByIdAsync(model.Id);
                if (existing != null)
                {
                    return BadRequest("Order Id in Use");
                }

                //map
                var Order = _mapper.Map<Order>(model);

                //save and return
                if (!await _repository.AddAsync(Order))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "Order",
                            new { Id = Order.Id });

                    return Created(location, _mapper.Map<OrderModel>(Order));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<OrderModel>> Put(int Id, OrderModel updatedModel)
        {
            try
            {
                var currentOrder = await _repository.GetByIdAsync(Id);
                if (currentOrder == null) return NotFound($"Could not find Order with Id of {Id}");

                _mapper.Map(updatedModel, currentOrder);

                if (await _repository.UpdateAsync(currentOrder))
                {
                    return _mapper.Map<OrderModel>(currentOrder);
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
                var Order = await _repository.GetByIdAsync(Id);
                if (Order == null) return NotFound();

                if (await _repository.DeleteAsync(Order))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }

            return BadRequest("Failed to delete the Order");
        }

    }
}
