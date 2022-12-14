using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ShopDomain.DataAccess;
using ShopDomain.Model;
using ShopWebAPI.Model;

namespace ShopWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class DeliveryController : Controller
    {
        private IRepository<Delivery> _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;


        public DeliveryController(IRepository<Delivery> repository, IMapper mapper, LinkGenerator linkGenerator)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }

        [HttpGet]
        public async Task<ActionResult<DeliveryModel[]>> Get()
        {
            try
            {
                var results = await _repository.GetAllAsync();

                return _mapper.Map<DeliveryModel[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
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
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
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
                    return BadRequest("Id in Use");
                }

                //save and return
                var newDelivery = _mapper.Map<Delivery>(model);

                if (!await _repository.AddAsync(newDelivery))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "Delivery",
                            new { newDelivery.Id });

                    return Created(location, _mapper.Map<DeliveryModel>(newDelivery));
                }

            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<DeliveryModel>> Put(DeliveryModel updatedModel)
        {
            try
            {
                var currentDelivery = await _repository.GetByIdAsync(updatedModel.Id);
                if (currentDelivery == null) return NotFound($"Could not find Delivery with Id of {updatedModel.Id}");

                _mapper.Map(updatedModel, currentDelivery);

                if (await _repository.SaveAsync())
                {
                    return _mapper.Map<DeliveryModel>(currentDelivery);
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
                var Delivery = await _repository.GetByIdAsync(Id);
                if (Delivery == null) return NotFound();

                if (await _repository.DeleteAsync(Delivery))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest("Failed to delete the Delivery");
        }

    }
}
