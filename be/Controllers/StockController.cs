using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using be.Data;
using be.Dtos.Stock;
using be.Interfaces;
using be.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace be.Controller
{

    [Route("be/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IStockRepository _stockRepo;

        public StockController(ApplicationDBContext context, IStockRepository stockRepo)
        {
            _context = context;
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stocks = await _stockRepo.GetAllStock();
            var stockdtos = stocks.Select(s => s.ToDtoFromStock());
            return Ok(stocks);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock = await _context.Stock.FindAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToDtoFromStock());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockDto dto)
        {
            var stock = dto.ToStockFromDto();
            await _context.Stock.AddAsync(stock);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = stock.ID }, stock.ToDtoFromStock());
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockDto dto)
        {
            var stock = await _context.Stock.FirstOrDefaultAsync(x => x.ID == id);
            if (stock == null)
            {
                return NotFound();
            }
            _context.Entry(stock).CurrentValues.SetValues(dto);
            await _context.SaveChangesAsync();
            return Ok(stock.ToDtoFromStock());
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var stock = await _context.Stock.FirstOrDefaultAsync(x => x.ID == id);
            if (stock == null)
            {
                return NotFound();
            }
            _context.Remove(stock);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}