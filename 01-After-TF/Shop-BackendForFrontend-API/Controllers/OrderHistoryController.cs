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
    public class OrderHistoryController : Controller
    {
        private readonly IHTTPRepository<OrderHistory> _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<OrderHistoryController> _logger;

        public OrderHistoryController(IHTTPRepository<OrderHistory> repository, IAccountsAPIBaseAddress baseAddress, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<OrderHistoryController> logger)
        {
            _repository = repository;
            _repository.SetBaseAddress(baseAddress, "api/OrderHistory");

            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<OrderHistoryModel[]>> Get()
        {
            try
            {
                var results = await _repository.GetAllAsync();

                return _mapper.Map<OrderHistoryModel[]>(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<OrderHistoryModel>> Get(int Id)
        {
            try
            {
                var result = await _repository.GetByIdAsync(Id);

                if (result == null) return NotFound();

                return _mapper.Map<OrderHistoryModel>(result);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPost]
        public async Task<ActionResult<OrderHistoryModel>> Post(OrderHistoryModel model)
        {
            try
            {
                //Make sure OrderHistoryId is not already taken
                var existing = await _repository.GetByIdAsync(model.Id);
                if (existing != null)
                {
                    return BadRequest("OrderHistory Id in Use");
                }

                //map
                var OrderHistory = _mapper.Map<OrderHistory>(model);

                //save and return
                if (!await _repository.AddAsync(OrderHistory))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "OrderHistory",
                            new { Id = OrderHistory.Id });

                    return Created(location, _mapper.Map<OrderHistoryModel>(OrderHistory));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<OrderHistoryModel>> Put(int Id, OrderHistoryModel updatedModel)
        {
            try
            {
                var currentOrderHistory = await _repository.GetByIdAsync(Id);
                if (currentOrderHistory == null) return NotFound($"Could not find OrderHistory with Id of {Id}");

                _mapper.Map(updatedModel, currentOrderHistory);

                if (await _repository.UpdateAsync(currentOrderHistory))
                {
                    return _mapper.Map<OrderHistoryModel>(currentOrderHistory);
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
                var OrderHistory = await _repository.GetByIdAsync(Id);
                if (OrderHistory == null) return NotFound();

                if (await _repository.DeleteAsync(OrderHistory))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }

            return BadRequest("Failed to delete the OrderHistory");
        }

    }
}
