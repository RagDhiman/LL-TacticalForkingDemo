using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ShopDomain.DataAccess;
using ShopDomain.Model;
using ShopWebAPI.Model;

namespace ShopWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private IRepository<Account> _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;


        public AccountController(IRepository<Account> repository, IMapper mapper, LinkGenerator linkGenerator)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }

        [HttpGet]
        public async Task<ActionResult<AccountModel[]>> Get()
        {
            try
            {
                var results = await _repository.GetAllAsync();

                return _mapper.Map<AccountModel[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<AccountModel>> Get(int Id)
        {
            try
            {
                var result = await _repository.GetByIdAsync(Id);

                if (result == null) return NotFound();
                return _mapper.Map<AccountModel>(result);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<AccountModel>> Post(AccountModel model)
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
                var newAccount = _mapper.Map<Account>(model);

                if (!await _repository.AddAsync(newAccount))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "Account",
                            new { newAccount.Id });

                    return Created(location, _mapper.Map<AccountModel>(newAccount));
                }

            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<AccountModel>> Put(AccountModel updatedModel)
        {
            try
            {
                var currentAccount = await _repository.GetByIdAsync(updatedModel.Id);
                if (currentAccount == null) return NotFound($"Could not find Address with Id of {updatedModel.Id}");

                _mapper.Map(updatedModel, currentAccount);

                if (await _repository.SaveAsync())
                {
                    return _mapper.Map<AccountModel>(currentAccount);
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

                if (await _repository.DeleteAsync(account))
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
