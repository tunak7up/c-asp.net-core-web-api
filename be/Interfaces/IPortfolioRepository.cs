using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using be.Dtos.Stock;
using be.Models;

namespace be.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<List<StockDto>> GetUserStocksAsync(AppUser user);
        Task<Portfolio> CreateAsync(Portfolio portfolio);
        Task<Portfolio> DeleteAsync(AppUser appUser, string symbol);
    }
}