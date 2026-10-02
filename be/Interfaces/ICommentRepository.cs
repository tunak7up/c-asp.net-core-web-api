using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using be.Dtos.Comment;
using be.Models;

namespace be.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment?> GetByIdAsync(int id);
        Task<Comment> CreateAsync(Comment cmt);
        Task<bool> CommentExist(int id);
        Task<Comment?> UpdateAsync(int id, Comment model);
        Task<Comment?> DeleteAsync(int id);
        Task<Comment[]?> DeleteByStockIdAsync(int id);
    }
}