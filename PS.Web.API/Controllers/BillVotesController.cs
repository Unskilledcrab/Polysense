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
    public class BillVotesController : BaseAPI
    {
        public BillVotesController(PolysenseContext context) : base(context)
        {
        }

        // GET: api/BillVotes
        [HttpGet]
        public async Task<ActionResult<PagedResponse<IEnumerable<BillVotes>>>> GetBillVotes([FromQuery] PaginationFilter filter)
        {
            var pagedData = await _context.BillVotes.GetPageResponseAsync(filter.PageNumber, filter.PageSize);
            return Ok(pagedData);
        }

        // GET: api/BillVotes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBillVotes([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var billVotes = await _context.BillVotes.FindAsync(id);

            if (billVotes == null)
            {
                return NotFound();
            }

            return Ok(billVotes);
        }

        // PUT: api/BillVotes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBillVotes([FromRoute] int id, [FromBody] BillVotes billVotes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != billVotes.Id)
            {
                return BadRequest();
            }

            _context.Entry(billVotes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BillVotesExists(id))
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

        // POST: api/BillVotes
        [HttpPost]
        public async Task<IActionResult> PostBillVotes([FromBody] BillVotes billVotes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.BillVotes.Add(billVotes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBillVotes", new { id = billVotes.Id }, billVotes);
        }

        // DELETE: api/BillVotes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBillVotes([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var billVotes = await _context.BillVotes.FindAsync(id);
            if (billVotes == null)
            {
                return NotFound();
            }

            _context.BillVotes.Remove(billVotes);
            await _context.SaveChangesAsync();

            return Ok(billVotes);
        }

        private bool BillVotesExists(int id)
        {
            return _context.BillVotes.Any(e => e.Id == id);
        }
    }
}