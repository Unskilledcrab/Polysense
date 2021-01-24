using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PS.Shared.Http;
using PS.Shared.Models;
using PS.Web.API.Data;
using PS.Web.API.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PS.Web.API.Controllers
{
    public class ScraperTextsController : BaseAPI
    {
        public ScraperTextsController(PolysenseContext context) : base(context)
        {
        }

        // GET: api/ScraperTexts
        [HttpGet]
        public async Task<ActionResult<PagedResponse<IEnumerable<ScraperText>>>> GetScraperText([FromQuery] PaginationFilter filter)
        {
            var pagedData = await _context.ScraperText.Include(s => s.Category).GetPageResponse(filter.PageNumber, filter.PageSize);
            return Ok(pagedData);
        }

        // GET: api/ScraperTexts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetScraperText([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var scraperText = await _context.ScraperText.Include(s => s.Category).FirstOrDefaultAsync(i => i.Id == id);

            if (scraperText == null)
            {
                return NotFound();
            }

            return Ok(scraperText);
        }

        // GET: api/ScraperTexts/category/5
        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<ScraperText>>> GetScraperTextByCategoryId([FromRoute] int categoryId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var scraperText = await _context.ScraperText.Include(s => s.Category).Where(i => i.Category.Id == categoryId).ToListAsync();

            if (scraperText == null)
            {
                return NotFound();
            }

            return Ok(scraperText);
        }

        // GET: api/ScraperTexts/category/null
        [HttpGet("category/null")]
        public async Task<ActionResult<IEnumerable<ScraperText>>> GetScraperTextByCategoryNull()
        {
            var scraperText = await _context.ScraperText.Include(s => s.Category).Where(i => i.Category == null).ToListAsync();

            return scraperText;
        }

        // PUT: api/ScraperTexts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutScraperText([FromRoute] int id, [FromBody] ScraperText scraperText)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != scraperText.Id)
            {
                return BadRequest();
            }

            try
            {
                await _context.UpdateOrCreate(scraperText);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScraperTextExists(id))
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

        // POST: api/ScraperTexts
        [HttpPost]
        public async Task<IActionResult> PostScraperText([FromBody] ScraperText scraperText)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _context.UpdateOrCreate(scraperText);
            return CreatedAtAction("GetScraperText", new { id = scraperText.Id }, scraperText);
        }

        // DELETE: api/ScraperTexts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScraperText([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var success = await _context.TryDelete(_context.ScraperText, id);

            var scraperText = await _context.ScraperText.FindAsync(id);
            if (!success)
            {
                return NotFound();
            }

            _context.ScraperText.Remove(scraperText);
            await _context.SaveChangesAsync();

            return Ok(scraperText);
        }

        private bool ScraperTextExists(int id)
        {
            return _context.ScraperText.Any(e => e.Id == id);
        }
    }
}