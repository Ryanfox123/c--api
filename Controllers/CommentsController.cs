using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Comments;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly ICommentRepository _commentRepo;
        public CommentController(ApplicationDBContext context, ICommentRepository commentRepo)
        {
            _context = context;
            _commentRepo = commentRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<CommentDto>>> GetAll()
        {
            var comments = await _commentRepo.GetAllAsync();

            var commentsDto = comments.Select(x => x.ToCommentDto());

            return Ok(commentsDto);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CommentDto>> GetById([FromRoute]int id)
        {
            var comment = await _commentRepo.GetByIdAsync(id);

            if (comment == null) return NotFound();

            return Ok(comment.ToCommentDto());
        }
    }
}