using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace be.Dtos.Account
{
    public class ChangePasswordDto
    {
        [Required]
        public required string OldPassword { get; set; }
        public required string NewPassword { get; set; }
    }
}