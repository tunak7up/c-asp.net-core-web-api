using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using be.Models;

namespace be.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment?> GetByIdAsync(int id);
        Task<Comment> CreateAsync(Comment cmt);
    }
}