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
    public class ReturnController : Controller
    {
        private readonly IHTTPRepository<Return> _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<ReturnController> _logger;

        public ReturnController(IHTTPRepository<Return> repository, IOrdersAPIBaseAddress baseAddress, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<ReturnController> logger)
        {
            _repository = repository;
            _repository.SetBaseAddress(baseAddress, "api/Return");

            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<ReturnModel[]>> Get()
        {
            try
            {
                var results = await _repository.GetAllAsync();

                return _mapper.Map<ReturnModel[]>(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<ReturnModel>> Get(int Id)
        {
            try
            {
                var result = await _repository.GetByIdAsync(Id);

                if (result == null) return NotFound();

                return _mapper.Map<ReturnModel>(result);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ReturnModel>> Post(ReturnModel model)
        {
            try
            {
                //Make sure ReturnId is not already taken
                var existing = await _repository.GetByIdAsync(model.Id);
                if (existing != null)
                {
                    return BadRequest("Return Id in Use");
                }

                //map
                var Return = _mapper.Map<Return>(model);

                //save and return
                if (!await _repository.AddAsync(Return))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "Return",
                            new { Id = Return.Id });

                    return Created(location, _mapper.Map<ReturnModel>(Return));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<ReturnModel>> Put(int Id, ReturnModel updatedModel)
        {
            try
            {
                var currentReturn = await _repository.GetByIdAsync(Id);
                if (currentReturn == null) return NotFound($"Could not find Return with Id of {Id}");

                _mapper.Map(updatedModel, currentReturn);

                if (await _repository.UpdateAsync(currentReturn))
                {
                    return _mapper.Map<ReturnModel>(currentReturn);
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
                var Return = await _repository.GetByIdAsync(Id);
                if (Return == null) return NotFound();

                if (await _repository.DeleteAsync(Return))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }

            return BadRequest("Failed to delete the Return");
        }

    }
}
