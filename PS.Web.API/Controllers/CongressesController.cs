using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PS.Shared.Models;
using PS.Web.API.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PS.Web.API.Controllers
{
    public class CongressesController : BaseAPI
    {
        public CongressesController(PolysenseContext context) : base(context)
        {
        }

        // GET: api/Congresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Congress>>> GetCongress()
        {
            return await _context.Congress.ToListAsync();
        }

        // GET: api/Congresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Congress>> GetCongress(int id)
        {
            var congress = await _context.Congress.FindAsync(id);

            if (congress == null)
            {
                return NotFound();
            }

            return congress;
        }

        // PUT: api/Congresses/5 To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCongress(int id, Congress congress)
        {
            if (id != congress.Id)
            {
                return BadRequest();
            }

            _context.Entry(congress).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CongressExists(id))
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

        // POST: api/Congresses To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Congress>> PostCongress(Congress congress)
        {
            _context.Congress.Add(congress);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCongress", new { id = congress.Id }, congress);
        }

        // DELETE: api/Congresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCongress(int id)
        {
            var congress = await _context.Congress.FindAsync(id);
            if (congress == null)
            {
                return NotFound();
            }

            _context.Congress.Remove(congress);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CongressExists(int id)
        {
            return _context.Congress.Any(e => e.Id == id);
        }
    }
}