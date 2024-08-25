using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReplyController : ControllerBase
    {
        private readonly BlogContext db;
        public ReplyController(BlogContext context)
        {
            db = context;
        }

        [HttpPost]
        public async Task<ActionResult> PostReply(int id, [FromBody] ReplyTable replyTable)
        {
            
            var publication = await db.PublicationTables.FindAsync(id);
            if (publication == null)
            {
                return NotFound("La publicación no existe.");
            }

            
            replyTable.IdPublication = id;

           
            db.Add(replyTable);
            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ReplyTable>>> GetReply(int id)
        {
            var replys = await db.ReplyTables.Where(x => x.IdPublication == id).ToListAsync();

            if (replys == null || !replys.Any())
            {
                return NotFound("No hay ninguna respuesta");
            }

            return Ok(replys);
        }
    }
}
