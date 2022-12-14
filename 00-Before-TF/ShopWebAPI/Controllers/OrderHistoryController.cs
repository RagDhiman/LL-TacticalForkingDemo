using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ShopDomain.DataAccess;
using ShopDomain.Model;
using ShopWebAPI.Model;

namespace ShopWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class OrderHistoryController : Controller
    {
        private IRepository<OrderHistory> _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;


        public OrderHistoryController(IRepository<OrderHistory> repository, IMapper mapper, LinkGenerator linkGenerator)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }

        [HttpGet]
        public async Task<ActionResult<OrderHistoryModel[]>> Get()
        {
            try
            {
                var results = await _repository.GetAllAsync();

                return _mapper.Map<OrderHistoryModel[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<OrderHistoryModel>> Get(int Id)
        {
            try
            {
                var result = await _repository.GetByIdAsync(Id);

                if (result == null) return NotFound();
                return _mapper.Map<OrderHistoryModel>(result);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<OrderHistoryModel>> Post(OrderHistoryModel model)
        {
            try
            {
                //Make sure OrderHistoryId is not already taken
                var existing = await _repository.GetByIdAsync(model.Id);
                if (existing != null)
                {
                    return BadRequest("Id in Use");
                }

                //save and return
                var newOrderHistory = _mapper.Map<OrderHistory>(model);

                if (!await _repository.AddAsync(newOrderHistory))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "OrderHistory",
                            new { newOrderHistory.Id });

                    return Created(location, _mapper.Map<OrderHistoryModel>(newOrderHistory));
                }

            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<OrderHistoryModel>> Put(OrderHistoryModel updatedModel)
        {
            try
            {
                var currentOrderHistory = await _repository.GetByIdAsync(updatedModel.Id);
                if (currentOrderHistory == null) return NotFound($"Could not find OrderHistory with Id of {updatedModel.Id}");

                _mapper.Map(updatedModel, currentOrderHistory);

                if (await _repository.SaveAsync())
                {
                    return _mapper.Map<OrderHistoryModel>(currentOrderHistory);
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
                var OrderHistory = await _repository.GetByIdAsync(Id);
                if (OrderHistory == null) return NotFound();

                if (await _repository.DeleteAsync(OrderHistory))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest("Failed to delete the OrderHistory");
        }

    }
}
