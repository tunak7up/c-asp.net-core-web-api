using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using be.Dtos;
using be.Dtos.Comment;
using be.Interfaces;
using be.Mappers;
using be.Models;
using Microsoft.AspNetCore.Mvc;

namespace be.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;
        public CommentController(ICommentRepository commentRepo, IStockRepository stockRepository)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentRepo.GetAllAsync();
            var commentdtos = comments.Select(s => s.ToDtoFromComment());
            return Ok(commentdtos);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var cmt = await _commentRepo.GetByIdAsync(id);
            if (cmt == null)
            {
                return NotFound();
            }
            return Ok(cmt.ToDtoFromComment());
        }
        [HttpPost]
        [Route("{id}")]
        public async Task<IActionResult> Create([FromRoute] int id, [FromBody] CreateCommentDto dto)
        {
            if (!await _stockRepo.StockExist(id))
            {
                return BadRequest("Stock not exist");
            }
            var comment = dto.ToCreateCommentFromDto(id);
            await _commentRepo.CreateAsync(comment);
            return CreatedAtAction(nameof(GetById), new { id = comment.Id }, comment.ToDtoFromComment());
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentDto dto)
        {
            var cmt = await _commentRepo.UpdateAsync(id, dto.ToUpdateCommentFromDto());
            if (cmt == null)
            {
                return NotFound("Comment not found");
            }
            return Ok(cmt.ToDtoFromComment());
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var cmt = await _commentRepo.DeleteAsync(id);
            if (cmt == null)
            {
                return NotFound("Comment not found");
            }
            return Ok(cmt);
        }
    }
}