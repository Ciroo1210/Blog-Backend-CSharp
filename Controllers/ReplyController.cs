using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly BlogContext _context;
        public CommentController(BlogContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> AddComment(int publicationId, [FromBody] ReplyTable comment)
        {
            var publication = await _context.Publications.FindAsync(publicationId);
            if (publication == null)
            {
                return NotFound("Publication not found.");
            }

            comment.IdPublication = publicationId;
            _context.Add(comment);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("{publicationId}")]
        public async Task<ActionResult<IEnumerable<ReplyTable>>> GetComments(int publicationId)
        {
            var comments = await _context.Replies.Where(x => x.IdPublication == publicationId).ToListAsync();

            if (comments == null || !comments.Any())
            {
                return NotFound("No comments found.");
            }

            return Ok(comments);
        }
    }
}
