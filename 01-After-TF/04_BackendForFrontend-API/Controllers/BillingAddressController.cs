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
    public class BillingAddressController : Controller
    {
        private readonly IHTTPRepository<BillingAddress> _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<BillingAddressController> _logger;

        public BillingAddressController(IHTTPRepository<BillingAddress> repository, IAccountsAPIBaseAddress baseAddress, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<BillingAddressController> logger)
        {
            _repository = repository;
            _repository.SetBaseAddress(baseAddress, "api/BillingAddress");

            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<BillingAddressModel[]>> Get()
        {
            try
            {
                var results = await _repository.GetAllAsync();

                return _mapper.Map<BillingAddressModel[]>(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<BillingAddressModel>> Get(int Id)
        {
            try
            {
                var result = await _repository.GetByIdAsync(Id);

                if (result == null) return NotFound();

                return _mapper.Map<BillingAddressModel>(result);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPost]
        public async Task<ActionResult<BillingAddressModel>> Post(BillingAddressModel model)
        {
            try
            {
                //Make sure BillingAddressId is not already taken
                var existing = await _repository.GetByIdAsync(model.Id);
                if (existing != null)
                {
                    return BadRequest("BillingAddress Id in Use");
                }

                //map
                var BillingAddress = _mapper.Map<BillingAddress>(model);

                //save and return
                if (!await _repository.AddAsync(BillingAddress))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "BillingAddress",
                            new { Id = BillingAddress.Id });

                    return Created(location, _mapper.Map<BillingAddressModel>(BillingAddress));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<BillingAddressModel>> Put(int Id, BillingAddressModel updatedModel)
        {
            try
            {
                var currentBillingAddress = await _repository.GetByIdAsync(Id);
                if (currentBillingAddress == null) return NotFound($"Could not find BillingAddress with Id of {Id}");

                _mapper.Map(updatedModel, currentBillingAddress);

                if (await _repository.UpdateAsync(currentBillingAddress))
                {
                    return _mapper.Map<BillingAddressModel>(currentBillingAddress);
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
                var BillingAddress = await _repository.GetByIdAsync(Id);
                if (BillingAddress == null) return NotFound();

                if (await _repository.DeleteAsync(BillingAddress))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }

            return BadRequest("Failed to delete the BillingAddress");
        }

    }
}
