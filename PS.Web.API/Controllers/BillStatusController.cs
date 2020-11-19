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
    public class BillStatusController : BaseAPI
    {

        public BillStatusController(PolysenseContext context) : base(context)
        {
        }

        // GET: api/BillStatus
        [HttpGet]
        public IEnumerable<BillStatus> GetBillStatus()
        {
            return _context.BillStatus;
        }

        // GET: api/BillStatus/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBillStatus([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var billStatus = await _context.BillStatus.FindAsync(id);

            if (billStatus == null)
            {
                return NotFound();
            }

            return Ok(billStatus);
        }

        // PUT: api/BillStatus/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBillStatus([FromRoute] int id, [FromBody] BillStatus billStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != billStatus.Id)
            {
                return BadRequest();
            }

            _context.Entry(billStatus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BillStatusExists(id))
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

        // POST: api/BillStatus
        [HttpPost]
        public async Task<IActionResult> PostBillStatus([FromBody] BillStatus billStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.BillStatus.Add(billStatus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBillStatus", new { id = billStatus.Id }, billStatus);
        }

        // DELETE: api/BillStatus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBillStatus([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var billStatus = await _context.BillStatus.FindAsync(id);
            if (billStatus == null)
            {
                return NotFound();
            }

            _context.BillStatus.Remove(billStatus);
            await _context.SaveChangesAsync();

            return Ok(billStatus);
        }

        private bool BillStatusExists(int id)
        {
            return _context.BillStatus.Any(e => e.Id == id);
        }
    }
}