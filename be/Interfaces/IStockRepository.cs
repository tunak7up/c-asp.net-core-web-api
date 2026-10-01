using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using be.Models;

namespace be.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllStock();
    }
}