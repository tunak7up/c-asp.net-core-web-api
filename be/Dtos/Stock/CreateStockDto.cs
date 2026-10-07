using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace be.Dtos.Stock
{
    public class CreateStockDto
    {
        [Required]
        [MaxLength(50, ErrorMessage = "symbol length cant be more than 50")]
        public string Symbol { get; set; } = string.Empty;
        [Required]
        [MaxLength(50, ErrorMessage = "length cant be more than 50")]
        public string CompanyName { get; set; } = string.Empty;
        [Required]
        [Range(1, 1000000000, ErrorMessage = "purchase range 1 to 1000000000")]
        public decimal Purchase { get; set; }
        [Required]
        [Range(0.001, 100, ErrorMessage = "please input range from 0.001 to 100")]
        public decimal LastDiv { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "length cant be more than 50")]
        public string Industry { get; set; } = string.Empty;
        [Range(1, 5000000000)]
        public long MarketCap { get; set; }
    }
}