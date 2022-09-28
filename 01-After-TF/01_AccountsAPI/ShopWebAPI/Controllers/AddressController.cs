using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ShopDomain.DataAccess;
using ShopDomain.Model;
using ShopWebAPI.Model;

namespace ShopWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class AddressController : Controller
    {
        private IRepository<Address> _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;


        public AddressController(IRepository<Address> repository, IMapper mapper, LinkGenerator linkGenerator)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }

        [HttpGet]
        public async Task<ActionResult<AddressModel[]>> Get()
        {
            try
            {
                var results = await _repository.GetAllAsync();

                return _mapper.Map<AddressModel[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
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
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
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
                    return BadRequest("Id in Use");
                }

                //save and return
                var newAddress = _mapper.Map<Address>(model);

                if (!await _repository.AddAsync(newAddress))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "Address",
                            new { newAddress.Id });

                    return Created(location, _mapper.Map<AddressModel>(newAddress));
                }

            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<AddressModel>> Put(AddressModel updatedModel)
        {
            try
            {
                var currentAddress = await _repository.GetByIdAsync(updatedModel.Id);
                if (currentAddress == null) return NotFound($"Could not find Address with Id of {updatedModel.Id}");

                _mapper.Map(updatedModel, currentAddress);

                if (await _repository.SaveAsync())
                {
                    return _mapper.Map<AddressModel>(currentAddress);
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
                var Address = await _repository.GetByIdAsync(Id);
                if (Address == null) return NotFound();

                if (await _repository.DeleteAsync(Address))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest("Failed to delete the Address");
        }

    }
}
