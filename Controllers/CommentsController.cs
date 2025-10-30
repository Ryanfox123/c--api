using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Controllers.Interfaces;
using api.Data;
using api.Dtos.Comments;
using api.Extensions;
using api.Interfaces;
using api.Mappers;
using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<AppUser> _userManager;
        public CommentsController(ApplicationDBContext context, ICommentRepository commentRepo, IStockRepository stockRepo, UserManager<AppUser> userManager)
        {
            _context = context;
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<CommentDto>>> GetAll()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var comments = await _commentRepo.GetAllAsync();

            var commentsDto = comments.Select(x => x.ToCommentDto());

            return Ok(commentsDto);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CommentDto>> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var comment = await _commentRepo.GetByIdAsync(id);

            if (comment == null) return NotFound();

            return Ok(comment.ToCommentDto());
        }
        [HttpPost("{stockId:int}")]
        [Authorize]
        public async Task<ActionResult<CommentDto>> Create([FromRoute] int stockId, CreateCommentDto commentDto)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!await _stockRepo.StockExists(stockId)) return BadRequest("Stock does not exist.");

            var username = User.GetUsernameFromClaim();
            var appUser = await _userManager.FindByNameAsync(username);

            var commentModel = commentDto.ToCommentFromCreateDto(stockId);
            commentModel.AppUserId = appUser.Id;

            await _commentRepo.CreateAsync(commentModel);

            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());
        }
        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<ActionResult<CommentDto>> Update([FromRoute] int id, [FromBody] UpdateCommentDto updateCommentDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var originalComment = await _commentRepo.GetByIdAsync(id);
            if (originalComment == null) return BadRequest("Comment not found");

            var username = User.GetUsernameFromClaim();
            if (username != originalComment.AppUser.UserName) return Unauthorized();

            var commentModel = await _commentRepo.UpdateAsync(updateCommentDto.ToCommentFromUpdateDto(), id, originalComment);
            if (commentModel == null) return StatusCode(500, "Failed to update comment.");

            return Ok(commentModel.ToCommentDto());
        }
        [HttpDelete]
        [Route("{id:int}")]
        [Authorize]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var commentModel = await _commentRepo.DeleteAsync(id);

            if (commentModel == null) return NotFound();

            return Ok(commentModel);
        }
    }
}