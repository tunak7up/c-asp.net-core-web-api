using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using be.Data;
using be.Mappers;
using be.Models;
using Microsoft.AspNetCore.Mvc;

namespace be.Controllers
{
    [Route("be/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        public readonly ApplicationDBContext _context;
        public CommentController(ApplicationDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var comments = _context.Comments.ToList().Select(s => s.ToCommentDto());
            return Ok(comments);
        }
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var comment = _context.Comments.Find(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment.ToCommentDto());
        }
    }
}