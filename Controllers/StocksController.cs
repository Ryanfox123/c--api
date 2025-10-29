using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Controllers.Interfaces;
using api.Data;
using api.Dtos.Stocks;
using api.Helpers;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IStockRepository _stockRepo;
        public StocksController(ApplicationDBContext context, IStockRepository stockRepo)
        {
            _context = context;
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<StocksDto>>> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var stocks = await _stockRepo.GetAllAsync(query);
            
            var stockDto = stocks.Select(s => s.ToStockDto()).ToList();

            return Ok(stockDto);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<StocksDto>> GetById(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var stock = await _stockRepo.GetByIdAsync(id);

            if (stock == null) return NotFound();

            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<ActionResult<StocksDto>> Create([FromBody] CreateStockDto stockDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var stockModel = stockDto.ToStockFromCreateDTO();

            await _stockRepo.CreateAsync(stockModel);

            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<StocksDto>> Update([FromRoute] int id, [FromBody] UpdateStockDto updateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var stockModel = await _stockRepo.UpdateAsync(id, updateDto);

            if (stockModel == null) return NotFound();

            return Ok(stockModel.ToStockDto());
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var stockModel = await _stockRepo.DeleteAsync(id);

            if (stockModel == null) return NotFound();

            return NoContent();
        }
    }
}