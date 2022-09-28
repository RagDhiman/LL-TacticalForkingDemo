﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ShopDomain.DataAccess;
using ShopDomain.Model;
using ShopWebAPI.Model;

namespace ShopWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class CreditCardController : Controller
    {
        private IRepository<CreditCard> _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;


        public CreditCardController(IRepository<CreditCard> repository, IMapper mapper, LinkGenerator linkGenerator)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }

        [HttpGet]
        public async Task<ActionResult<CreditCardModel[]>> Get()
        {
            try
            {
                var results = await _repository.GetAllAsync();

                return _mapper.Map<CreditCardModel[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
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
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
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
                    return BadRequest("Id in Use");
                }

                //save and return
                var newCreditCard = _mapper.Map<CreditCard>(model);

                if (!await _repository.AddAsync(newCreditCard))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "CreditCard",
                            new { newCreditCard.Id });

                    return Created(location, _mapper.Map<CreditCardModel>(newCreditCard));
                }

            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<CreditCardModel>> Put(CreditCardModel updatedModel)
        {
            try
            {
                var currentCreditCard = await _repository.GetByIdAsync(updatedModel.Id);
                if (currentCreditCard == null) return NotFound($"Could not find CreditCard with Id of {updatedModel.Id}");

                _mapper.Map(updatedModel, currentCreditCard);

                if (await _repository.SaveAsync())
                {
                    return _mapper.Map<CreditCardModel>(currentCreditCard);
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
                var CreditCard = await _repository.GetByIdAsync(Id);
                if (CreditCard == null) return NotFound();

                if (await _repository.DeleteAsync(CreditCard))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest("Failed to delete the CreditCard");
        }

    }
}
