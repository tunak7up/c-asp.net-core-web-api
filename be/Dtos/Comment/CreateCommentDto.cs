using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace be.Dtos.Comment
{
    public class CreateCommentDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Please input more than 5 char")]
        [MaxLength(280, ErrorMessage = "title cant be more than 280 char")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(5, ErrorMessage = "Please input more than 5 char")]
        [MaxLength(280, ErrorMessage = "content cant be more than 280 char")]
        public string Content { get; set; } = string.Empty;
    }
}