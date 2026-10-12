using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using be.Data;
using be.Dtos.Stock;
using be.Helpers;
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
            var toRemove = await _context.Stocks.FirstOrDefaultAsync(x => x.ID == id);
            if (toRemove == null)
            {
                return null;
            }
            _context.Stocks.Remove(toRemove);
            await _context.SaveChangesAsync();
            return toRemove;
        }

        public async Task<List<Stock>> GetAllAsync(QueryObject query)
        {
            var stocks = _context.Stocks.Include(c => c.Comments).AsQueryable();
            if (!string.IsNullOrEmpty(query.CompanyName))
            {
                stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName));
            }
            if (!string.IsNullOrEmpty(query.Symbol))
            {
                stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol));
            }
            if (!string.IsNullOrEmpty(query.SortBy))
            {
                if (query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = query.IsDecending ? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderBy(s => s.Symbol);
                }
            }
            return await stocks.Skip((query.Page - 1) * query.PageSize).Take(query.PageSize).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(s => s.ID == id);
        }

        public async Task<bool> StockExist(int id)
        {
            return await _context.Stocks.AnyAsync(s => s.ID == id);
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockDto dto)
        {
            var model = await _context.Stocks.FirstOrDefaultAsync(x => x.ID == id);
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