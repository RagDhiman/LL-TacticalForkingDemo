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
    public class AddressController : Controller
    {
        private readonly IHTTPRepository<Address> _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<AddressController> _logger;

        public AddressController(IHTTPRepository<Address> repository, IAccountsAPIBaseAddress baseAddress, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<AddressController> logger)
        {
            _repository = repository;
            _repository.SetBaseAddress(baseAddress, "api/Address");

            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<AddressModel[]>> Get()
        {
            try
            {
                var results = await _repository.GetAllAsync();

                return _mapper.Map<AddressModel[]>(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<AddressModel>> Get(int Id)
        {
            try
            {
                var result = await _repository.GetByIdAsync(Id);

                if (result == null) return NotFound();

                return _mapper.Map<AddressModel>(result);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPost]
        public async Task<ActionResult<AddressModel>> Post(AddressModel model)
        {
            try
            {
                //Make sure AddressId is not already taken
                var existing = await _repository.GetByIdAsync(model.Id);
                if (existing != null)
                {
                    return BadRequest("Address Id in Use");
                }

                //map
                var Address = _mapper.Map<Address>(model);

                //save and return
                if (!await _repository.AddAsync(Address))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "Address",
                            new { Id = Address.Id });

                    return Created(location, _mapper.Map<AddressModel>(Address));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<AddressModel>> Put(int Id, AddressModel updatedModel)
        {
            try
            {
                var currentAddress = await _repository.GetByIdAsync(Id);
                if (currentAddress == null) return NotFound($"Could not find Address with Id of {Id}");

                _mapper.Map(updatedModel, currentAddress);

                if (await _repository.UpdateAsync(currentAddress))
                {
                    return _mapper.Map<AddressModel>(currentAddress);
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
                var Address = await _repository.GetByIdAsync(Id);
                if (Address == null) return NotFound();

                if (await _repository.DeleteAsync(Address))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }

            return BadRequest("Failed to delete the Address");
        }

    }
}
