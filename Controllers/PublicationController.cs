using Blog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Blog.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly BlogContext _context;
        public ArticleController(BlogContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> AddArticle([FromBody] PublicationTable article)
        {
            _context.Add(article);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateArticle([FromBody] PublicationTable article, int articleId)
        {
            var existingArticle = await _context.Publications.FirstOrDefaultAsync(x => x.IdPublication == articleId);
            if (existingArticle == null)
            {
                return NoContent();
            }
            existingArticle.Content = article.Content;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublicationTable>>> GetArticles()
        {
            var articles = await _context.Publications.ToListAsync();
            return Ok(articles);
        }

        [HttpGet("{articleId}")]
        public async Task<ActionResult<PublicationTable>> GetArticle(int articleId)
        {
            var article = await _context.Publications.FirstOrDefaultAsync(x => x.IdPublication == articleId);
            return Ok(article);
        }
    }
}
