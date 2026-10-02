using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using be.Dtos.Stock;
using be.Models;

namespace be.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync();
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock> CreateAsync(Stock newStock);
        Task<Stock?> UpdateAsync(int id, UpdateStockDto dto);
        Task<Stock?> DeleteAsync(int id);
        Task<bool> StockExist(int id);
    }
}