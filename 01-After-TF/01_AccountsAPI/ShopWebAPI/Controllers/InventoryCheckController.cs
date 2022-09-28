using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ShopDomain.DataAccess;
using ShopDomain.Model;
using ShopWebAPI.Model;

namespace ShopWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class InventoryCheckController : Controller
    {
        private IRepository<InventoryCheck> _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;


        public InventoryCheckController(IRepository<InventoryCheck> repository, IMapper mapper, LinkGenerator linkGenerator)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }

        [HttpGet]
        public async Task<ActionResult<InventoryCheckModel[]>> Get()
        {
            try
            {
                var results = await _repository.GetAllAsync();

                return _mapper.Map<InventoryCheckModel[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
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
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
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
                    return BadRequest("Id in Use");
                }

                //save and return
                var newInventoryCheck = _mapper.Map<InventoryCheck>(model);

                if (!await _repository.AddAsync(newInventoryCheck))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "InventoryCheck",
                            new { newInventoryCheck.Id });

                    return Created(location, _mapper.Map<InventoryCheckModel>(newInventoryCheck));
                }

            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<InventoryCheckModel>> Put(InventoryCheckModel updatedModel)
        {
            try
            {
                var currentInventoryCheck = await _repository.GetByIdAsync(updatedModel.Id);
                if (currentInventoryCheck == null) return NotFound($"Could not find InventoryCheck with Id of {updatedModel.Id}");

                _mapper.Map(updatedModel, currentInventoryCheck);

                if (await _repository.SaveAsync())
                {
                    return _mapper.Map<InventoryCheckModel>(currentInventoryCheck);
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
                var InventoryCheck = await _repository.GetByIdAsync(Id);
                if (InventoryCheck == null) return NotFound();

                if (await _repository.DeleteAsync(InventoryCheck))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest("Failed to delete the InventoryCheck");
        }

    }
}
