using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shop_BackendForFrontend_API.BaseAddresses;
using Shop_BackendForFrontend_API.Data;
using Shop_BackendForFrontend_API.Domain;
using Shop_BackendForFrontend_API.Model;

namespace Shop_BackendForFrontend_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IHTTPRepository<Account> _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IHTTPRepository<Account> repository, IAccountsAPIBaseAddress baseAddress, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<AccountController> logger)
        {
            _repository = repository;
            _repository.SetBaseAddress(baseAddress, "api/Account");

            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<AccountModel[]>> Get()
        {
            try
            {
                var results = await _repository.GetAllAsync();

                return _mapper.Map<AccountModel[]>(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
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
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPost]
        public async Task<ActionResult<AccountModel>> Post(AccountModel model)
        {
            try
            {
                //Make sure AccountId is not already taken
                var existing = await _repository.GetByIdAsync(model.Id);
                if (existing != null)
                {
                    return BadRequest("Account Id in Use");
                }

                //map
                var Account = _mapper.Map<Account>(model);

                //save and return
                if (!await _repository.AddAsync(Account))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "Account",
                            new { Id = Account.Id });

                    return Created(location, _mapper.Map<AccountModel>(Account));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<AccountModel>> Put(int Id, AccountModel updatedModel)
        {
            try
            {
                var currentAccount = await _repository.GetByIdAsync(Id);
                if (currentAccount == null) return NotFound($"Could not find Account with Id of {Id}");

                _mapper.Map(updatedModel, currentAccount);

                if (await _repository.UpdateAsync(currentAccount))
                {
                    return _mapper.Map<AccountModel>(currentAccount);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }

            return BadRequest();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                var Account = await _repository.GetByIdAsync(Id);
                if (Account == null) return NotFound();

                if (await _repository.DeleteAsync(Account))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }

            return BadRequest("Failed to delete the Account");
        }

    }
}
