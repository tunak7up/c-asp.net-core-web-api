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
        public static CommentDto ToDtoFromComment(this Comment cmt)
        {
            return new CommentDto
            {
                Id = cmt.Id,
                Title = cmt.Title,
                Content = cmt.Content,
                CreatedOn = cmt.CreatedOn,
                StockId = cmt.StockId
            };
        }
        public static Comment ToCreateCommentFromDto(this CreateCommentDto dto, int stockId)
        {
            return new Comment
            {
                Title = dto.Title,
                Content = dto.Content,
                StockId = stockId
            };
        }
        public static Comment ToUpdateCommentFromDto(this UpdateCommentDto dto)
        {
            return new Comment
            {
                Title = dto.Title,
                Content = dto.Content,
            };
        }
    }
}