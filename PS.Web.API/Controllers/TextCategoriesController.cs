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
    public class TextCategoriesController : BaseAPI
    {
        public TextCategoriesController(PolysenseContext context) : base(context)
        {
        }

        // GET: api/TextCategories
        [HttpGet]
        public async Task<ActionResult<PagedResponse<IEnumerable<TextCategory>>>> GetTextCategory([FromQuery] PaginationFilter filter)
        {
            var pagedData = await _context.TextCategory.GetPageResponse(filter.PageNumber, filter.PageSize);
            return Ok(pagedData);
        }

        // GET: api/TextCategories/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTextCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var textCategory = await _context.TextCategory.FindAsync(id);

            if (textCategory == null)
            {
                return NotFound();
            }

            return Ok(textCategory);
        }

        // PUT: api/TextCategories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTextCategory([FromRoute] int id, [FromBody] TextCategory textCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != textCategory.Id)
            {
                return BadRequest();
            }

            _context.Entry(textCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TextCategoryExists(id))
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

        // POST: api/TextCategories
        [HttpPost]
        public async Task<IActionResult> PostTextCategory([FromBody] TextCategory textCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.TextCategory.Add(textCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTextCategory", new { id = textCategory.Id }, textCategory);
        }

        // DELETE: api/TextCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTextCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var textCategory = await _context.TextCategory.FindAsync(id);
            if (textCategory == null)
            {
                return NotFound();
            }

            _context.TextCategory.Remove(textCategory);
            await _context.SaveChangesAsync();

            return Ok(textCategory);
        }

        private bool TextCategoryExists(int id)
        {
            return _context.TextCategory.Any(e => e.Id == id);
        }
    }
}