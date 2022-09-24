using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ShopDomain.DataAccess;
using ShopDomain.Model;
using ShopWebAPI.Model;

namespace ShopWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private IRepository<Order> _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;


        public OrderController(IRepository<Order> repository, IMapper mapper, LinkGenerator linkGenerator)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }

        [HttpGet]
        public async Task<ActionResult<OrderModel[]>> Get()
        {
            try
            {
                var results = await _repository.GetAllAsync();

                return _mapper.Map<OrderModel[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<OrderModel>> Get(int Id)
        {
            try
            {
                var result = await _repository.GetByIdAsync(Id);

                if (result == null) return NotFound();
                return _mapper.Map<OrderModel>(result);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<OrderModel>> Post(OrderModel model)
        {
            try
            {
                //Make sure OrderId is not already taken
                var existing = await _repository.GetByIdAsync(model.Id);
                if (existing != null)
                {
                    return BadRequest("Id in Use");
                }

                //save and return
                var newOrder = _mapper.Map<Order>(model);

                if (!await _repository.AddAsync(newOrder))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "Order",
                            new { newOrder.Id });

                    return Created(location, _mapper.Map<OrderModel>(newOrder));
                }

            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<OrderModel>> Put(OrderModel updatedModel)
        {
            try
            {
                var currentOrder = await _repository.GetByIdAsync(updatedModel.Id);
                if (currentOrder == null) return NotFound($"Could not find Order with Id of {updatedModel.Id}");

                _mapper.Map(updatedModel, currentOrder);

                if (await _repository.SaveAsync())
                {
                    return _mapper.Map<OrderModel>(currentOrder);
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
                var Order = await _repository.GetByIdAsync(Id);
                if (Order == null) return NotFound();

                if (await _repository.DeleteAsync(Order))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest("Failed to delete the Order");
        }

    }
}
