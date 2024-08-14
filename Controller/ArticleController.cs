using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
[ApiController]
[Route("api/[controller]")]
public class ArticleController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ArticleController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Article
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ArticleDTO>>> GetArticles()
    {
        return await _context.Articles
            .Select(article => article.ToDTO())
            .ToListAsync();
    }

    // GET: api/Article/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<ArticleDTO>> GetArticle(int id)
    {
        var article = await _context.Articles
            .Select(article => article.ToDTO())
            .FirstOrDefaultAsync(a => a.Id == id);

        if (article == null)
        {
            return NotFound();
        }

        return article;
    }

    // GET: api/Article/name/{name}
    [HttpGet("name/{name}")]
    public async Task<ActionResult<ArticleDTO>> GetArticleByName(string name)
    {
        var article = await _context.Articles
            .Where(a => a.Categorie == name)
            .Select(article => article.ToDTO())
            .FirstOrDefaultAsync();

        if (article == null)
        {
            return NotFound();
        }

        return article;
    }

    // POST: api/Article
    [HttpPost]
    public async Task<ActionResult<ArticleDTO>> PostArticle(ArticleDTO articleDTO)
    {
        var article = articleDTO.ToEntity();

        _context.Articles.Add(article);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetArticle), new { id = article.Id }, article.ToDTO());
    }

    // PUT: api/Article/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutArticle(int id, ArticleDTO articleDTO)
    {
        if (id != articleDTO.Id)
        {
            return BadRequest();
        }

        var article = articleDTO.ToEntity();

        _context.Entry(article).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ArticleExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: api/Article/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteArticle(int id)
    {
        var article = await _context.Articles.FindAsync(id);
        if (article == null)
        {
            return NotFound();
        }

        _context.Articles.Remove(article);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ArticleExists(int id)
    {
        return _context.Articles.Any(e => e.Id == id);
    }
}