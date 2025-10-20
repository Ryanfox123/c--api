using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Controllers.Interfaces;
using api.Data;
using api.Dtos.Comments;
using api.Interfaces;
using api.Mappers;
using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;
        public CommentsController(ApplicationDBContext context, ICommentRepository commentRepo, IStockRepository stockRepo)
        {
            _context = context;
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<CommentDto>>> GetAll()
        {
            var comments = await _commentRepo.GetAllAsync();

            var commentsDto = comments.Select(x => x.ToCommentDto());

            return Ok(commentsDto);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CommentDto>> GetById([FromRoute] int id)
        {
            var comment = await _commentRepo.GetByIdAsync(id);

            if (comment == null) return NotFound();

            return Ok(comment.ToCommentDto());
        }
        [HttpPost("{stockId}")]
        public async Task<ActionResult<CommentDto>> Create([FromRoute] int stockId, CreateCommentDto commentDto)
        {
            if (!await _stockRepo.StockExists(stockId)) return BadRequest("Stock does not exist.");

            var commentModel = commentDto.ToCommentFromCreateDto(stockId);

            await _commentRepo.CreateAsync(commentModel);

            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<CommentDto>> Update([FromRoute] int id,[FromBody] UpdateCommentDto updateCommentDto)
        {

            var commentModel = await _commentRepo.UpdateAsync(updateCommentDto.ToCommentFromUpdateDto(), id);

            if (commentModel == null) return NotFound();

            return Ok(commentModel.ToCommentDto());
        }
    }
}