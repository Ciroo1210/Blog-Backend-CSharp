using Blog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Blog.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicationController : ControllerBase
    {
        private readonly BlogContext db;
        public PublicationController(BlogContext context)
        {
            db = context;
        }
        [HttpPost]
        public async Task<ActionResult> PostPublication([FromBody]PublicationTable dataPubl)
        {
            db.Add(dataPubl);
            await db.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut]
        public async Task<ActionResult> PutPublication([FromBody]PublicationTable dataPubl, int id)
        {
            var publ = await db.PublicationTables.FirstOrDefaultAsync(x => x.IdPublication == id);
            if (id == null)
            {
                return NoContent();
            }
            publ.Content = dataPubl.Content;
            await db.SaveChangesAsync();
            return Ok();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublicationTable>>> GetPublication()
        {
            var publ = await db.PublicationTables.ToListAsync();

            return Ok(publ);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<PublicationTable>>> GetPublication(int id)
        {
            var publ = await db.PublicationTables.FirstOrDefaultAsync(x => x.IdPublication == id);
            return Ok(publ);
        }
    }
}
