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
    public class InventoryCheckController : Controller
    {
        private readonly IHTTPRepository<InventoryCheck> _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<InventoryCheckController> _logger;

        public InventoryCheckController(IHTTPRepository<InventoryCheck> repository, IAccountsAPIBaseAddress baseAddress, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<InventoryCheckController> logger)
        {
            _repository = repository;
            _repository.SetBaseAddress(baseAddress, "api/InventoryCheck");

            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<InventoryCheckModel[]>> Get()
        {
            try
            {
                var results = await _repository.GetAllAsync();

                return _mapper.Map<InventoryCheckModel[]>(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<InventoryCheckModel>> Get(int Id)
        {
            try
            {
                var result = await _repository.GetByIdAsync(Id);

                if (result == null) return NotFound();

                return _mapper.Map<InventoryCheckModel>(result);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPost]
        public async Task<ActionResult<InventoryCheckModel>> Post(InventoryCheckModel model)
        {
            try
            {
                //Make sure InventoryCheckId is not already taken
                var existing = await _repository.GetByIdAsync(model.Id);
                if (existing != null)
                {
                    return BadRequest("InventoryCheck Id in Use");
                }

                //map
                var InventoryCheck = _mapper.Map<InventoryCheck>(model);

                //save and return
                if (!await _repository.AddAsync(InventoryCheck))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "InventoryCheck",
                            new { Id = InventoryCheck.Id });

                    return Created(location, _mapper.Map<InventoryCheckModel>(InventoryCheck));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<InventoryCheckModel>> Put(int Id, InventoryCheckModel updatedModel)
        {
            try
            {
                var currentInventoryCheck = await _repository.GetByIdAsync(Id);
                if (currentInventoryCheck == null) return NotFound($"Could not find InventoryCheck with Id of {Id}");

                _mapper.Map(updatedModel, currentInventoryCheck);

                if (await _repository.UpdateAsync(currentInventoryCheck))
                {
                    return _mapper.Map<InventoryCheckModel>(currentInventoryCheck);
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
                var InventoryCheck = await _repository.GetByIdAsync(Id);
                if (InventoryCheck == null) return NotFound();

                if (await _repository.DeleteAsync(InventoryCheck))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }

            return BadRequest("Failed to delete the InventoryCheck");
        }

    }
}
