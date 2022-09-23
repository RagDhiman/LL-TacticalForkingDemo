using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopData;
using ShopDomain.DataAccess;
using ShopDomain.Model;

namespace ShopWebAPI.Controllers
{
    public class AccountController : Controller
    {
        private IRepository<Account> _repository;

        public AccountController()
        {
            _repository = new Repository<Account>();
        }

        public AccountController(IRepository<Account> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<Account[]>> Get()
        {
            try
            {
                var results = await _repository.GetAllAsync();
                return results.ToArray();
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Account>> Get(int Id)
        {
            try
            {
                var result = await _repository.GetByIdAsync(Id);

                if (result == null) return NotFound();
                return result;

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Account>> Post(Account model)
        {
            try
            {
                //Make sure AddressId is not already taken
                var existing = await _repository.GetByIdAsync(model.Id);
                if (existing != null)
                {
                    return BadRequest("Address Id in Use");
                }

                //save and return
                if (!await _repository.AddAsync(model))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {

                    return model; // Created(location, _mapper.Map<AddressModel>(Address));
                }

            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<Account>> Put(Account updatedModel)
        {
            try
            {
                var currentAccount = await _repository.GetByIdAsync(updatedModel.Id);
                if (currentAccount == null) return NotFound($"Could not find Address with Id of {updatedModel.Id}");

                if (await _repository.UpdateAsync(updatedModel))
                {
                    return updatedModel;
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
                var account = await _repository.GetByIdAsync(Id);
                if (account == null) return NotFound();

                if (await _repository.Delete(account))
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
