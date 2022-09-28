using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ShopDomain.DataAccess;
using ShopDomain.Model;
using ShopWebAPI.Model;

namespace ShopWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class SupplierController : Controller
    {
        private IRepository<Supplier> _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;


        public SupplierController(IRepository<Supplier> repository, IMapper mapper, LinkGenerator linkGenerator)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }

        [HttpGet]
        public async Task<ActionResult<SupplierModel[]>> Get()
        {
            try
            {
                var results = await _repository.GetAllAsync();

                return _mapper.Map<SupplierModel[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<SupplierModel>> Get(int Id)
        {
            try
            {
                var result = await _repository.GetByIdAsync(Id);

                if (result == null) return NotFound();
                return _mapper.Map<SupplierModel>(result);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<SupplierModel>> Post(SupplierModel model)
        {
            try
            {
                //Make sure SupplierId is not already taken
                var existing = await _repository.GetByIdAsync(model.Id);
                if (existing != null)
                {
                    return BadRequest("Id in Use");
                }

                //save and return
                var newSupplier = _mapper.Map<Supplier>(model);

                if (!await _repository.AddAsync(newSupplier))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "Supplier",
                            new { newSupplier.Id });

                    return Created(location, _mapper.Map<SupplierModel>(newSupplier));
                }

            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<SupplierModel>> Put(SupplierModel updatedModel)
        {
            try
            {
                var currentSupplier = await _repository.GetByIdAsync(updatedModel.Id);
                if (currentSupplier == null) return NotFound($"Could not find Supplier with Id of {updatedModel.Id}");

                _mapper.Map(updatedModel, currentSupplier);

                if (await _repository.SaveAsync())
                {
                    return _mapper.Map<SupplierModel>(currentSupplier);
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
                var Supplier = await _repository.GetByIdAsync(Id);
                if (Supplier == null) return NotFound();

                if (await _repository.DeleteAsync(Supplier))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest("Failed to delete the Supplier");
        }

    }
}
