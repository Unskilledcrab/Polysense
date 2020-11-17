using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PS.Web.API.Data;
using PS.Shared.Models;

namespace PS.Web.API.Controllers
{ 
    public class PoliticiansController : BaseAPI
    {
        public PoliticiansController(PolysenseContext context) : base(context)
        {
        }

        // GET: api/Politicians
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Politician>>> GetPolitician()
        {
            return await _context.Politician.ToListAsync();
        }

        // GET: api/Politicians/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Politician>> GetPolitician(int id)
        {
            var politician = await _context.Politician.FindAsync(id);

            if (politician == null)
            {
                return NotFound();
            }

            return politician;
        }

        // PUT: api/Politicians/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPolitician(int id, Politician politician)
        {
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Politician>> PostPolitician(Politician politician)
        {
            _context.Politician.Add(politician);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPolitician", new { id = politician.Id }, politician);
        }

        // DELETE: api/Politicians/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePolitician(int id)
        {
            var politician = await _context.Politician.FindAsync(id);
            if (politician == null)
            {
                return NotFound();
            }

            _context.Politician.Remove(politician);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PoliticianExists(int id)
        {
            return _context.Politician.Any(e => e.Id == id);
        }
    }
}
