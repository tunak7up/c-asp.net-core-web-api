using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace be.Dtos.Stock
{
    public class StockDto
    {
        public int ID { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public decimal Purchase { get; set; }
    }
}