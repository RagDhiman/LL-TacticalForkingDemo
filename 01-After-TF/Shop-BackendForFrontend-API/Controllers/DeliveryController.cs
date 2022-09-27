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
    public class DeliveryController : Controller
    {
        private readonly IHTTPRepository<Delivery> _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<DeliveryController> _logger;

        public DeliveryController(IHTTPRepository<Delivery> repository, IAccountsAPIBaseAddress baseAddress, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<DeliveryController> logger)
        {
            _repository = repository;
            _repository.SetBaseAddress(baseAddress, "api/Delivery");

            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<DeliveryModel[]>> Get()
        {
            try
            {
                var results = await _repository.GetAllAsync();

                return _mapper.Map<DeliveryModel[]>(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<DeliveryModel>> Get(int Id)
        {
            try
            {
                var result = await _repository.GetByIdAsync(Id);

                if (result == null) return NotFound();

                return _mapper.Map<DeliveryModel>(result);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPost]
        public async Task<ActionResult<DeliveryModel>> Post(DeliveryModel model)
        {
            try
            {
                //Make sure DeliveryId is not already taken
                var existing = await _repository.GetByIdAsync(model.Id);
                if (existing != null)
                {
                    return BadRequest("Delivery Id in Use");
                }

                //map
                var Delivery = _mapper.Map<Delivery>(model);

                //save and return
                if (!await _repository.AddAsync(Delivery))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "Delivery",
                            new { Id = Delivery.Id });

                    return Created(location, _mapper.Map<DeliveryModel>(Delivery));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<DeliveryModel>> Put(int Id, DeliveryModel updatedModel)
        {
            try
            {
                var currentDelivery = await _repository.GetByIdAsync(Id);
                if (currentDelivery == null) return NotFound($"Could not find Delivery with Id of {Id}");

                _mapper.Map(updatedModel, currentDelivery);

                if (await _repository.UpdateAsync(currentDelivery))
                {
                    return _mapper.Map<DeliveryModel>(currentDelivery);
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
                var Delivery = await _repository.GetByIdAsync(Id);
                if (Delivery == null) return NotFound();

                if (await _repository.DeleteAsync(Delivery))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }

            return BadRequest("Failed to delete the Delivery");
        }

    }
}
