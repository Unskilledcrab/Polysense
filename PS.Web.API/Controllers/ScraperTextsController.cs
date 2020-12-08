using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PS.Shared.Models;
using PS.Web.API.Data;
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
        public IEnumerable<ScraperText> GetScraperText()
        {
            return _context.ScraperText;
        }

        // GET: api/ScraperTexts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetScraperText([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var scraperText = await _context.ScraperText.FindAsync(id);

            if (scraperText == null)
            {
                return NotFound();
            }

            return Ok(scraperText);
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

            _context.Entry(scraperText).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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

            var isMatchingText = _context.ScraperText.Any(s => s.Text == scraperText.Text);

            if (isMatchingText == false)
            {
                _context.ScraperText.Add(scraperText);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetScraperText", new { id = scraperText.Id }, scraperText);
            }
            else
                return Conflict("Scraper already exists!");
        }

        // DELETE: api/ScraperTexts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScraperText([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var scraperText = await _context.ScraperText.FindAsync(id);
            if (scraperText == null)
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