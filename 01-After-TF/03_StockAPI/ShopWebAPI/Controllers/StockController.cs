﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ShopDomain.DataAccess;
using ShopDomain.Model;
using ShopWebAPI.Model;

namespace ShopWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class StockController : Controller
    {
        private IRepository<Stock> _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;


        public StockController(IRepository<Stock> repository, IMapper mapper, LinkGenerator linkGenerator)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }

        [HttpGet]
        public async Task<ActionResult<StockModel[]>> Get()
        {
            try
            {
                var results = await _repository.GetAllAsync();

                return _mapper.Map<StockModel[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<StockModel>> Get(int Id)
        {
            try
            {
                var result = await _repository.GetByIdAsync(Id);

                if (result == null) return NotFound();
                return _mapper.Map<StockModel>(result);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<StockModel>> Post(StockModel model)
        {
            try
            {
                //Make sure StockId is not already taken
                var existing = await _repository.GetByIdAsync(model.Id);
                if (existing != null)
                {
                    return BadRequest("Id in Use");
                }

                //save and return
                var newStock = _mapper.Map<Stock>(model);

                if (!await _repository.AddAsync(newStock))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "Stock",
                            new { newStock.Id });

                    return Created(location, _mapper.Map<StockModel>(newStock));
                }

            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<StockModel>> Put(StockModel updatedModel)
        {
            try
            {
                var currentStock = await _repository.GetByIdAsync(updatedModel.Id);
                if (currentStock == null) return NotFound($"Could not find Stock with Id of {updatedModel.Id}");

                _mapper.Map(updatedModel, currentStock);

                if (await _repository.SaveAsync())
                {
                    return _mapper.Map<StockModel>(currentStock);
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
                var Stock = await _repository.GetByIdAsync(Id);
                if (Stock == null) return NotFound();

                if (await _repository.DeleteAsync(Stock))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest("Failed to delete the Stock");
        }

    }
}
