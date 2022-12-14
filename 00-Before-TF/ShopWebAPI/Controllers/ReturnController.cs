using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ShopDomain.DataAccess;
using ShopDomain.Model;
using ShopWebAPI.Model;

namespace ShopWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class ReturnController : Controller
    {
        private IRepository<Return> _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;


        public ReturnController(IRepository<Return> repository, IMapper mapper, LinkGenerator linkGenerator)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }

        [HttpGet]
        public async Task<ActionResult<ReturnModel[]>> Get()
        {
            try
            {
                var results = await _repository.GetAllAsync();

                return _mapper.Map<ReturnModel[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
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
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
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
                    return BadRequest("Id in Use");
                }

                //save and return
                var newReturn = _mapper.Map<Return>(model);

                if (!await _repository.AddAsync(newReturn))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "Return",
                            new { newReturn.Id });

                    return Created(location, _mapper.Map<ReturnModel>(newReturn));
                }

            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<ReturnModel>> Put(ReturnModel updatedModel)
        {
            try
            {
                var currentReturn = await _repository.GetByIdAsync(updatedModel.Id);
                if (currentReturn == null) return NotFound($"Could not find Return with Id of {updatedModel.Id}");

                _mapper.Map(updatedModel, currentReturn);

                if (await _repository.SaveAsync())
                {
                    return _mapper.Map<ReturnModel>(currentReturn);
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
                var Return = await _repository.GetByIdAsync(Id);
                if (Return == null) return NotFound();

                if (await _repository.DeleteAsync(Return))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest("Failed to delete the Return");
        }

    }
}
