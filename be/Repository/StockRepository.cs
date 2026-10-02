using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using be.Data;
using be.Dtos.Stock;
using be.Interfaces;
using be.Models;
using Microsoft.EntityFrameworkCore;

namespace be.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context;

        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Stock> CreateAsync(Stock newStock)
        {
            await _context.AddAsync(newStock);
            await _context.SaveChangesAsync();
            return newStock;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var toRemove = await _context.Stock.FirstOrDefaultAsync(x => x.ID == id);
            if (toRemove == null)
            {
                return null;
            }
            _context.Stock.Remove(toRemove);
            await _context.SaveChangesAsync();
            return toRemove;
        }

        public async Task<List<Stock>> GetAllAsync()
        {
            return await _context.Stock.Include(c => c.Comments).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stock.Include(c => c.Comments).FirstOrDefaultAsync();
        }

        public async Task<bool> StockExist(int id)
        {
            return await _context.Stock.AnyAsync(s => s.ID == id);
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockDto dto)
        {
            var model = await _context.Stock.FirstOrDefaultAsync(x => x.ID == id);
            if (model == null)
            {
                return null;
            }
            _context.Entry(model).CurrentValues.SetValues(dto);
            await _context.SaveChangesAsync();
            return model;
        }
    }
}