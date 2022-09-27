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
    public class StockController : Controller
    {
        private readonly IHTTPRepository<Stock> _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<StockController> _logger;

        public StockController(IHTTPRepository<Stock> repository, IStockAPIBaseAddress baseAddress, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<StockController> logger)
        {
            _repository = repository;
            _repository.SetBaseAddress(baseAddress, "api/Stock");

            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<StockModel[]>> Get()
        {
            try
            {
                var results = await _repository.GetAllAsync();

                return _mapper.Map<StockModel[]>(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<StockModel>> Get(int Id)
        {
            try
            {
                var result = await _repository.GetByIdAsync(Id);

                if (result == null) return NotFound();

                return _mapper.Map<StockModel>(result);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPost]
        public async Task<ActionResult<StockModel>> Post(StockModel model)
        {
            try
            {
                //Make sure StockId is not already taken
                var existing = await _repository.GetByIdAsync(model.Id);
                if (existing != null)
                {
                    return BadRequest("Stock Id in Use");
                }

                //map
                var Stock = _mapper.Map<Stock>(model);

                //save and return
                if (!await _repository.AddAsync(Stock))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "Stock",
                            new { Id = Stock.Id });

                    return Created(location, _mapper.Map<StockModel>(Stock));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<StockModel>> Put(int Id, StockModel updatedModel)
        {
            try
            {
                var currentStock = await _repository.GetByIdAsync(Id);
                if (currentStock == null) return NotFound($"Could not find Stock with Id of {Id}");

                _mapper.Map(updatedModel, currentStock);

                if (await _repository.UpdateAsync(currentStock))
                {
                    return _mapper.Map<StockModel>(currentStock);
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
                var Stock = await _repository.GetByIdAsync(Id);
                if (Stock == null) return NotFound();

                if (await _repository.DeleteAsync(Stock))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }

            return BadRequest("Failed to delete the Stock");
        }

    }
}
