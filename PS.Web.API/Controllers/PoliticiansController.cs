using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PS.Web.API.Extensions;
using PS.Shared.Http;
        using PS.Shared.Models;
        using PS.Web.API.Data;

namespace PS.Web.API.Controllers
{
    public class PoliticiansController : BaseAPI
    {

    public PoliticiansController(PolysenseContext context) : base(context)
    {
    }

    // GET: api/Politicians
    [HttpGet]
    public async Task<ActionResult<PagedResponse<IEnumerable<Politician>>>> GetPolitician([FromQuery] PaginationFilter filter)
        {
            var pagedData = await _context.Politician.GetPageResponse(filter.PageNumber, filter.PageSize);
            return Ok(pagedData);
        }

        // GET: api/Politicians/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPolitician([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var politician = await _context.Politician.FindAsync(id);

            if (politician == null)
            {
                return NotFound();
            }

            return Ok(politician);
        }

        // PUT: api/Politicians/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPolitician([FromRoute] int id, [FromBody] Politician politician)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != politician.Id)
            {
                return BadRequest();
            }

            _context.Entry(politician).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PoliticianExists(id))
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

        // POST: api/Politicians
        [HttpPost]
        public async Task<IActionResult> PostPolitician([FromBody] Politician politician)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Politician.Add(politician);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPolitician", new { id = politician.Id }, politician);
        }

        // DELETE: api/Politicians/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePolitician([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var politician = await _context.Politician.FindAsync(id);
            if (politician == null)
            {
                return NotFound();
            }

            _context.Politician.Remove(politician);
            await _context.SaveChangesAsync();

            return Ok(politician);
        }

        private bool PoliticianExists(int id)
        {
            return _context.Politician.Any(e => e.Id == id);
        }
    }
}