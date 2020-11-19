using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PS.Shared.Models;
using PS.Web.API.Data;

namespace PS.Web.API.Controllers
{
    public class CongressionalCommittesController : BaseAPI
    {

        public CongressionalCommittesController(PolysenseContext context) : base(context)
        {
        }

        // GET: api/CongressionalCommittes
        [HttpGet]
        public IEnumerable<CongressionalCommitte> GetCongressionalCommitte()
        {
            return _context.CongressionalCommitte;
        }

        // GET: api/CongressionalCommittes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCongressionalCommitte([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var congressionalCommitte = await _context.CongressionalCommitte.FindAsync(id);

            if (congressionalCommitte == null)
            {
                return NotFound();
            }

            return Ok(congressionalCommitte);
        }

        // PUT: api/CongressionalCommittes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCongressionalCommitte([FromRoute] int id, [FromBody] CongressionalCommitte congressionalCommitte)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != congressionalCommitte.Id)
            {
                return BadRequest();
            }

            _context.Entry(congressionalCommitte).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CongressionalCommitteExists(id))
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

        // POST: api/CongressionalCommittes
        [HttpPost]
        public async Task<IActionResult> PostCongressionalCommitte([FromBody] CongressionalCommitte congressionalCommitte)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CongressionalCommitte.Add(congressionalCommitte);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCongressionalCommitte", new { id = congressionalCommitte.Id }, congressionalCommitte);
        }

        // DELETE: api/CongressionalCommittes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCongressionalCommitte([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var congressionalCommitte = await _context.CongressionalCommitte.FindAsync(id);
            if (congressionalCommitte == null)
            {
                return NotFound();
            }

            _context.CongressionalCommitte.Remove(congressionalCommitte);
            await _context.SaveChangesAsync();

            return Ok(congressionalCommitte);
        }

        private bool CongressionalCommitteExists(int id)
        {
            return _context.CongressionalCommitte.Any(e => e.Id == id);
        }
    }
}