using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ShopDomain.DataAccess;
using ShopDomain.Model;
using ShopWebAPI.Model;

namespace ShopWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class BillingAddressController : Controller
    {
        private IRepository<BillingAddress> _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;


        public BillingAddressController(IRepository<BillingAddress> repository, IMapper mapper, LinkGenerator linkGenerator)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }

        [HttpGet]
        public async Task<ActionResult<BillingAddressModel[]>> Get()
        {
            try
            {
                var results = await _repository.GetAllAsync();

                return _mapper.Map<BillingAddressModel[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
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
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
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
                    return BadRequest("Id in Use");
                }

                //save and return
                var newBillingAddress = _mapper.Map<BillingAddress>(model);

                if (!await _repository.AddAsync(newBillingAddress))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "BillingAddress",
                            new { newBillingAddress.Id });

                    return Created(location, _mapper.Map<BillingAddressModel>(newBillingAddress));
                }

            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<BillingAddressModel>> Put(BillingAddressModel updatedModel)
        {
            try
            {
                var currentBillingAddress = await _repository.GetByIdAsync(updatedModel.Id);
                if (currentBillingAddress == null) return NotFound($"Could not find BillingAddress with Id of {updatedModel.Id}");

                _mapper.Map(updatedModel, currentBillingAddress);

                if (await _repository.SaveAsync())
                {
                    return _mapper.Map<BillingAddressModel>(currentBillingAddress);
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
                var BillingAddress = await _repository.GetByIdAsync(Id);
                if (BillingAddress == null) return NotFound();

                if (await _repository.DeleteAsync(BillingAddress))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest("Failed to delete the BillingAddress");
        }

    }
}
