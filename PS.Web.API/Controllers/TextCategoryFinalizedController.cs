using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PS.Shared.Models;
using PS.Web.API.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PS.Web.API.Controllers
{
    public class TextCategoryFinalizedController : BaseAPI
    {
        public TextCategoryFinalizedController(PolysenseContext context) : base(context)
        {
        }

        // GET: api/TextCategoryFinalized
        [HttpGet]
        public IEnumerable<TextCategoryFinalized> GetTextCategoryFinalized()
        {
            return _context.TextCategoryFinalized.Include(t => t.Category);
        }

        // GET: api/TextCategoryFinalized/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTextCategoryFinalized([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var textCategoryFinalized = await _context.TextCategoryFinalized.Include(t => t.Category).FirstOrDefaultAsync(t => t.Id == id);

            if (textCategoryFinalized == null)
            {
                return NotFound();
            }

            return Ok(textCategoryFinalized);
        }

        // PUT: api/TextCategoryFinalized/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTextCategoryFinalized([FromRoute] int id, [FromBody] TextCategoryFinalized textCategoryFinalized)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != textCategoryFinalized.Id)
            {
                return BadRequest();
            }

            _context.Entry(textCategoryFinalized).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TextCategoryFinalizedExists(id))
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

        // POST: api/TextCategoryFinalized
        [HttpPost]
        public async Task<IActionResult> PostTextCategoryFinalized([FromBody] TextCategoryFinalized textCategoryFinalized)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.TextCategoryFinalized.Update(textCategoryFinalized);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTextCategoryFinalized", new { id = textCategoryFinalized.Id }, textCategoryFinalized);
        }

        // DELETE: api/TextCategoryFinalized/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTextCategoryFinalized([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var textCategoryFinalized = await _context.TextCategoryFinalized.FindAsync(id);
            if (textCategoryFinalized == null)
            {
                return NotFound();
            }

            _context.TextCategoryFinalized.Remove(textCategoryFinalized);
            await _context.SaveChangesAsync();

            return Ok(textCategoryFinalized);
        }

        private bool TextCategoryFinalizedExists(int id)
        {
            return _context.TextCategoryFinalized.Any(e => e.Id == id);
        }
    }
}