using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading.Tasks;
using be.Data;
using be.Dtos.Comment;
using be.Interfaces;
using be.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace be.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _context;
        public CommentRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.Comments.FindAsync(id);
        }
        public async Task<Comment> CreateAsync(Comment cmt)
        {
            await _context.AddAsync(cmt);
            await _context.SaveChangesAsync();
            return cmt;
        }

        public async Task<bool> CommentExist(int id)
        {
            return await _context.Comments.AnyAsync(x => x.Id == id);
        }
        public async Task<Comment?> UpdateAsync(int id, Comment model)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return null;
            }
            comment.Title = model.Title;
            comment.Content = model.Content;
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            var cmt = await _context.Comments.FindAsync(id);
            if (cmt == null)
            {
                return null;
            }
            _context.Remove(cmt);
            await _context.SaveChangesAsync();
            return cmt;
        }

        public async Task<Comment[]?> DeleteByStockIdAsync(int id)
        {
            Comment[] comments = await _context.Comments
            .Where(cmt => cmt.StockId == id)
            .ToArrayAsync();
            if (comments.Length == 0)
            {
                return null;
            }
            foreach (Comment cmt in comments)
            {
                _context.Remove(cmt);
            }
            await _context.SaveChangesAsync();
            return comments;
        }
    }
}