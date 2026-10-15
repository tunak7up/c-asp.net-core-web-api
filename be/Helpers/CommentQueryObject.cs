using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace be.Helpers
{
    public class CommentQueryObject
    {
        public string? CreatedByStockWithSymbol { get; set; }
        public bool IsDecsending { get; set; } = true;
    }
}