using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using be.Dtos.Comment;
using be.Models;

namespace be.Mappers
{
    public static class CommentMappers
    {
        public static CommentDto ToCommentDto(this Comment commentDto)
        {
            return new CommentDto
            {
                Id = commentDto.Id,
                Title = commentDto.Title,
                Content = commentDto.Content,
                CreatedOn = commentDto.CreatedOn,
                StockId = commentDto.StockId
            };
        }
    }
}