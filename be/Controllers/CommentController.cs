using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using be.Dtos;
using be.Dtos.Comment;
using be.Extensions;
using be.Helpers;
using be.Interfaces;
using be.Mappers;
using be.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace be.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;
        private readonly UserManager<AppUser> _userManager;
        public CommentController(ICommentRepository commentRepo, IStockRepository stockRepository, UserManager<AppUser> userManager)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepository;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] CommentQueryObject query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comments = await _commentRepo.GetAllAsync(query);
            var commentdtos = comments.Select(s => s.ToDtoFromComment());
            return Ok(commentdtos);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var cmt = await _commentRepo.GetByIdAsync(id);
            if (cmt == null)
            {
                return NotFound();
            }
            return Ok(cmt.ToDtoFromComment());
        }
        [HttpPost]
        [Route("{stockId:int}")]
        [Authorize]
        public async Task<IActionResult> Create([FromRoute] int stockId, [FromBody] CreateCommentDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!await _stockRepo.StockExist(stockId))
            {
                return BadRequest("Stock not exist");
            }
            var username = User.GetCurrentUsername();
            var appUser = await _userManager.FindByNameAsync(username);

            var comment = dto.ToCreateCommentFromDto(stockId);
            comment.AppUserId = appUser.Id;
            await _commentRepo.CreateAsync(comment);
            return CreatedAtAction(nameof(GetById), new { id = comment.Id }, comment.ToDtoFromComment());
        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var cmt = await _commentRepo.UpdateAsync(id, dto.ToUpdateCommentFromDto());
            if (cmt == null)
            {
                return NotFound("Comment not found");
            }
            return Ok(cmt.ToDtoFromComment());
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var cmt = await _commentRepo.DeleteAsync(id);
            if (cmt == null)
            {
                return NotFound("Comment not found");
            }
            return Ok(cmt);
        }
    }
}