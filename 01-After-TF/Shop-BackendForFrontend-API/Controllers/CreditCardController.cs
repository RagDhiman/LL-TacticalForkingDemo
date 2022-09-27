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
    public class CreditCardController : Controller
    {
        private readonly IHTTPRepository<CreditCard> _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<CreditCardController> _logger;

        public CreditCardController(IHTTPRepository<CreditCard> repository, IAccountsAPIBaseAddress baseAddress, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<CreditCardController> logger)
        {
            _repository = repository;
            _repository.SetBaseAddress(baseAddress, "api/CreditCard");

            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<CreditCardModel[]>> Get()
        {
            try
            {
                var results = await _repository.GetAllAsync();

                return _mapper.Map<CreditCardModel[]>(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<CreditCardModel>> Get(int Id)
        {
            try
            {
                var result = await _repository.GetByIdAsync(Id);

                if (result == null) return NotFound();

                return _mapper.Map<CreditCardModel>(result);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPost]
        public async Task<ActionResult<CreditCardModel>> Post(CreditCardModel model)
        {
            try
            {
                //Make sure CreditCardId is not already taken
                var existing = await _repository.GetByIdAsync(model.Id);
                if (existing != null)
                {
                    return BadRequest("CreditCard Id in Use");
                }

                //map
                var CreditCard = _mapper.Map<CreditCard>(model);

                //save and return
                if (!await _repository.AddAsync(CreditCard))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "CreditCard",
                            new { Id = CreditCard.Id });

                    return Created(location, _mapper.Map<CreditCardModel>(CreditCard));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<CreditCardModel>> Put(int Id, CreditCardModel updatedModel)
        {
            try
            {
                var currentCreditCard = await _repository.GetByIdAsync(Id);
                if (currentCreditCard == null) return NotFound($"Could not find CreditCard with Id of {Id}");

                _mapper.Map(updatedModel, currentCreditCard);

                if (await _repository.UpdateAsync(currentCreditCard))
                {
                    return _mapper.Map<CreditCardModel>(currentCreditCard);
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
                var CreditCard = await _repository.GetByIdAsync(Id);
                if (CreditCard == null) return NotFound();

                if (await _repository.DeleteAsync(CreditCard))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }

            return BadRequest("Failed to delete the CreditCard");
        }

    }
}
